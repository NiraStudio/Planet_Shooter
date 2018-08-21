using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Arena;

public interface Ihitable {
    void OnHit(float dmg,Transform Hiter) ;
    void OnHeal(float Amount,Transform Healer);
    void OnDie(Transform Killer);


}
