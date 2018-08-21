using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(Currency))]
public class CurrencyEditor : PropertyDrawer
{

    SerializedProperty type;
    SerializedProperty amount;
    Rect t;
    int need;
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        //  base.OnGUI(position, property, label);

        type = property.FindPropertyRelative("type");
        amount = property.FindPropertyRelative("amount");
        t = position;
        t.width = t.width - need - 15;
        type.enumValueIndex = (int)((CurrencyType)EditorGUI.EnumPopup(t, "Currecy :", (CurrencyType)type.enumValueIndex));


        switch ((RewardType)type.enumValueIndex)
        {

            case RewardType.Coin:
                need = 150;
                t = position;
                t.x = t.width - need;
                t.width = need;
                amount.intValue = EditorGUI.IntField(t, amount.intValue);
                break;
            case RewardType.NiraCoin:
                need = 150;
                t = position;
                t.x = t.width - need;
                t.width = need;
                amount.intValue = EditorGUI.IntField(t,  amount.intValue);
                break;
            default:
                GUILayout.Label("There no currency kind like this");
                break;
        }




    }
}
