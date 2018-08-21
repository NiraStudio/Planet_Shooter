using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Items
{
    public class SpringItem : PickableItem
    {

        public override void OnPick()
        {
            DetectedCollider.GetComponent<CharacterHolder>().Jump();
            Destroy(gameObject);
        }
    }
}