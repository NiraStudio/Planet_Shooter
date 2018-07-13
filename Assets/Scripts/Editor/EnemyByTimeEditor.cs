using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(EnemyByTime))]
public class EnemyByTimeEditor : PropertyDrawer {
    EnemyByTime t;

    float EnemyObjectPicketWidth = 200;

    SerializedProperty time;
    SerializedProperty enemy;
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        time = property.FindPropertyRelative("Time");
        enemy = property.FindPropertyRelative("Enemy");

        EditorGUI.ObjectField(new Rect(position.x, position.y, EnemyObjectPicketWidth, position.height), enemy, GUIContent.none);
        EditorGUI.Slider(new Rect(position.width-EnemyObjectPicketWidth, position.y, position.width- EnemyObjectPicketWidth, position.height), time,  0, 5,GUIContent.none);
    }
}
