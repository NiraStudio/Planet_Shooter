using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundLooker : MonoBehaviour {
    public GameObject Ground;
    public float DChanger;
    // Use this for initialization
    private void Awake()
    {
        Ground = GameObject.FindWithTag("Ground");
        var dir = Ground.transform.position - transform.position;
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        angle += 90 + DChanger;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
    void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        var dir = Ground.transform.position - transform.position;
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        angle += 90+DChanger;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}
