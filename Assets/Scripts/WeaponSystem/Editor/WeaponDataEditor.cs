using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Attributes;

public class WeaponDataEditor : EditorWindow{

    public const string FOLDER_NAME = "DataBase";
    public const string FILE_NAME = "WeaponDataBase.asset";
    public const string FULL_PATH = @"Assets/" + FOLDER_NAME + "/" + FILE_NAME;

    public static Vector2 WindowSize= new Vector2(800, 500);
    public static Vector2 CreateAndEditButtonSize= new Vector2(100, 250);
    public static Vector2 IconButtonSize = new Vector2(100, 100);


    static Vector2 EditScrollPos,AttributeScroll;
    WeaponDataBase dataBase;
    WeaponData temp, EditTemp;
    Texture2D tempIcon,EditTexture;
    bool create=true,autoId=true;
    int SelectedCharacter=-1;
    [MenuItem("AlphaTool/WeaponSystem")]
    public static void InIt()
    {
        WeaponDataEditor window = EditorWindow.GetWindow<WeaponDataEditor>();
        window.minSize = WindowSize; window.maxSize = WindowSize;

        window.title = "Weapon System";
        window.Show();
    }


    void OnEnable()
    {
        dataBase = AssetDatabase.LoadAssetAtPath(FULL_PATH, typeof(WeaponDataBase)) as WeaponDataBase;

        if (dataBase == null)
        {
            if (!AssetDatabase.IsValidFolder(@"Assets/" + FOLDER_NAME))
                AssetDatabase.CreateFolder(@"Assets", FOLDER_NAME);

            dataBase = new WeaponDataBase();
            AssetDatabase.CreateAsset(dataBase, FULL_PATH);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
        temp=EditTemp = CreateInstance<WeaponData>();
        EditorUtility.SetDirty(temp); 
    }

    void OnGUI()
    {

        if (GUI.changed)
        {
            EditorUtility.SetDirty(temp);

        }
        GUILayout.BeginHorizontal("Box");

        //Create And Edit Button
            GUILayout.BeginVertical(GUILayout.Width(CreateAndEditButtonSize.x));
    
               if (GUILayout.Button("Create", GUILayout.Height(CreateAndEditButtonSize.y), GUILayout.Width(CreateAndEditButtonSize.x)))
                    create = true;
               else if (GUILayout.Button("Edit", GUILayout.Height(CreateAndEditButtonSize.y), GUILayout.Width(CreateAndEditButtonSize.x)))
                    create = false;

            GUILayout.EndVertical();


        //Create And Edit Part
        if (create)
            Create();
        else
            Edit();


        GUILayout.EndHorizontal();
    }

    void Create()
    {
        GUILayout.BeginVertical("Box",GUILayout.Width(500));


        GUILayout.BeginHorizontal();
        //-----IconButton--------

        if (temp.icon != null)
            tempIcon = temp.icon.texture;


        if (GUILayout.Button(tempIcon, GUILayout.Width(IconButtonSize.x), GUILayout.Height(IconButtonSize.y)))
        {
            EditorGUIUtility.ShowObjectPicker<Sprite>(null, true, null, 0);
        }
        string commend = Event.current.commandName;
        if (commend == "ObjectSelectorClosed")
        {
            temp.icon = (Sprite)EditorGUIUtility.GetObjectPickerObject();
        }
        //---------------------------

        #region Name,ID,Quality,Type&Prefab

        //-------Name,ID,Quality,Type------
        GUILayout.BeginVertical();
        GUILayout.Label("Weapon Name Code:");
        temp.weaponName = GUILayout.TextField(temp.weaponName, GUILayout.Width(100));

        GUILayout.BeginHorizontal();

        autoId = GUILayout.Toggle(autoId,"Auto ID Generate");

        if (!autoId)
        {
            GUILayout.Label("ID:");
            temp.id = EditorGUILayout.IntField(temp.id,GUILayout.Width(100));
        }
        GUILayout.EndHorizontal();


        GUILayout.Label("Weapon Prefab:");
        temp.prefab = EditorGUILayout.ObjectField(temp.prefab, typeof(GameObject), false) as GameObject;

        

        GUILayout.BeginHorizontal();
        GUILayout.Label("Weapon Type:");
        temp.type = (WeaponType)EditorGUILayout.EnumPopup(temp.type, GUILayout.Width(100));
        GUILayout.Label("Weapon Quality:");
        temp.quality = (ItemQuality)EditorGUILayout.EnumPopup(temp.quality, GUILayout.Width(100));
        GUILayout.EndHorizontal();

        #endregion


        GUILayout.EndVertical();

        //----------------------------------

        GUILayout.EndHorizontal();

        #region Attributes
        //------attributes----------------

        AttributeScroll=GUILayout.BeginScrollView(AttributeScroll,"Box");
        for (int i = 0; i < temp.attributes.Count; i++)
        {
            GUILayout.BeginHorizontal("Box");
            GUILayout.Label("Type:");
            temp.attributes[i].type = (AttributeType)EditorGUILayout.EnumPopup(temp.attributes[i].type, GUILayout.Width(75));

            GUILayout.Label("Value Type:");
            temp.attributes[i].valueType = (AttributeValueType)EditorGUILayout.EnumPopup(temp.attributes[i].valueType, GUILayout.Width(75));

            GUILayout.Label("Value:");
            if (temp.attributes[i].valueType == AttributeValueType.Flat)
                temp.attributes[i].value = EditorGUILayout.FloatField(temp.attributes[i].value);
            else if (temp.attributes[i].valueType == AttributeValueType.Percent)
                temp.attributes[i].value = EditorGUILayout.Slider(temp.attributes[i].value, 1, 6);

            if (GUILayout.Button("X", GUILayout.Width(20), GUILayout.Height(20)))
            {
                temp.attributes.Remove(temp.attributes[i]);
            }

            GUILayout.EndHorizontal();
        }
        if (GUILayout.Button("Add Attribute",  GUILayout.Height(50)))
        {
            temp.attributes.Add(new Attribute());
        }

        GUILayout.EndScrollView();

        //-------------------------------

        #endregion

        if (GUILayout.Button("Create", GUILayout.Height(100)))
        {
            CreateData();
        }

        GUILayout.EndVertical();
    }



    void Edit()
    {
        GUILayout.BeginHorizontal();

        #region Edit

        if (EditTemp != null)
        {

            GUILayout.BeginVertical("Box", GUILayout.Width(500));


            GUILayout.BeginHorizontal();
            //-----IconButton--------

            if (EditTemp.icon != null)
                EditTexture = EditTemp.icon.texture;


            if (GUILayout.Button(EditTexture, GUILayout.Width(IconButtonSize.x), GUILayout.Height(IconButtonSize.y)))
            {
                EditorGUIUtility.ShowObjectPicker<Sprite>(null, true, null, 0);
            }
            string commend = Event.current.commandName;
            if (commend == "ObjectSelectorClosed")
            {
                EditTemp.icon = (Sprite)EditorGUIUtility.GetObjectPickerObject();
            }
            //---------------------------

            #region Name,ID,Quality,Type&Prefab

            //-------Name,ID,Quality,Type------
            GUILayout.BeginVertical();
            GUILayout.Label("Weapon Name Code:");
            EditTemp.weaponName = GUILayout.TextField(EditTemp.weaponName, GUILayout.Width(100));
            GUILayout.Label("ID:");
            EditTemp.id = EditorGUILayout.IntField(EditTemp.id);
            GUILayout.Label("Weapon Prefab:");
            EditTemp.prefab = EditorGUILayout.ObjectField(EditTemp.prefab, typeof(GameObject), false) as GameObject;



            GUILayout.BeginHorizontal();
            GUILayout.Label("Weapon Type:");
            temp.type = (WeaponType)EditorGUILayout.EnumPopup(temp.type, GUILayout.Width(100));
            GUILayout.Label("Weapon Quality:");
            temp.quality = (ItemQuality)EditorGUILayout.EnumPopup(temp.quality, GUILayout.Width(100));
            GUILayout.EndHorizontal();

            #endregion


            GUILayout.EndVertical();

            //----------------------------------

            GUILayout.EndHorizontal();

            #region Attributes
            //------attributes----------------

            AttributeScroll = GUILayout.BeginScrollView(AttributeScroll, "Box");
            for (int i = 0; i < EditTemp.attributes.Count; i++)
            {
                GUILayout.BeginHorizontal("Box");
                GUILayout.Label("Type:");
                EditTemp.attributes[i].type = (AttributeType)EditorGUILayout.EnumPopup(EditTemp.attributes[i].type, GUILayout.Width(75));

                GUILayout.Label("Value Type:");
                EditTemp.attributes[i].valueType = (AttributeValueType)EditorGUILayout.EnumPopup(EditTemp.attributes[i].valueType, GUILayout.Width(75));

                GUILayout.Label("Value:");
                if (EditTemp.attributes[i].valueType == AttributeValueType.Flat)
                    EditTemp.attributes[i].value = EditorGUILayout.FloatField(EditTemp.attributes[i].value);
                else if (EditTemp.attributes[i].valueType == AttributeValueType.Percent)
                    EditTemp.attributes[i].value = EditorGUILayout.Slider(EditTemp.attributes[i].value, 1, 6);

                if (GUILayout.Button("X", GUILayout.Width(15), GUILayout.Height(15)))
                {
                    EditTemp.attributes.Remove(EditTemp.attributes[i]);
                }

                GUILayout.EndHorizontal();
            }
            if (GUILayout.Button("Add Attribute", GUILayout.Height(50)))
            {
                EditTemp.attributes.Add(new Attribute());
            }

            GUILayout.EndScrollView();

            //-------------------------------

            #endregion



            GUILayout.EndVertical();


        }

        EditTemp.setDirty();

        #endregion


        #region Weapon Choose

        EditScrollPos = GUILayout.BeginScrollView(EditScrollPos);

        for (int i = 0; i < dataBase.Count; i++)
        {
            GUILayout.BeginVertical("Box");
            GUILayout.BeginHorizontal();
            if (dataBase.GiveByIndex(i).icon != null)
                EditTexture = dataBase.GiveByIndex(i).icon.texture;


            if (GUILayout.Button(EditTexture, GUILayout.Width(IconButtonSize.x), GUILayout.Height(IconButtonSize.y)))
            {
                EditTemp = dataBase.GiveByIndex(i);
            }
            if (GUILayout.Button("X", GUILayout.Width(20), GUILayout.Height(20)))
            {
                if (EditorUtility.DisplayDialog("Delete Weapon", "Are you sure you want to delete " + dataBase.GiveByIndex(i).weaponName + "?", "Yes", "No"))
                {
                    AssetDatabase.DeleteAsset(@"Assets/Data/WeaponData/" + dataBase.GiveByIndex(i).name + ".asset");
                    dataBase.RemoveWeapon(i);
                    return;

                }
            }

            GUILayout.EndHorizontal();
            GUILayout.Label(dataBase.GiveByIndex(i).weaponName);

            GUILayout.EndVertical();
        }

        GUILayout.EndScrollView();

        #endregion



        GUILayout.EndHorizontal();
    }
    /* void Create()
     {
         GUILayout.BeginVertical(GUILayout.ExpandWidth(true));



         GUILayout.BeginHorizontal();// Upgrade and Detail

         #region Detail

         GUILayout.BeginVertical("Box",GUILayout.Width(500));


         #region Icon_Name_Quality_Prefab_HP

         GUILayout.BeginHorizontal();

         //-----IconButton--------

         if (temp.icon != null)
             tempIcon = temp.icon.texture;


         if (GUILayout.Button(tempIcon, GUILayout.Width(IconButtonSize.x), GUILayout.Height(IconButtonSize.y)))
         {
             EditorGUIUtility.ShowObjectPicker<Sprite>(null, true, null, 0);
         }
         string commend = Event.current.commandName;
         if (commend == "ObjectSelectorClosed")
         {
             temp.icon = (Sprite)EditorGUIUtility.GetObjectPickerObject();
         }
         //---------------------------


         GUILayout.BeginVertical();


         GUILayout.BeginHorizontal();
         GUILayout.Label("Character Name Code:");
         temp.characterNameCode = GUILayout.TextField(temp.characterNameCode, GUILayout.Width(100));
         GUILayout.Label("Character Quality:");
         temp.Quality = (CharacterQuality)EditorGUILayout.EnumPopup(temp.Quality, GUILayout.Width(100));

         GUILayout.EndHorizontal();

         GUILayout.Space(25);

         GUILayout.BeginHorizontal();
         GUILayout.Label("Character Prefab:");
         temp.prefab = EditorGUILayout.ObjectField(temp.prefab, typeof(GameObject), false) as GameObject;
         GUILayout.Label("HitPoint:");
         temp.hp = EditorGUILayout.IntField( temp.hp);
         GUILayout.Label("Character Type:");
         temp.Type = (CharacterType)EditorGUILayout.EnumPopup(temp.Type, GUILayout.Width(100));
         GUILayout.EndHorizontal();


         GUILayout.EndVertical();



         GUILayout.EndHorizontal();





         #endregion

         GUILayout.Space(15);

         #region Range_Damage_AttackSpeed_Speed

         GUILayout.BeginHorizontal();

         GUILayout.Label("Attack Speed:");
         temp.attackSpeed = EditorGUILayout.FloatField( temp.attackSpeed);
         GUILayout.Space(15);

         GUILayout.Label("Speed:");
         temp.speed = EditorGUILayout.FloatField(temp.speed);
         GUILayout.Space(15);

         GUILayout.Label("Damage:");
         temp.damage = EditorGUILayout.FloatField(temp.damage);
         GUILayout.Space(15);

         GUILayout.Label("Range:");
         temp.range = EditorGUILayout.FloatField(temp.range);
         GUILayout.Space(15);



         GUILayout.EndHorizontal();

         #endregion

         GUILayout.Space(15);

         #region StartSpawn_IncreasePerLevel

         GUILayout.BeginHorizontal();

         GUILayout.Label("Start Spawn Count:");
         temp.startSpawnCount = EditorGUILayout.IntSlider(temp.startSpawnCount, 1, 6);
         GUILayout.Space(15);
         GUILayout.Label("Ratick Cost:");
         EditTemp.ratickCost = EditorGUILayout.IntField(EditTemp.ratickCost);

         GUILayout.EndHorizontal();

         #endregion

         GUILayout.EndVertical();


         #endregion

         #region Upgrade

         #endregion

         GUILayout.EndHorizontal();


         if (GUILayout.Button("Create Character",GUILayout.Height(75),GUILayout.ExpandWidth(true)))// Create Button
         {
             #region AddCharacter And Clean Temp
             CharacterData a = ScriptableObject.CreateInstance<CharacterData>();
             a.characterNameCode = temp.characterNameCode;
             a.prefab = temp.prefab;
             a.id = dataBase.IDGiver;
             a.icon = temp.icon;
             a.speed = temp.speed;
             a.Quality = temp.Quality;
             a.attackSpeed = temp.attackSpeed;
             a.hp = temp.hp;
             a.damage = temp.damage;
             a.startSpawnCount = temp.startSpawnCount;
             a.Code = temp.Code;
             a.range = temp.range;

             string path = @"Assets/Data/CharacterData/";
             if (!AssetDatabase.IsValidFolder(@"Assets/Data"))
                 AssetDatabase.CreateFolder("Assets", "Data");

             if (!AssetDatabase.IsValidFolder(@"Assets/Data/" + "CharacterData"))
                 AssetDatabase.CreateFolder(@"Assets/Data", "CharacterData");
             string b;
             if (a.characterNameCode != null)
                 b = temp.characterNameCode;
             else
                 b = "New character data";
             string assetPathAndName = AssetDatabase.GenerateUniqueAssetPath(path + b + ".asset");

             AssetDatabase.CreateAsset(a, assetPathAndName);

             AssetDatabase.SaveAssets();
             AssetDatabase.Refresh();
             dataBase.AddCharacter(a);
             temp = CreateInstance<CharacterData>();
             EditorUtility.SetDirty(temp);
             #endregion
         }


         GUILayout.EndVertical();
     }

     void Edit()
     {
         GUILayout.BeginVertical();
         #region EditPart
         if (EditTemp != null)
         {
             GUILayout.BeginVertical(GUILayout.ExpandWidth(true));



             GUILayout.BeginHorizontal();// Upgrade and Detail

             #region Detail

             GUILayout.BeginVertical("Box", GUILayout.Width(500));


             #region Icon_Name_Quality_Prefab_HP

             GUILayout.BeginHorizontal();

             //-----IconButton--------

             if (EditTemp.icon != null)
                 tempIcon = EditTemp.icon.texture;
             else
                 tempIcon = null;

             if (GUILayout.Button(tempIcon, GUILayout.Width(IconButtonSize.x), GUILayout.Height(IconButtonSize.y)))
             {
                 EditorGUIUtility.ShowObjectPicker<Sprite>(null, true, null, 0);
             }
             string commend = Event.current.commandName;
             if (commend == "ObjectSelectorClosed")
             {
                 EditTemp.icon = (Sprite)EditorGUIUtility.GetObjectPickerObject();
             }
             //---------------------------


             GUILayout.BeginVertical();


             GUILayout.BeginHorizontal();
             GUILayout.Label("Character Name Code:");
             EditTemp.characterNameCode=EditTemp.name = GUILayout.TextField(EditTemp.characterNameCode, GUILayout.Width(100));
             EditTemp.name = EditTemp.characterNameCode;
             GUILayout.Label("Character Quality:");
             EditTemp.Quality = (CharacterQuality)EditorGUILayout.EnumPopup(EditTemp.Quality, GUILayout.Width(100));
             GUILayout.EndHorizontal();

             GUILayout.Space(25);

             GUILayout.BeginHorizontal();
             GUILayout.Label("Character Prefab:");
             EditTemp.prefab = EditorGUILayout.ObjectField(EditTemp.prefab, typeof(GameObject), false) as GameObject;
             GUILayout.Label("HitPoint:");
             EditTemp.hp = EditorGUILayout.IntField(EditTemp.hp);
             GUILayout.Label("Character Type:");
             temp.Type = (CharacterType)EditorGUILayout.EnumPopup(temp.Type, GUILayout.Width(100));
             GUILayout.EndHorizontal();


             GUILayout.EndVertical();



             GUILayout.EndHorizontal();





             #endregion

             GUILayout.Space(15);

             #region Range_Damage_AttackSpeed_Speed

             GUILayout.BeginHorizontal();

             GUILayout.Label("Attack Speed:");
             EditTemp.attackSpeed = EditorGUILayout.FloatField(EditTemp.attackSpeed);
             GUILayout.Space(15);

             GUILayout.Label("Speed:");
             EditTemp.speed = EditorGUILayout.FloatField(EditTemp.speed);
             GUILayout.Space(15);

             GUILayout.Label("Damage:");
             EditTemp.damage = EditorGUILayout.FloatField(EditTemp.damage);
             GUILayout.Space(15);

             GUILayout.Label("Range:");
             EditTemp.range = EditorGUILayout.FloatField(EditTemp.range);
             GUILayout.Space(15);



             GUILayout.EndHorizontal();

             #endregion

             GUILayout.Space(15);

             #region StartSpawn_IncreasePerLevel

             GUILayout.BeginHorizontal();

             GUILayout.Label("Start Spawn Count:");
             EditTemp.startSpawnCount = EditorGUILayout.IntSlider(EditTemp.startSpawnCount, 1, 6);
             GUILayout.Space(15);

             GUILayout.Label("Ratick Cost:");
             EditTemp.ratickCost = EditorGUILayout.IntField(EditTemp.ratickCost);

             GUILayout.EndHorizontal();

             #endregion

             GUILayout.EndVertical();


             #endregion

             #region Upgrade

             #endregion

             GUILayout.EndHorizontal();




             GUILayout.EndVertical();
         }
         #endregion

        EditScrollPos= GUILayout.BeginScrollView(EditScrollPos,"Box",GUILayout.ExpandHeight(true),GUILayout.ExpandWidth(true));

         GUILayout.BeginHorizontal();
         GUILayout.Space(30);
         for (int i = 0; i < dataBase.Count; i++)
         {
             GUILayout.BeginVertical("Box", GUILayout.Width(IconButtonSize.x));

             #region Select_Delete_BTN
             GUILayout.BeginHorizontal();
             if (dataBase.GiveByIndex(i).icon != null)
                 EditTexture = dataBase.GiveByIndex(i).icon.texture;
             else
                 EditTexture = null;
             float m = i == SelectedCharacter ? 1.5f : 1;
             if (GUILayout.Button(EditTexture, GUILayout.Width(IconButtonSize.x *m), GUILayout.Height(IconButtonSize.y * m)))
             {
                 EditTemp = dataBase.GiveByIndex(i);
                 SelectedCharacter = i;
             }
             if (GUILayout.Button("X", GUILayout.Width(20), GUILayout.Height(20))){
                 if (EditorUtility.DisplayDialog("Delete Character", "Are you sure you want to delete " + dataBase.GiveByIndex(i).characterNameCode + "?", "Yes", "No"))
                 {
                     AssetDatabase.DeleteAsset(@"Assets/Data/CharacterData/" + dataBase.GiveByIndex(i).name + ".asset");
                     dataBase.RemoveCharacter(i);
                     return;

                 }
             }
             GUILayout.EndHorizontal();
             #endregion


             GUILayout.Label(dataBase.GiveByIndex(i).characterNameCode);
             GUILayout.EndVertical();


             GUILayout.Space(15);
         }
         GUILayout.Space(30);

         GUILayout.EndHorizontal();
         GUILayout.EndScrollView();

         GUILayout.EndVertical();
         dataBase.setDirty();
         EditTemp.setDirty();
     }*/


    void CreateData()
    {

        if (autoId)
            temp.id = dataBase.IDGiver;

        if (!dataBase.ValidID(temp.id))
        {
            EditorUtility.DisplayDialog("UnValid ID", "ID is UnValid ","OK");
            return;
        }
       

            WeaponData a = ScriptableObject.CreateInstance<WeaponData>();
            a.id = temp.id;
            a.weaponName = temp.weaponName;
            a.quality = temp.quality;
            a.type = temp.type;
            a.prefab = temp.prefab;
            a.attributes = temp.attributes;

            string path = @"Assets/Data/WeaponData/";
            if (!AssetDatabase.IsValidFolder(@"Assets/Data"))
                AssetDatabase.CreateFolder("Assets", "Data");

            if (!AssetDatabase.IsValidFolder(@"Assets/Data/" + "WeaponData"))
                AssetDatabase.CreateFolder(@"Assets/Data", "WeaponData");
            string b;
            if (a.weaponName != null)
                b = temp.weaponName;
            else
                b = "New Weapon data";
            string assetPathAndName = AssetDatabase.GenerateUniqueAssetPath(path + b + ".asset");

            AssetDatabase.CreateAsset(a, assetPathAndName);

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            dataBase.AddWeapon(a);
            temp = CreateInstance<WeaponData>();
            EditorUtility.SetDirty(temp);
        
    }
}
