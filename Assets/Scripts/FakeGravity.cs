using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeGravity : MonoBehaviour {
    public Transform GravityPoint;
    public bool Allow;

    Rigidbody2D rg;
    Vector2 t;
	// Use this for initialization
	void Start () {
        rg = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		if(Allow)
        {
            t = transform.position - GravityPoint.position;
            t.Normalize();

            rg.AddForce(t*Physics2D.gravity.y, ForceMode2D.Force);
        }
	}
}
