using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SFX))]
public class LevelModeEnemy : Enemy {



    public GameObject AmmoPack, DieParticel;
    public int ammoAmount;

    protected SFX sfx;
    LevelModeGameManager LM;

    float LevelMulti;
    public override void Start()
    {
        base.Start();
        LM = LevelModeGameManager.instance;
        LevelMulti = LM.LevelMulti;
        hitPoint *= LevelMulti;
        dmg *= LevelMulti;
        HpBar.maxValue = hitPoint;
        sfx = GetComponent<SFX>();
    }
    public override void OnDie()
    {
        Instantiate(AmmoPack, transform.position, Quaternion.identity).SendMessage("ChangeAmmo", ammoAmount);
        Instantiate(DieParticel, transform.position, transform.localRotation);
        ComboManager.Instance.AddCombo(1);
        LM.AddScore(ComboManager.Instance.Combo*100*Data.dmg);
        Destroy(gameObject);
    }
}
