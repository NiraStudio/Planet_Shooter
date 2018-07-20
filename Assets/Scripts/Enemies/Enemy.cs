using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(GroundLooker))]
public class Enemy : MainBehavior, Ihitable
{


    public EnemyData Data;
    public Slider HpBar;
    public float CharacterSizeDetection;
    public float GroundSizeDetection;
    public bool right;
    public int Direction
    {
        get
        {
            return direction;
        }
        set { direction = value; }
    }
    public float Angle
    {
        get
        {
            var dir = CenterSpace.position - transform.position;
            var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            return angle + 180;
        }
    }

    protected string enemyName;
    protected float speed;
    protected float hitPoint;
    protected float dmg;
    protected Rigidbody2D rg;
    protected Collider2D characterCollider;
    protected Animator anim;
    protected int direction = 0;
    protected bool ground;
    protected float dazedTime, stopTime;

    Transform CenterSpace;
    int MoveAllow;
    GamePlayManager GPM;
    CharacterHolder CH;
    // Use this for initialization
    public virtual void Start()
    {
        GPM = GamePlayManager.Instance;
        CH = CharacterHolder.Instance;
        CenterSpace = GameObject.FindWithTag("Ground").transform;

        rg = GetComponent<Rigidbody2D>();
        CameraController.Instance.AddTarget(gameObject);
        anim = GetComponent<Animator>();
        RenewState();

        HpBar.gameObject.SetActive(false);

        OnStart();
    }

    // Update is called once per frame
    public virtual void Update()
    {

        if (GPM.gamePlayState != GamePlayState.Play)
        {
            rg.velocity = Vector2.zero;
            return;
        }

        if (dazedTime > 0)
            dazedTime -= Time.deltaTime;
        if (stopTime > 0)
            stopTime -= Time.deltaTime;

        ground = Physics2D.Raycast(transform.position, transform.up * -1, GroundSizeDetection, GroundLayer);

        #region MoveAllow
        MoveAllow = (stopTime > 0 ? 0 : 1) * (dazedTime > 0 ? 0 : 1);

        #endregion

        if (ground)
            rg.velocity = transform.right * MoveAllow * direction * speed;

        #region Flip
        if (right && direction == -1)
            Flip();
        else if (!right && direction == 1)
            Flip();

        #endregion


        characterCollider = Physics2D.OverlapCircle(transform.position, CharacterSizeDetection, characterLayer);
        if (characterCollider)
        {
            OnCharacterEnter();

        }

        HpBar.value = hitPoint;

        OnUpdate();
    }


    public virtual void RenewState()
    {
        enemyName = Data.enemyName;
        hitPoint = Data.hitPoint;
        speed = Data.speed;
        dmg = Data.dmg;

        HpBar.maxValue = hitPoint;
    }
    public virtual void OnCharacterEnter()
    {
        characterCollider.GetComponent<Ihitable>().OnHit(dmg);
    }
    public virtual void OnUpdate() { }
    public virtual void OnStart() { }

    public void Turn()
    {

        direction *= -1;
    }
    public void Stun(float time)
    {
        dazedTime = time;
    }
    public void Stop(float time)
    {
        stopTime = time;
    }
    public virtual void OnHit(float dmg)
    {
        if (!HpBar.gameObject.activeInHierarchy)
            HpBar.gameObject.SetActive(true);

        hitPoint -= dmg;
        if (hitPoint <= 0)
        {
            hitPoint = 0;
            OnDie();
        }
    }
    public virtual void OnHeal(float dmg)
    {
    }
    public virtual void OnDie()
    {
        Destroy(gameObject);
    }


    void Flip()
    {
        Vector3 t = transform.localScale;
        t.x *= -1;
        transform.localScale = t;
        right = !right;
    }

    protected virtual void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.black;
        Vector2 tt = transform.position + (transform.up * -1 * GroundSizeDetection);
        Gizmos.DrawLine(transform.position, tt);
        Gizmos.DrawWireSphere(transform.position, CharacterSizeDetection);
    }



    private void Reset()
    {
        if (GetComponent<Rigidbody2D>())
            rg = GetComponent<Rigidbody2D>();
        else
            rg = gameObject.AddComponent<Rigidbody2D>();

        rg.gravityScale = 0;
        gameObject.layer = 10;
    }

}
