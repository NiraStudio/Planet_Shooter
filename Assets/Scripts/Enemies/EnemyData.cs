using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New_Enemy_Data", menuName = "Enemy Data")]

public class EnemyData : ScriptableObject {

    public string enemyName;
    public float speed;
    public float hitPoint;
    public float dmg;
}
