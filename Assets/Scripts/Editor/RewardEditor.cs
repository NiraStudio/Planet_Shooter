using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(Reward))]
public class RewardEditor : PropertyDrawer
{

    SerializedProperty icon;
    SerializedProperty type;
    SerializedProperty amount;
    SerializedProperty weaponID;
    SerializedProperty skinID;
    Rect t;
    int need;
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
      //  base.OnGUI(position, property, label);

        icon = property.FindPropertyRelative("icon");
        type = property.FindPropertyRelative("type");
        amount = property.FindPropertyRelative("amount");
        weaponID = property.FindPropertyRelative("weaponID");
        skinID = property.FindPropertyRelative("skinID");
         t = position;
        t.width =t.width- need-15;
       type.enumValueIndex=(int)((RewardType) EditorGUI.EnumPopup(t,"Type :",(RewardType) type.enumValueIndex));

         
         switch ((RewardType) type.enumValueIndex)
         {

             case RewardType.Coin:
                need = 100;
                 t= position;
                t.x = t.width - need;
                t.width = need;
                 amount.intValue=EditorGUI.IntField(t, amount.intValue);
                 break;
             case RewardType.XP:
                need = 100;
                t = position;
                t.x = t.width - need;
                t.width = need;
                amount.intValue = EditorGUI.IntField(t, amount.intValue);
                break;
             case RewardType.NiraCoin:
                need = 100;
                t = position;
                t.x = t.width - need;
                t.width = need;
                amount.intValue = EditorGUI.IntField(t, amount.intValue);
                break;
             case RewardType.Weapon:
                need = 100;
                t = position;
                t.x = t.width - need;
                t.width = need;
                weaponID.intValue = EditorGUI.IntField(t, weaponID.intValue);
                break;
             case RewardType.Skin:
                need = 100;
                t = position;
                t.x = t.width - need;
                t.width = need;
                skinID.intValue = EditorGUI.IntField(t, skinID.intValue);
                break;
             default:
                 GUILayout.Label("There no reward kind like this");
                 break;
         }
       



    }
}
