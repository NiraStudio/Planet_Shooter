using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class SkinDataBaseReloader : EditorWindow {

    [MenuItem("AlphaTool/Skin System/Create Skin Data")]

    public static void CreateSkinData()
    {
        ScriptableObjectUtility.CreateAsset<Skin>("SkinData");
    }





    public const string FOLDER_NAME = "DataBase";
    public const string FILE_NAME = "SkinDataBase.asset";
    public const string FULL_PATH = @"Assets/" + FOLDER_NAME + "/" + FILE_NAME;
    public const string DATAS_PATH = @"Assets/" + "Data"+ "/SkinData";




    public static Vector2 WindowSize = new Vector2(800, 500);
    public static Vector2 ButtonSize = new Vector2(100, 100);

    SkinDataBase dataBase;
    static Vector2 EditScrollPos;
    WeaponData temp, EditTemp;
    Texture2D tempIcon;
    bool create = true, autoId = true;
    int SelectedCharacter = -1;

    [MenuItem("AlphaTool/Skin System/Skin Database")]
    public static void InIt()
    {
        SkinDataBaseReloader window = EditorWindow.GetWindow<SkinDataBaseReloader>();
        window.minSize = WindowSize; window.maxSize = WindowSize;

        window.title = "Skin System";
        window.Show();
    }

    private void OnEnable()
    {
       dataBase  = AssetDatabase.LoadAssetAtPath(FULL_PATH, typeof(SkinDataBase)) as SkinDataBase;

        if (dataBase == null)
        {
            if (!AssetDatabase.IsValidFolder(@"Assets/" + FOLDER_NAME))
                AssetDatabase.CreateFolder(@"Assets", FOLDER_NAME);

            dataBase = new SkinDataBase();
            AssetDatabase.CreateAsset(dataBase, FULL_PATH);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
    }



    private void OnGUI()
    {
        EditorGUILayout.BeginVertical("Box");

        EditorGUILayout.BeginHorizontal();

        if(GUILayout.Button("Make New Skin",GUILayout.Width(WindowSize.x/2), GUILayout.Height(WindowSize.y / 5)))
        {
            Debug.Log("Make");
            CreateSkinData();
            Reload();
        }
        if (GUILayout.Button("ReImport All Skin", GUILayout.Width(WindowSize.x / 2), GUILayout.Height(WindowSize.y / 5)))
        {
            Reload();
        }

        EditorGUILayout.EndHorizontal();


        EditScrollPos = EditorGUILayout.BeginScrollView(EditScrollPos, "Box");

         
        for (int i = 0; i < dataBase.Count; i++)
        {
            EditorGUILayout.BeginHorizontal("Box");

            if (dataBase.GiveByIndex(i).Icon != null)
                tempIcon = dataBase.GiveByIndex(i).Icon.texture;


            if (GUILayout.Button(tempIcon, GUILayout.Width(ButtonSize.x), GUILayout.Height(ButtonSize.y)))
            {
                Selection.activeObject = dataBase.GiveByIndex(i);
            }

            GUILayout.Label("Name: " + dataBase.GiveByIndex(i).skinName + " ID: " + dataBase.GiveByIndex(i).ID);

            if (GUILayout.Button("X", GUILayout.Width(20), GUILayout.Height(20)))
            {
                if (EditorUtility.DisplayDialog("Delete Skin", "Are you sure you want to delete " + dataBase.GiveByIndex(i).skinName + "?", "Yes", "No"))
                {
                    AssetDatabase.DeleteAsset(@"Assets/Data/SkinData/" + dataBase.GiveByIndex(i).name + ".asset");
                    dataBase.RemoveWeapon(dataBase.GiveByIndex(i));
                    return;

                }
            }

            EditorGUILayout.EndHorizontal();
        }


        EditorGUILayout.EndScrollView();




        EditorGUILayout.EndVertical();
    }



    public void Reload()
    {


        

        string[] t=  AssetDatabase.FindAssets(null, new[] { DATAS_PATH });
        Skin[] s = new Skin[t.Length];
        
        for (int i = 0; i < t.Length; i++)
        {
            s[i] = AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath(t[i]), typeof(Skin))as Skin;
        }
        foreach (var item in s)
        {
            if (!dataBase.DB.Contains(item))
                dataBase.AddSkin(item);
        }
    }


}
