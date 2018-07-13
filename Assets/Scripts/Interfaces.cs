using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Ihitable {
    void OnHit(float dmg) ;
    void OnHeal(float Amount);
    void OnDie();


}
