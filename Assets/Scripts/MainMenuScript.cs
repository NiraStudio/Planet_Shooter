using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuScript : MainBehavior {

    [SerializeField] Text damageText, HpText, AmmoText,coinText;
    [SerializeField] Button damageBtn, HpBtn, AmmoBtn;
    int damage, Hp, Ammo;

    GameManager gm;
    private void Start()
    {
        gm = GameManager.Instance;
       
    }
   
}
