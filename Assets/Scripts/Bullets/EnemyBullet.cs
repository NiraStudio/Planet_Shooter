using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : Bullet ,Ihitable {
    public void OnDie()
    {
        Destroy(gameObject);
    }

    public void OnDie(Transform Killer)
    {
        throw new System.NotImplementedException();
    }

    public void OnHeal(float Amount,Transform Healer)
    {
    }

    public void OnHit(float dmg, Transform Hiter)
    {
        OnDie();
    }

    public override void OnTargetHit()
    {
        base.OnTargetHit();
    }


    private void Reset()
    {
        GetComponent<Rigidbody2D>().gravityScale = 0;
        gameObject.layer = 14;
    }
}
