using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketLevelMode : LevelModeEnemy {

    public override void Start()
    {
        base.Start();
        rg.AddForce(transform.up * speed, ForceMode2D.Impulse);
    }

    public override void Update()
    {
        rg.AddForce(transform.up * speed, ForceMode2D.Force);


        ground = Physics2D.Raycast(transform.position, transform.up * -1, GroundSizeDetection, GroundLayer);

        characterCollider = Physics2D.OverlapCircle(transform.position, CharacterSizeDetection, characterLayer);
        if (characterCollider)
        {
            OnCharacterEnter();

        }

        if (ground)
            OnDie(null);
    }

    public override void OnDie(Transform Killer)
    {
        //explode
        Instantiate(DieParticel, transform.position, transform.rotation*=Quaternion.Euler(0,0,180));
        Destroy(gameObject);
    }
    public override void OnCharacterEnter()
    {
        base.OnCharacterEnter();
        OnDie(null);
    }


}
