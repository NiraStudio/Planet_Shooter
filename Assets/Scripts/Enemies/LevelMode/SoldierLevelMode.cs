using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierLevelMode : LevelModeEnemy {

    public float waitTime;
    public GameObject Bullet;
    public Transform ShootPos;

    float t;
    GameObject b;
    public override void Update()
    {
        base.Update();
        if (t < waitTime)
        {
            t += Time.deltaTime;
            if (t >= waitTime)
                PlayAttackAnimation();
        }

    }

    public void PlayAttackAnimation()
    {
        anim.SetTrigger("Shoot");
        Stop(0.5f);
    }
    public void Shoot()
    {
        b = Instantiate(Bullet, ShootPos.position, Quaternion.identity);
        b.GetComponent<Bullet>().Release(direction, dmg);
    }
    public void WaitTimeReset()
    {
        t = 0;
    }
    public override void OnCharacterEnter()
    {
        characterCollider.GetComponent<Ihitable>().OnHit(dmg/2);

    }
}
