using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CageScript : MonoBehaviour,Ihitable {
    public GameObject Hit_VFX;
    public float hp=3;
    public FairyBehavior fairy;
    public void OnDie(Transform Killer)
    {
        Killer.GetComponent<GoldenFairyCollector>().fairies++;
        fairy.center = Killer.GetComponent<GoldenFairyCollector>().fairyCenter;
        fairy.release = true;
        fairy.changeMovePos();
        fairy.transform.SetParent(null);
        Destroy(gameObject);
    }



    public void OnHeal(float Amount, Transform Healer)
    {
        throw new System.NotImplementedException();
    }

    public void OnHit(float dmg, Transform Hiter)
    {
        hp -= dmg;
        Instantiate(Hit_VFX, transform.position, transform.rotation);
        if(hp<=0)
        {
            hp = 0;
            OnDie(Hiter);
        }
    }
}
