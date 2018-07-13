using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundRotate : MonoBehaviour {
    public float speed;

    Quaternion tempRotation;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        tempRotation = transform.localRotation;
        tempRotation *= Quaternion.Euler(0, 0, -speed * Time.deltaTime);
        transform.localRotation = tempRotation;
	}
}
