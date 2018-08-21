using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAIArenaMode : CharacterHolderArenaMode
{


    public int SkinId, WeaponID;
    public float detectRange, shootRange;
    public Transform Target;
    MoveDelegate AIDelegate;
    Vector2 t;
    public NumberRange detectRangeRandom=new NumberRange(1.2f,1.5f);
    public NumberRange waitToShoot;
    public NumberRange longDetectRange;
    public NumberRange FillToAttack;
    float tt,wT,temp;
    float neededAmmo;
    [SerializeField]
    bool empty;
    Collider2D c;
    GameObject[] g ;


    protected override void Start()
    {
        sk = SM.SkinByID(SkinId);
        weaponData = GM.GetWeapon(WeaponID);
        base.Start();
        WeaponPos.transform.GetChild(0).GetComponent<Weapon>().AI = true;
        detectRange = detectRangeRandom.RandomFloat;

        AIDelegate = (() =>
        {




            //checkForItem
            if (item)
            {
                //switch for item type
            }


            //go for Amoo if Empty==true
            if (empty)
            {
                print("Empty");
                c = Physics2D.OverlapCircle(transform.position, detectRange, 1 << 13);
                if (c == null)
                    c = Physics2D.OverlapCircle(transform.position, detectRange, 1 << 10);
                if (c != null)
                    Target = c.transform;
                if (weapon.Ammo >= neededAmmo)
                {
                    empty = false;
                    Target = null;
                    detectRange = detectRangeRandom.RandomFloat;
                }
                
            }
            else
            {
                print("Cage");

                if (Target == null)
                    g = GameObject.FindGameObjectsWithTag("Cage");
                if (g.Length==0)
                    Target =null;
                else
                {
                    float d = 100;
                    foreach (var item in g)
                    {
                        if(item)
                        if(Vector2.Distance(transform.position,item.transform.position)<d)
                        {
                            d = Vector2.Distance(transform.position, item.transform.position);
                            Target = item.transform;
                        }
                    }
                }
            }



            #region Go to target and shoot

            if (Target==null)
                return;


            tt = Vector2.Dot(t, transform.right);
            t = Target.transform.position - transform.position;


            if (Vector2.Distance(transform.position, Target.position) > detectRange)
            {

                if (tt > 0 && direction == -1)
                {
                    ChangeDirection();
                    detectRange = detectRangeRandom.RandomFloat;

                }

                else if (tt < 0 && direction == 1)
                {
                    ChangeDirection();
                    detectRange = detectRangeRandom.RandomFloat;

                }
            }
            if (Vector2.Distance(transform.position, Target.position) < shootRange &&Target.gameObject.layer!=13)
                if ((tt > 0 && direction == 1) || (tt < 0 && direction == -1))
                    Shoot();

            #endregion

        });
        Movement += AIDelegate;
        wT = waitToShoot.RandomFloat;

    }

    protected override void Update()
    {
        if(GPM.gamePlayState!=GamePlayState.Play)
        {
            rg.velocity = Vector2.zero;
            return;
        }

        base.Update();
       
        if (temp < wT)
            temp += Time.deltaTime;

    }

    void Shoot()
    {
        if (temp < wT)
            return;

        if (weapon.Empty)
        {
            empty = true;
            neededAmmo = weapon.MaxAmmo * (FillToAttack.RandomFloat / 100);
            detectRange = longDetectRange.RandomFloat;
            return;
        }
        weapon.Shoot();
        wT = waitToShoot.RandomFloat;
        temp = 0;

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectRange);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, shootRange);
    }
}
