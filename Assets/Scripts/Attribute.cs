using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeStage.AntiCheat.ObscuredTypes;

[System.Serializable]
public class Attribute
{
    public AttributeType type;
    public AttributeValueType valueType;
    public ObscuredFloat value;
}
public enum AttributeValueType
{
    Flat,Percent
}
public enum AttributeType
{
    Hp,Damage,AmmoRegenerate,AmmoAmount
}
