using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Anima2D;
using Attributes;

[System.Serializable]
public class Skin : MonoBehaviour
{
    [SerializeField]
    public string skinName;
    public int ID;

    

    [HideInInspector]
    [SerializeField]
    public List<SkinPart> skinParts;


    public List<Attribute> attributes = new List<Attribute>();
}

