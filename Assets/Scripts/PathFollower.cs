using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PathFollower : MonoBehaviour {
    public Transform target;
    public float speed;
    public UnityEvent reachAction;
    Vector2 t;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Vector2.Distance(transform.position, target.position) > 0.3f)
            transform.position = Vector2.Lerp(transform.position, target.position, 0.05f);
        else
            reachAction.Invoke();
	}
}
