using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuScript : MainBehavior {

    [SerializeField] Text damageText, HpText, AmmoText,coinText;
    [SerializeField] Button damageBtn, HpBtn, AmmoBtn;
    int damage, Hp, Ammo;
    public LevelData level;
    GameManager gm;
    private void Start()
    {
        gm = GameManager.Instance;
       
    }

    private void Update()
    {
        damageText.text = level.LevelName;
    }
    public void CHnage()
    {
        level.LevelName = Random.Range(1000, 3000).ToString();
    }

}
