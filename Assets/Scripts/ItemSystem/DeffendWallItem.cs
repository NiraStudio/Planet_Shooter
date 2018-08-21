using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Items
{
    public class DeffendWallItem : PickableItem
    {
        public int Hp;

        Collider2D tt;
        // Use this for initialization

        public override void OnPick()
        {
            if (tt == DetectedCollider)
                return;
            DetectedCollider.GetComponent<Enemy>().Turn();
            Hp--;
            if (Hp <= 0)
                Die();
            tt = DetectedCollider;
        }
        // Update is called once per frame

        void Die()
        {
            DestroyGameObject();   
        }

       
    }
}
