using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class PoseMaker : MonoBehaviour {
    public Transform center;
    public float radius;

    Vector2 t;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        t = transform.position-center.position;
        t.Normalize();
        transform.position = (Vector2)center.position + (t * radius);
    }
}
