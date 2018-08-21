using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionLevelMode : LevelModeEnemy {

    float t;

    public override void Start()
    {
        base.Start();
        sfx.PlaySound("Sound " + Random.Range(1, 4));
    }

    public override void Update()
    {
        base.Update();


        anim.SetBool("Move", ground);
        /*if (t >= 5)
            if (Random.Range(0, 100) <= 15)
            {
                Turn();
                t = 0;
            }*/
    }
}
