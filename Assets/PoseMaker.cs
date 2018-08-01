using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class PoseMaker : MonoBehaviour {
    public Transform center,character;
    public float radius,ttt;
    public Vector2 test;
    Vector2 t,tt;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        t = transform.position-center.position;
        t.Normalize();
        transform.position = (Vector2)center.position + (t * radius);

        tt = center.transform.position - transform.position;



        tt.Normalize();
        test = tt;

        tt = character.position - transform.position;
        ttt = Vector2.Dot(tt, transform.right);

    }
}
