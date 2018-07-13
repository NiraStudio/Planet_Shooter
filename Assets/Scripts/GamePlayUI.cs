using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Alpha.Localization;

public class GamePlayUI : MonoBehaviour {
    #region Singleton
    public static GamePlayUI Instance;
    void Awake()
    {
        Instance = this;
    }
    #endregion



    public LocalizedDynamicText WaveCounter,CoinCounter;
    public Slider AmmoBar, HpBar;
    [SerializeField]
    Animator coinShockAnimator;

    GamePlayManager GPM;
    CharacterHolder CH;
    Weapon weapon;
    string PersianText, EnglishText;


	// Use this for initialization
	IEnumerator Start () {
        GPM = GamePlayManager.Instance;
        CH = CharacterHolder.Instance;
        yield return new WaitUntil(() => CH.weapon != null);
        weapon = CH.weapon;
        AmmoBar.maxValue = weapon.MaxAmmo;
        HpBar.maxValue = CH.HP;
	}
	
	// Update is called once per frame
	void LateUpdate () {
        AmmoBar.maxValue = weapon.MaxAmmo;
        HpBar.maxValue = CH.HP;

        AmmoBar.value = weapon.Ammo;
        HpBar.value = Mathf.Lerp(HpBar.value,CH._hp,0.1f);

        
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
}
