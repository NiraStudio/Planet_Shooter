using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Items
{
    [RequireComponent(typeof(GroundLooker))]
    public class BowlingBallItem : PickableItem
    {
        [Header("Bowling Ball")]
        public GameObject explodeParticel;
        public float speed, time;
        Rigidbody2D rg;
        [HideInInspector]
        public int dir = 1;
        [HideInInspector]
        public float dmg;

        Transform shape;
        
        private void Start()
        {
            rg = GetComponent<Rigidbody2D>();
            shape = transform.GetChild(0);
        }

        public override void Update()
        {
            base.Update();
            if (time > 0)
            {
                time -= Time.deltaTime;
                if (time <= 0)
                    Die();
            }
            rg.velocity = transform.right * speed * dir;
            shape.Rotate(0, 0, speed*-dir);
        }

        public override void OnPick()
        {
            DetectedCollider.GetComponent<Ihitable>().OnHit(dmg,transform);
            Instantiate(explodeParticel, transform.position, transform.rotation);
        }

        void Die()
        {
            DestroyGameObject();
        }
        public void ChangeDirection(int d)
        {
            dir = d;
        }
    }
}