using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Attributes;

public class CharacterHolder : MainBehavior,Ihitable {

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




    //--------------Delegates----------
    public MoveDelegate Movement;
    public MoveDelegate NormalMovementMethod;
    public MoveDelegate JetPackMovementMethod;

    public HitDelegate Hit;
    public HitDelegate normalHit;


    float regenerateSpeed;
    bool right=true;
    bool ground;
    [HideInInspector]
    public int direction = 1;
    protected Rigidbody2D rg;
    Animator anim;
    protected WeaponData weaponData;
    protected Skin sk;
    protected GameManager GM;
    GameObject detailText;
    Transform CenterSpace;
    protected GamePlayManager GPM;
    PowerUpManager PUM;

    float j;

    private void Awake()
    {
        GM = GameManager.Instance;
    }

    // Use this for initialization
    protected virtual void  Start () {
        rg = GetComponent<Rigidbody2D>();
        CenterSpace = GameObject.FindWithTag("Ground").transform;
        GPM = GamePlayManager.GPM;
        PUM = PowerUpManager.Instance;
        detailText = Resources.Load<GameObject>("DetailText");



        #region Movement Delegates

        NormalMovementMethod = () =>
        {
            if (ground&&j<=0)
                rg.velocity = transform.right * direction * speed;
        };
        JetPackMovementMethod = () =>
        {
            Vector2 tt = rg.velocity;
            if (Vector2.Distance(transform.position, CenterSpace.transform.position) < 8)
            {
                tt = ((Vector2)transform.up * 7) + ((Vector2)transform.right * speed * direction);
            }
            rg.velocity = tt;
        };

        Movement = NormalMovementMethod;


        normalHit = ((dmg, Hiter, character) =>
          {
              _hp -= dmg;
              if (_hp > 0)
              {
                  ComboManager.Instance.RemoveCombo();
                  GetComponent<CharacterHitScript>().Hited();
              }
              else
                  OnDie(Hiter);

          });
        Hit = normalHit;
        #endregion



        characterShape.SetActive(true);
        characterGhost.SetActive(false);

        Initialize();

        _hp = MaxHp;
	}
    // Update is called once per frame
    protected virtual void Update () {

        if (GPM.gamePlayState != GamePlayState.Play)
        {
            direction = 0;
            rg.velocity = Vector2.zero;
            return;
        }

        if (direction == 0)
            direction = 1;

    
        ground = Physics2D.Raycast(transform.position, transform.up * -1, .3f, GroundLayer);



        Movement();


        if (direction == 1 & !right)
            Flip();
        else if (direction == -1 && right)
            Flip();


        CharacterAnimator.SetBool("Move", direction == 0 ? false:true);
    }

    public virtual void Jump()
    {
        j=0.3f;
        // PUM.Activate(PowerUpType.JetPack);
        CharacterAnimator.SetTrigger("Jump");
        rg.AddForce(transform.up * 750, ForceMode2D.Force);
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
        GameObject g= Instantiate(weaponData.prefab, WeaponPos.position, Quaternion.identity);
        weapon = g.GetComponent<Weapon>();
        weapon.Shooter = gameObject;
        g.transform.SetParent(WeaponPos);
        g.transform.localScale = Vector2.one;
        g.transform.localRotation = Quaternion.Euler(0,0,0);
        AttributeChanger(weaponData.attributes);
    }

    public virtual void InitializeSkin()
    {
        SM.LoadSkin(sk);
        AttributeChanger(sk.attributes);
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

    public virtual void OnHit(float dmg, Transform Hiter)
    {

        if (PUM.IsActive(PowerUpType.Shield))
        {
           
            return;
        }



        Hit(dmg,Hiter,transform);
    }

    public virtual void OnHeal(float Amount,Transform Healer)
    {
    }

    public virtual void GetEnergy(float Amount)
    {
        weapon.GetAmmo(Amount);
    }

    public virtual void OnDie(Transform Killer)
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


public delegate void MoveDelegate();

public delegate void HitDelegate(float dmg,Transform hitter, Transform character);