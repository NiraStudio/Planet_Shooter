using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalOneShootGun : Weapon {

    bool generating;
    Bullet b;
    float t,spendTime;

    Vector2[] p = new Vector2[] { new Vector2(0, 1), new Vector2(0, -1), new Vector2(1, 0), new Vector2(-1, 0) };
    protected override void OnTouchBegin()
    {
       if(Ammo>=1)
        {
            // StartCoroutine(Shoot());
            Shoot();
            Ammo -= 1;
        }
        else
        {
            sfx.PlaySound("Empty");
        }
    }
    void Shoot()
    {
        /* Vector2 t;
         for (int i = 0; i < 4; i++)
         {
             t = (Vector2) shootPos.position + (p[i] * 0.5f);
             b = Instantiate(Bullet, t, Quaternion.identity).GetComponent<Bullet>();
             b.ChangeDirection(Direction);
             b.ChangeDmg(Damage);
         }*/
        b = Instantiate(Bullet, shootPos.position, Quaternion.identity).GetComponent<Bullet>();
        b.Release(Direction, Damage);
        sfx.PlaySound("Shot");
        spendTime = 0;
    }

    protected override void AmmoRegenerationMethod()
    {
        spendTime += Time.deltaTime;

      generating=  spendTime > 0.5f? true : false;



        if (generating)
        {
            if (t < 1)
                t += Time.deltaTime;
            else
            {
                Ammo += RegenerateSpeed;
                t = 0;
                if (Ammo >= MaxAmmo)
                {
                    Ammo = MaxAmmo;
                    generating = false;
                }
            }

        }
        
   

    }

   
}
