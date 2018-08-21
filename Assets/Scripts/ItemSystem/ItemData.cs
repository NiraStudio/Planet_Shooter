using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ItemData : ScriptableObject {
    public string itemName;
    public int id;
    [Range(1,10)]
    public int limit;
    public GameObject prefab;
    public Vector2 instasiatePos;
    public Currency price;
}
