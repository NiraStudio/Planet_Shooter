using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Alpha.Localization;

public class LevelModeGamePlayUI : MonoBehaviour {
    #region Singleton
    public static LevelModeGamePlayUI Instance;
    void Awake()
    {
        Instance = this;
    }
    #endregion



    public LocalizedDynamicText WaveCounter,CoinCounter,ScoreText,TimeText;
    public Slider AmmoBar, HpBar;
    
    [SerializeField]
    Animator coinShockAnimator;

    LevelModeGameManager LM;
    CharacterHolder CH;
    Weapon weapon;
    string PersianText, EnglishText;

    bool ready;
	// Use this for initialization
	IEnumerator Start () {
        CH = CharacterHolder.Instance;
        yield return new WaitUntil(() => CH.weapon != null);
        weapon = CH.weapon;
        AmmoBar.maxValue = weapon.MaxAmmo;
        HpBar.maxValue = CH.MaxHp;
        LM = LevelModeGameManager.LPM;
        ready = true;
	}
	
	// Update is called once per frame
	void LateUpdate () {
        if (!ready)
            return;

        ScoreText.text = LM.Score.ToString() ;

        AmmoBar.maxValue = weapon.MaxAmmo;
        HpBar.maxValue = CH.MaxHp;

        AmmoBar.value = weapon.Ammo;
        HpBar.value = Mathf.Lerp(HpBar.value,CH._hp,0.1f);

        ChangeTimeText(LM.timeSpend);
        
       // CoinCounter.text = "X" + GPM.Coin;
        
	}

    public void WaveChange()
    {
        WaveCounter.gameObject.transform.parent.GetComponent<Animator>().SetTrigger("Change");
    }
    public void CoinShock()
    {
        coinShockAnimator.SetTrigger("Shock");
    }

    void ChangeTimeText(float time)
    {
        float tt = time;
        string r = "";
        r += ((int)tt / 3600).ToString() + ":";
        tt -= ((int)tt / 3600) * 3600;
        r += ((int)tt / 60).ToString("00") + ":";
        r += (tt % 60).ToString("00");
        TimeText.text = r;
    }
}
