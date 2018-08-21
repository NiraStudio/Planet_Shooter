using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PickableItem : MonoBehaviour {
    public LayerMask DetectedLayer;
    public Vector3 offSet;
    public float DetectSize;

    protected Collider2D DetectedCollider;
	// Use this for initialization
	
	
	// Update is called once per frame
	public virtual void Update () {
        DetectedCollider = Physics2D.OverlapCircle(transform.position+offSet, DetectSize, DetectedLayer);
        if (DetectedCollider)
        {
            OnPick();
        }
    }
    public virtual void OnPick()
    {

    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;

        Gizmos.DrawWireSphere(transform.position+offSet, DetectSize);
    }


    public void DestroyGameObject()
    {
        Destroy(gameObject);
    }
}
