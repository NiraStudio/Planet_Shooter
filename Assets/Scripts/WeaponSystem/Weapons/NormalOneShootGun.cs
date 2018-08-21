using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalOneShootGun : Weapon {

    bool generating;
    Bullet b;
    float t,spendTime;



    public override void OnTouchBegin()
    {
       if(Ammo>=1)
        {
            // StartCoroutine(Shoot());
            Shoot();
        }
        else
        {
            sfx.PlaySound("Empty");
        }
    }
    public override void Shoot()
    {
        anim.SetTrigger("Shoot");
        b = Instantiate(Bullet, shootPos.position, Quaternion.identity).GetComponent<Bullet>();
        b.Release(Direction, Damage,Shooter.transform);
        sfx.PlaySound("Shot");
        spendTime = 0;
        Ammo -= 1;
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
