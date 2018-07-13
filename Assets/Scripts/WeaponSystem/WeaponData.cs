using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class WeaponData:ScriptableObject
{
    public string weaponName;
    public int id;
    public Sprite icon;
    public WeaponType type;
    public ItemQuality quality;
    public GameObject prefab;
    public List<Attribute> attributes = new List<Attribute>();

    public void setDirty()
    {
#if UNITY_EDITOR
        UnityEditor.EditorUtility.SetDirty(this);
#endif
    }

}
public enum WeaponType
{
    LightGun,HeavyGun,Melee,Throw
}


public enum ItemQuality
{
    Common,Rare,Epic,Legendary
}
