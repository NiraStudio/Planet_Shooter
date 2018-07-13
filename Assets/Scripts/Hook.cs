using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hook : MonoBehaviour {
    public float PushPower, PullPower;


    HookState state;
    GameObject Rock;
    LineRenderer line;
    Vector2 direction,temp;
    Rigidbody2D rg;
    float distance,disBetween=.2f;
    public bool hit;
	// Use this for initialization
	void Start () {
        line = GetComponent<LineRenderer>();
        rg = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        direction = transform.position - transform.parent.position;
        direction.Normalize();
        distance = Vector2.Distance(transform.parent.position, transform.position);

        switch (state)
        {
            case HookState.Relax:
                transform.position = transform.parent.position;
                break;
            case HookState.Push:
                break;
            case HookState.Pull:
                if(Rock)
                {
                    rg.velocity = direction * -1 * 100;
                }
                else
                {
                    rg.velocity = direction * -1 * 50;

                }
                if(distance<0.5f)
                {
                    rg.velocity = Vector2.zero;
                    state = HookState.Relax;
                }
                break;
            
        }
        
        line.positionCount = (int)(distance/disBetween);
        
        if (line.positionCount > 0)
        {
            line.SetPosition(0, transform.parent.position);
            for (int i = 1; i < line.positionCount - 1; i++)
            {
                temp =(Vector2) transform.parent.position + (direction * (disBetween * (i + 1)));
                line.SetPosition(i, temp);
            }
            line.SetPosition(line.positionCount-1, transform.position);
        }


    }
    void OnTriggerEnter2D(Collider2D target)
    {
        if (target.tag == "Ground")
        {
            rg.velocity = Vector2.zero;
            state = HookState.Pull;
        }
    }
    public void PushDown()
    {
        if (state == HookState.Relax)
        {
            rg.velocity = transform.up * -1 * 50;
            state = HookState.Push;
        }
    }



    public enum HookState
    {
        Relax,Pull,Push
    }
}
