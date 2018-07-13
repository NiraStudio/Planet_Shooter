using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour {
    public Reward r;
    public PathFollower p;
    public Transform target;
	// Use this for initialization
	void Start () {
        p.target = target;
        p.reachAction.AddListener(() =>
        {
            print("Hello");
            Destroy(p.gameObject);
            Destroy(gameObject);
        });
	}
	
	// Update is called once per frame
	void Update () {
       
	}
}
