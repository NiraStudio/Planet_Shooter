using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalOneShootGun : Weapon {

    bool generating;
    Bullet b;
    float t;

    Vector2[] p = new Vector2[] { new Vector2(0, 1), new Vector2(0, -1), new Vector2(1, 0), new Vector2(-1, 0) };
    protected override void OnTouchBegin()
    {
       if(Ammo>=1)
        {
            // StartCoroutine(Shoot());
            Shoot();
            Ammo -= 1;
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

    }

    protected override void AmmoRegenerationMethod()
    {
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
        else
        {
            if (Ammo < 1)
                generating = true;
        }
   

    }

    IEnumerator Generate()
    {
        while (Ammo<MaxAmmo)
        {
            print("Generating");
            Ammo += RegenerateSpeed;
            yield return new WaitForSeconds(1);
        }
        Ammo = MaxAmmo;
        generating = false;
    }
}
