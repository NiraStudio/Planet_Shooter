using UnityEngine;
using UnityEditor;
using PowerUps;


[CustomPropertyDrawer(typeof(PowerUpByTime))]
public class PowerUpTimeEditor : PropertyDrawer
{

    SerializedProperty type;
    SerializedProperty time;
    Rect t;
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        //  base.OnGUI(position, property, label);

        type = property.FindPropertyRelative("type");
        time = property.FindPropertyRelative("time");
        t = position;
        t.width = t.width / 2;
        type.enumValueIndex = (int)((PowerUpType)EditorGUI.EnumPopup(t, "Type :", (PowerUpType)type.enumValueIndex));

        t.x = t.width;

        EditorGUI.PropertyField(t, time);




    }
}
