using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterHolderLevelMode : CharacterHolder {


    protected override void Start()
    {
        sk = SM.SkinByID(GM.CurrentSkin);
        weaponData = GM.GetCurrentWeapon();
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
        if (Application.isEditor)
        {

            if (Input.GetMouseButtonDown(1))
                ChangeDirection();

            if (Input.GetKeyDown(KeyCode.H))
                Jump();
        }
    }

}
