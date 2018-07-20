using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class AssetCreator : MonoBehaviour {

    [MenuItem("AlphaTool/Data Creator/Create Enemy Data")]
    
    public static void CreateCharacterData()
    {
        ScriptableObjectUtility.CreateAsset<EnemyData>("EnemyData");
    }

    [MenuItem("AlphaTool/Data Creator/Create Level Data")]

    public static void CreateLevelData()
    {
        ScriptableObjectUtility.CreateAsset<LevelData>("LevelData");
    }


    









}
