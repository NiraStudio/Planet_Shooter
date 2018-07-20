using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;

[CustomEditor(typeof(LevelData))]
public class LevelDataEditor : Editor {

    LevelData d;

    ReorderableList EnemyList;

    SerializedObject o;
    int addAmount=0;

    bool makeRandomLevel;
    private void OnEnable()
    {
        //Create the list
        EnemyList = new ReorderableList(serializedObject,
            serializedObject.FindProperty("enemies"),
            true, true, true, true);

        EnemyList.drawHeaderCallback = (Rect rect) => {
            EditorGUI.LabelField(rect, "Enemy Order");
        };




        //GUI Changes
        EnemyList.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) => {
            var element = EnemyList.serializedProperty.GetArrayElementAtIndex(index);
            rect.y += 2;
            
            EditorGUI.PropertyField(
                new Rect(rect.x, rect.y, rect.width / 5, EditorGUIUtility.singleLineHeight),
                element.FindPropertyRelative("Enemy"),
                GUIContent.none);
            EditorGUI.PropertyField(
                new Rect(rect.x + (rect.width / 5), rect.y, rect.width / 5, EditorGUIUtility.singleLineHeight),
                element.FindPropertyRelative("Time"),
                GUIContent.none);

            EditorGUI.PropertyField(
                new Rect(rect.x + (rect.width / 5)*2, rect.y, rect.width / 5, EditorGUIUtility.singleLineHeight),
                element.FindPropertyRelative("SpawnPos"),
                GUIContent.none);

            EditorGUI.PropertyField(
               new Rect(rect.x + (rect.width / 5) * 3, rect.y, rect.width / 5, EditorGUIUtility.singleLineHeight),
               element.FindPropertyRelative("dir"),
               GUIContent.none);

            if ( EditorGUI.DropdownButton(new Rect(rect.x + (rect.width / 5) * 4, rect.y, rect.width / 5, EditorGUIUtility.singleLineHeight),new GUIContent("Set Pos"),FocusType.Keyboard))
            {
                if (Selection.activeGameObject!=null)
                element.FindPropertyRelative("SpawnPos").vector2Value = Selection.activeGameObject.transform.position;
            }
            serializedObject.ApplyModifiedProperties();
        };


       /* EnemyList.onAddDropdownCallback = (Rect buttonRect, ReorderableList l) => {
            var menu = new GenericMenu();

            menu.AddItem(new GUIContent("New Enemy"), false, AddEnemy, new EnemyByTime());

            menu.ShowAsContext();
        };*/

    }

    private void AddEnemy(object target)
    {
        //Add a normal empty field
        var index = EnemyList.serializedProperty.arraySize;
        EnemyList.serializedProperty.arraySize++;
        //Apply changes
        serializedObject.ApplyModifiedProperties();
    }

    public override void OnInspectorGUI()
    {
        d = (LevelData)target;


        GUILayout.BeginVertical();

        GUILayout.Label("Level Name");
        d.LevelName = GUILayout.TextField(d.LevelName);

        d.LevelMulti = EditorGUILayout.FloatField("Level Multi", d.LevelMulti);
        GUILayout.Space(20);

        serializedObject.Update();
        EnemyList.DoLayoutList();

        addAmount = EditorGUILayout.IntField("Amount",addAmount);

       if( GUILayout.Button("Add " + addAmount + (addAmount>1? " Enemies" : " Enemy")))
        {
            for (int i = 0; i < addAmount; i++)
            {
                d.enemies.Add(new EnemyByTime());
            }
            addAmount = 0;
        }

        GUILayout.EndVertical();

        SerializedProperty p = serializedObject.FindProperty("winRewards");

        EditorGUILayout.PropertyField(p, true);
        serializedObject.ApplyModifiedProperties();

    }
}
