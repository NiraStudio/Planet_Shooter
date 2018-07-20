using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Anima2D;

[System.Serializable]
public class Skin : ScriptableObject
{
    
    [SerializeField]
    public string skinName;
    public Sprite Icon;
    public int ID;
    [HideInInspector]
    public List<SkinPart> skinParts;

   
}

