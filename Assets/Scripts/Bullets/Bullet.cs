using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(GroundLooker))]
[RequireComponent(typeof(CapsuleCollider2D))]

public class Bullet : MonoBehaviour {
    public float speed;
    public LayerMask DetectionLayer;
    public Vector2 OverLapBoxSize;

    public float lifeTime;

    protected bool release;
    protected float dmg = .5f;
    protected int direction=1;
    protected Rigidbody2D rg;
    protected Collider2D detect;
	// Use this for initialization
	protected virtual void Start () {
        rg= GetComponent<Rigidbody2D>();
        Destroy(gameObject,lifeTime);
	}
	
	// Update is called once per frame
	protected virtual void Update () {
        if (!release)
            return;
        rg.velocity = transform.right * speed*direction;
        detect = Physics2D.OverlapBox(transform.position, OverLapBoxSize, 0, DetectionLayer);
        if (detect)
            OnTargetHit();
    }

    public void Release(int direction,float dmg)
    {
       this. direction = direction;
        this.dmg = dmg;
        release = true;
    }
    
    public virtual void OnTargetHit()
    {
        detect.GetComponent<Ihitable>().OnHit(dmg);
        Destroy(gameObject);

    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;

        Gizmos.DrawWireCube(transform.position, OverLapBoxSize);
        //Gizmos.DrawWireCube(transform.position, overlapBoxSize);

    }

    private void Reset()
    {
        GetComponent<Rigidbody2D>().gravityScale = 0;
        gameObject.layer = 11;
    }

}
