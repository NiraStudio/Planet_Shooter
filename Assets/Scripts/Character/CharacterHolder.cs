using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Attributes;

public class CharacterHolder : MainBehavior,Ihitable {
    public static CharacterHolder Instance;
    void Awake()
    {
        Instance = this;
    }

    public GameObject characterShape, characterGhost;
    public Transform WeaponPos;


    public float speed;

    [HideInInspector]
    public float ammo,damage,MaxHp;
    [HideInInspector]
    public float _hp;
    [HideInInspector]
    public Weapon weapon;

    public SkinManager SM;
    public Animator CharacterAnimator;




    float regenerateSpeed;
    bool right=true;
    bool ground;
    int direction = -1;
    Rigidbody2D rg;
    Animator anim;
    WeaponData weaponData;
    GameManager GM;
    GameObject detailText;
    Transform CenterSpace;
    GamePlayManager GPM;
    PowerUpManager PUM;

    float j;
	// Use this for initialization
	protected virtual void  Start () {
        rg = GetComponent<Rigidbody2D>();
        CenterSpace = GameObject.FindWithTag("Ground").transform;
        GM = GameManager.Instance;
        GPM = GamePlayManager.GPM;
        PUM = PowerUpManager.Instance;
        detailText = Resources.Load<GameObject>("DetailText");

        characterShape.SetActive(true);
        characterGhost.SetActive(false);

        Initialize();

        _hp = MaxHp;
	}
    // Update is called once per frame
    protected virtual void Update () {

        if (GPM.gamePlayState != GamePlayState.Play)
        {
            rg.velocity = Vector2.zero;
            return;
        }

        if (Application.isEditor)
        {

            if (Input.GetMouseButtonDown(1))
                ChangeDirection();

            if (Input.GetKeyDown(KeyCode.H))
                Jump();
        }
        ground = Physics2D.Raycast(transform.position, transform.up * -1, .3f, GroundLayer);


        //move Part

        if (PUM.IsActive(PowerUpType.JetPack))
        {
            Vector2 tt=rg.velocity ;
            if (Vector2.Distance(transform.position, CenterSpace.transform.position) < 8)
            {
                tt =((Vector2) transform.up * 7)+((Vector2)transform.right*speed*direction);
            }
            rg.velocity = tt; 
        }
        else if(j>0)
        {
            j -= Time.deltaTime;
        }
        else if (ground)
            rg.velocity = transform.right * direction * speed;



        if (direction == 1 & !right)
            Flip();
        else if (direction == -1 && right)
            Flip();





        CharacterAnimator.SetBool("Move", direction == 0 ? false:true);
    }

    public virtual void Jump()
    {
        j=0.3f;
        PUM.Activate(PowerUpType.JetPack);
       // rg.AddForce(transform.up * 750, ForceMode2D.Force);
    }

    public virtual void Initialize()
    {
       // InitializeData();
        InitializedWeapon();
        InitializeSkin();

        weapon.Damage = damage;
        weapon.MaxAmmo = ammo;
        weapon.RegenerateSpeed = regenerateSpeed;
    }



    public virtual void InitializeData()
    {

    }

    public virtual void InitializedWeapon()
    {
        weaponData = GM.GetCurrentWeapon();
        GameObject g= Instantiate(weaponData.prefab, WeaponPos.position, Quaternion.identity);
        weapon = g.GetComponent<Weapon>();
        g.transform.SetParent(WeaponPos);
        g.transform.localScale = Vector2.one;
        g.transform.localRotation = Quaternion.Euler(0,0,0);
        AttributeChanger(weaponData.attributes);
    }

    public virtual void InitializeSkin()
    {
        Skin a = SM.SkinByID(GM.CurrentSkin);
        SM.LoadSkin(a);
        AttributeChanger(a.attributes);
    }

    public void AttributeChanger(List<Attribute> at)
    {
        foreach (var item in at)
        {
            switch (item.type)
            {
                case AttributeType.Hp:
                    MaxHp += item.value;
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
    public void AttributeChanger(Attribute at)
    {
        switch (at.type)
        {
            case AttributeType.Hp:
                MaxHp += at.value;
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

    public virtual void OnHit(float dmg)
    {

        if (PUM.IsActive(PowerUpType.Bomb))
        {
            //make bomb
            return;
        }

        if (PUM.IsActive(PowerUpType.Shield))
        {
           
            return;
        }



        _hp -= dmg;
        if (_hp > 0)
        {
            ComboManager.Instance.RemoveCombo();
            GetComponent<CharacterHitScript>().Hited();
        }
        else
            OnDie();
    }

    public virtual void OnHeal(float Amount)
    {
    }

    public virtual void GetEnergy(float Amount)
    {
        weapon.GetAmmo(Amount);
    }

    public virtual void OnDie()
    {
        characterGhost.SetActive(true);
        characterShape.SetActive(false);
        WeaponPos.gameObject.SetActive(false);
        GetComponent<CircleCollider2D>().enabled = false;
        rg.bodyType = RigidbodyType2D.Static;
        GPM.OnGameLost();
        print("Death");
    }

    protected void Flip()
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
