using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelModeEnemy : Enemy {



    public GameObject AmmoPack, DieParticel;
    public int ammoAmount;


    LevelModeGameManager LM;

    float LevelMulti;
    public override void Start()
    {
        base.Start();
        LM = LevelModeGameManager.instance;
        LevelMulti = LM.LevelMulti;
        hitPoint *= LevelMulti;
        dmg *= LevelMulti;
    }
    public override void OnDie()
    {
        Instantiate(AmmoPack, transform.position, Quaternion.identity).SendMessage("ChangeAmmo", ammoAmount);
        Instantiate(DieParticel, transform.position, transform.localRotation);
        Destroy(gameObject);
    }
}
