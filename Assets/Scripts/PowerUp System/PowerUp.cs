using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PowerUps;

namespace PowerUps
{ public class PowerUp : PickableItem
    {

        public PowerUpType type;
        // Use this for initialization
        public override void OnPick()
        {
            PowerUpManager.Instance.Activate(type);
            GetComponent<SFX>().PlaySound("Sound 1");
            DestroyGameObject();
        }
    }
}
