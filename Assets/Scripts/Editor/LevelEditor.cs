using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;

public class LevelEditor : EditorWindow {

    public static Vector2 WindowSize=new Vector2(100,350);

    public LevelData d;

    static Vector2 scrollPos;

    int addAmount = 0;
    ReorderableList EnemyList;
    bool makeRandomLevel;
    GameObject g;
    SerializedObject o;
    [MenuItem("AlphaTool/LevelEditor")]
    public static void InIt()
    {
        LevelEditor window = EditorWindow.GetWindow<LevelEditor>();
        window.minSize = WindowSize; 

        window.title = "Level Editor";
        window.Show();
    }




    private void OnGUI()
    {
        d = EditorGUILayout.ObjectField((Object)d, typeof(LevelData),false) as LevelData;
        if (d == null)
            return;
        scrollPos = GUILayout.BeginScrollView(scrollPos);

        o = new SerializedObject(d);

        EnemyList = new ReorderableList(o,
            o.FindProperty("enemies"),
            true, true, true, true);

        EnemyList.drawHeaderCallback = (Rect rect) => {
            EditorGUI.LabelField(rect, "Enemy Order");
        };

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
                new Rect(rect.x + (rect.width / 5) * 2, rect.y, rect.width / 5, EditorGUIUtility.singleLineHeight),
                element.FindPropertyRelative("SpawnPos"),
                GUIContent.none);

            EditorGUI.PropertyField(
               new Rect(rect.x + (rect.width / 5) * 3, rect.y, rect.width / 5, EditorGUIUtility.singleLineHeight),
               element.FindPropertyRelative("dir"),
               GUIContent.none);

            if (EditorGUI.DropdownButton(new Rect(rect.x + (rect.width / 5) * 4, rect.y, rect.width / 5, EditorGUIUtility.singleLineHeight), new GUIContent("Set Pos"), FocusType.Keyboard))
            {
                if (Selection.activeGameObject != null)
                    element.FindPropertyRelative("SpawnPos").vector2Value = Selection.activeGameObject.transform.position;
            }
            o.ApplyModifiedProperties();
        };

        GUILayout.Label("Level Name");
        d.LevelName = GUILayout.TextField(d.LevelName);

        d.LevelHPMulti = EditorGUILayout.FloatField("Level Hp Multi", d.LevelHPMulti);

        d.LevelDmgMulti = EditorGUILayout.FloatField("Level Dmg Multi", d.LevelDmgMulti);
        GUILayout.Space(20);

        o.Update();
        EnemyList.DoLayoutList();

        addAmount = EditorGUILayout.IntField("Amount", addAmount);

        if (GUILayout.Button("Add " + addAmount + (addAmount > 1 ? " Enemies" : " Enemy")))
        {
            for (int i = 0; i < addAmount; i++)
            {
                d.enemies.Add(new EnemyByTime());
            }
            addAmount = 0;
        }

        #region ---- Attention ----- In case of Level Design Fails
        
       /* if (GUILayout.Button("Make Random Level "))
        {
            makeRandomLevel = !makeRandomLevel;
        }

        if (makeRandomLevel)
        {
            g = EditorGUILayout.ObjectField(g, typeof(GameObject),true)as GameObject;
            if (GUILayout.Button("Make A Level With " + (addAmount > 1 ? " Enemies" : " Enemy")))
            {
                float tt = 0;
                d.enemies = new List<EnemyByTime>();
                EnemyByTime ebt=new EnemyByTime();
                for (int i = 0; i < addAmount; i++)
                {
                    ebt = new EnemyByTime();
                    ebt.Enemy = g;
                    Vector2 t = (Random.insideUnitCircle*8.84f) - new Vector2(0, -3.21f);
                    t.Normalize();
                    ebt.SpawnPos = new Vector2(0, -3.21f) + (t * 8.84f);
                    tt += Random.Range(0f, 1f);
                    ebt.Time = tt;
                    while ((int)ebt.dir==0)
                    {
                        ebt.dir = (Direction)Random.Range(-1, 2);
                    }

                    d.enemies.Add(ebt);
                }
                addAmount = 0;
            }
        }
        */
        #endregion

        SerializedProperty p = o.FindProperty("winRewards");

        EditorGUILayout.PropertyField(p, true);
        o.ApplyModifiedProperties();
        d.setDirty();
        GUILayout.EndScrollView();
    }
}
