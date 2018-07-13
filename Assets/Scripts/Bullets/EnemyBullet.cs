using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : Bullet ,Ihitable {
    public void OnDie()
    {
        Destroy(gameObject);
    }

    public void OnHeal(float Amount)
    {
    }

    public void OnHit(float dmg)
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
