using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayHUDController : MonoBehaviour {
    public Slider AmmoBar, HpBar;
    public CharacterHolder character;

    Weapon weapon;

    bool ready;
    // Use this for initialization
    IEnumerator Start () {
        yield return new WaitUntil(() => character.weapon != null);
        weapon = character.weapon;
        AmmoBar.maxValue = weapon.MaxAmmo;
        HpBar.maxValue = character.MaxHp;
        ready = true;
    }
	
	// Update is called once per frame
	void Update () {
        if (!ready)
            return;

        AmmoBar.maxValue = weapon.MaxAmmo;
        HpBar.maxValue = character.MaxHp;

        AmmoBar.value = weapon.Ammo;
        HpBar.value = Mathf.Lerp(HpBar.value, character._hp, 0.1f);
    }
}
