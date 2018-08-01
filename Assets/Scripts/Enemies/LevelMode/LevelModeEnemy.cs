using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SFX))]
public class LevelModeEnemy : Enemy {



    public GameObject AmmoPack, DieParticel;
    public int ammoAmount;
    [HideInInspector]
    public int carryingCoin;
    protected SFX sfx;
    LevelModeGameManager LM;

    float LevelMulti;
    public override void Start()
    {
        base.Start();
        LM = LevelModeGameManager.LPM;
        LevelMulti = LM.LevelMulti;
        hitPoint *= LM.levelData.LevelHPMulti;
        dmg *= LM.levelData.LevelDmgMulti;
        HpBar.maxValue = hitPoint;
        sfx = GetComponent<SFX>();
        carryingCoin = Data.CoindCapacity * LM.EnemyCoinDivide;
    }
    public override void OnDie()
    {
        //CoinGetting
        Instantiate(AmmoPack, transform.position, Quaternion.identity).SendMessage("ChangeAmmo", ammoAmount);//Ammo
        Instantiate(DieParticel, transform.position, transform.localRotation);//Die Particle
        ComboManager.Instance.AddCombo(1);// Adding Combo(Need To Remove)
        LM.AddScore(ComboManager.Instance.Combo*100*Data.dmg);//Adding Score (need To Remove)
        Destroy(gameObject);
    }
}
