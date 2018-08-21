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
        if (LM.gamePlayState != GamePlayState.Play)
            return;
        if (t < waitTime)
        {
            t += Time.deltaTime;
            if (t >= waitTime)
              StartCoroutine(  PlayAttackAnimation());
        }

    }

    public IEnumerator PlayAttackAnimation()
    {
        anim.SetTrigger("Shoot");
        Stop(.8f);
        yield return new WaitForSeconds(0.5f);
        Shoot();
        

    }
    public void Shoot()
    {
        b = Instantiate(Bullet, ShootPos.position, Quaternion.identity);
        b.GetComponent<Bullet>().Release(direction, dmg,gameObject.transform);
        if (Random.Range(0, 100) <= 30)
            Turn();
    }
    public void WaitTimeReset()
    {
        t = 0;
    }
    public override void OnCharacterEnter()
    {
        characterCollider.GetComponent<Ihitable>().OnHit(dmg/2,transform);

    }
}
