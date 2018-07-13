using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundLooker : MonoBehaviour {
    public GameObject Ground;
	// Use this for initialization
	void Start () {
        Ground = GameObject.FindWithTag("Ground");
	}
	
	// Update is called once per frame
	void Update () {
        var dir = Ground.transform.position - transform.position;
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        angle += 90;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}
