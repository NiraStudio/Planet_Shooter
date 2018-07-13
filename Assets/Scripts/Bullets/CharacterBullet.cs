using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBullet : Bullet {
    public bool daze;
    public float dazeTime;

    public override void OnTargetHit()
    {
       
            if (daze)
                detect.SendMessage("Stun", dazeTime);
            base.OnTargetHit();

    }

    private void Reset()
    {

            
        GetComponent<Rigidbody2D>().gravityScale = 0;
        gameObject.layer = 11;
    }
}
