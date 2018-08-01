using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour {

    public Transform target;
    Rigidbody2D rg;
    Vector2 t;
    bool move;
	// Use this for initialization
	IEnumerator Start () {
        target = GameObject.FindWithTag("Ground").transform;
        rg = GetComponent<Rigidbody2D>();


        var dir = target.transform.position - transform.position;
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        angle += 90;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        transform.rotation *= Quaternion.Euler(0, 0, Random.Range(-90f, 90f));

        rg.AddForce(transform.up*5, ForceMode2D.Impulse);
        yield return new WaitForSeconds(.5f);
        rg.velocity = Vector2.zero;
        t = target.position - transform.position;
        t.Normalize();
        rg.AddForce(t * 30, ForceMode2D.Impulse);

    }
	
	// Update is called once per frame
	void Update () {
		
            if (Vector2.Distance(transform.position, target.position) < 0.5)
            {
                Die();
            }
	}
    void Die()
    {
        LevelModeGamePlayUI.Instance.CoinShock();
        Destroy(gameObject);
    }
}
