using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class test : MonoBehaviour {
    public Reward r;
    public PathFollower p;
    public Transform target;
    public CharacterHolder ch;


    public float characterAngle
    {
        get
        {
            var dir = target.position - ch.transform.position;
            var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            return angle + 180;
        }
    }
    public float Angle
    {
        get
        {
            var dir = target.position - transform.position;
            var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            return angle + 180;
        }
    }
    // Use this for initialization
    void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        
        print("Angle :"+Angle+" CHaracter :"+characterAngle+"distnce :"+ (Angle -characterAngle));
	}
}
