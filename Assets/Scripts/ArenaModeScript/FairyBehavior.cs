using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FairyBehavior : MonoBehaviour {
    public float InCageArea, ReleaseArea;

    Vector2 movePos;
    public Transform center;
    public bool release;
    float t,wT;
    NumberRange a=new NumberRange(0.3f,0.7f);
    // Use this for initialization
    void Start () {
        changeMovePos();

	}

    // Update is called once per frame
    void Update()
    {
        if ((Vector2)transform.position != (Vector2)center.position + movePos)
        {
            transform.position = Vector2.Lerp(transform.position, (Vector2)center.position + movePos,  Time.deltaTime*3);
        }

        if (t < 1)
        {
            t += Time.deltaTime;
            if (t >= 1)
                changeMovePos();
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(center.position, InCageArea);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(center.position, ReleaseArea);
    }
    public void changeMovePos()
    {
        movePos = Random.insideUnitCircle * (release ? ReleaseArea : InCageArea);
        t = 0;
        wT = a.RandomFloat;
    }
}
