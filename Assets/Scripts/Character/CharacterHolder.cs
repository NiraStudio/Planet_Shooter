using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CharacterHolder : MainBehavior,Ihitable {
    public static CharacterHolder Instance;
    void Awake()
    {
        Instance = this;
    }

    public Transform GunPose;


    public float Angle
    {
        get
        {
            var dir = CenterSpace.position - transform.position;
            var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            return angle+180;
        }
    }
    public float speed, ammo,damage;
    public float HP;
    [HideInInspector]
    public float _hp;
    [HideInInspector]
    public Weapon weapon;





    float regenerateSpeed;
    bool right=true;
    bool ground;
    int direction = -1;
    Rigidbody2D rg;
    Animator anim;
    WeaponData weaponData;
    GameManager GM;

     Transform CenterSpace;
    public GamePlayManager GPM;
    PowerUpManager PUM;
	// Use this for initialization
	void Start () {
        rg = GetComponent<Rigidbody2D>();
        CenterSpace = GameObject.FindWithTag("Ground").transform;
        GM = GameManager.Instance;
        GPM = GamePlayManager.Instance;
        PUM = PowerUpManager.Instance;
        anim = transform.GetChild(0).GetComponent<Animator>();
        Initialize();

        _hp = HP;
	}
	// Update is called once per frame
	void Update () {

        if (GPM.gamePlayState != GamePlayState.Play)
        {
            rg.velocity = Vector2.zero;
            return;
        }


        if (Input.GetMouseButtonDown(1))
            ChangeDirection();



        ground = Physics2D.Raycast(transform.position, transform.up * -1, .3f, GroundLayer);
        if (ground)
            rg.velocity = transform.right * direction * speed;



        if (direction == 1 & !right)
            Flip();
        else if (direction == -1 && right)
            Flip();


        var dir = CenterSpace.transform.position - transform.position;
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;


        anim.SetBool("Move", direction == 0 ? false:true);
    }

    public void Initialize()
    {
       // InitializeData();
        InitializedWeapon();
        InitializeSkin();

        weapon.Damage = damage;
        weapon.MaxAmmo = ammo;
        weapon.RegenerateSpeed = regenerateSpeed;
    }



    public void InitializeData()
    {

    }

    public void InitializedWeapon()
    {
        weaponData = GM.GetCurrentWeapon();
        GameObject g= Instantiate(weaponData.prefab, GunPose.position, Quaternion.identity);
        weapon = g.GetComponent<Weapon>();
        g.transform.SetParent(GunPose);
        g.transform.localScale = Vector2.one;
        g.transform.localRotation = Quaternion.Euler(0,0,0);
        AttributeChanger(weaponData.attributes);
    }

    public void InitializeSkin()
    {

    }

    void AttributeChanger(List<Attribute> at)
    {
        foreach (var item in at)
        {
            switch (item.type)
            {
                case AttributeType.Hp:
                    HP += item.value;
                    break;
                case AttributeType.Damage:
                    damage += item.value;
                    break;
                case AttributeType.AmmoAmount:
                    ammo += item.value;
                    break;
                case AttributeType.AmmoRegenerate:
                    regenerateSpeed += item.value;
                    break;

            }

        }
    }
    void AttributeChanger(Attribute at)
    {
        switch (at.type)
        {
            case AttributeType.Hp:
                HP += at.value;
                break;
            case AttributeType.Damage:
                damage += at.value;
                break;
            case AttributeType.AmmoAmount:
                ammo += at.value;
                break;
            case AttributeType.AmmoRegenerate:
                regenerateSpeed += at.value;
                break;

        }
    }

   

    public void ChangeDirection()
    {
        if (direction == 0)
            direction = 1;
        else
            direction *= -1;

        weapon.Direction = direction;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Vector2 tt = transform.position + (transform.up * -1 * .3f);
        Gizmos.DrawLine(transform.position, tt);

    }

    public void OnHit(float dmg)
    {

        if (PUM.IsActive(PowerUpType.Bomb))
        {
            //make bomb
            return;
        }

        if (PUM.IsActive(PowerUpType.Shield))
            return;

        _hp -= dmg;
        if (_hp > 0)
        {
            ComboManager.Instance.RemoveCombo();
            GetComponent<CharacterHitScript>().Hited();
        }
        else
            OnDie();
    }

    public void OnHeal(float Amount)
    {
    }

    public void GetEnergy(float Amount)
    {
        weapon.GetAmmo(Amount);
    }

    public void OnDie()
    {
        GPM.OnGameLost();
        print("Death");
    }

    void Flip()
    {
        Vector3 t = transform.localScale;
        t.x *= -1;
        transform.localScale = t;
        right = !right;
    }


    public void ResetAttributes()
    {

    }
}
