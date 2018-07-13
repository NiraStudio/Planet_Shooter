using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LevelModeGameManager : GamePlayManager {

    #region Singleton
    public static LevelModeGameManager instance;
   
    #endregion




    [System.Serializable]
    public class EnemyPoint
    {
        public float time;
        public GameObject point;
    }

    public LevelData levelData;
    public GameObject spawnPoint;
    public List<GameObject> enemies = new List<GameObject>();
    public List<EnemyPoint> enemyPoints = new List<EnemyPoint>();
    public float LevelMulti
    {
        get { return levelData.LevelMulti; }
    }
    GamePlayManager GPM;
    float t;
    // Use this for initialization
    public override void Start()
    {
        instance = this;
        InitializeData();
        base.Start();
    }

    // Update is called once per frame
    void Update () {
        if (gamePlayState != GamePlayState.Play)
            return;

        ChechForNextEnemy();


        if (enemyPoints.Count == 0 && enemies.Count == 0)
        {
            OnGameWon();
        }
        
	}

    void ChechForNextEnemy()
    {
        t += Time.deltaTime;
        foreach (var item in enemyPoints.ToArray())
        {
            if (item.point == null)
            {
                enemyPoints.Remove(item);
                continue;
            }
            if (item.point.activeInHierarchy == false)
                if (item.time <= t)
                {
                    CameraController.Instance.AddTarget(item.point);
                    item.point.SetActive(true);
                    enemies.Add(item.point.transform.GetChild(1).gameObject);
                }
        }
        foreach (var item in enemies.ToArray())
        {
            if (item == null)
                enemies.Remove(item);
        }
    }

    void InitializeData()
    {
        float t=0;
        GameObject g;
        EnemyPoint p;
        foreach (var item in levelData.enemies.ToArray())
        {
            p = new EnemyPoint();
            t += item.Time;
            g = Instantiate(spawnPoint, item.SpawnPos, Quaternion.identity);g.GetComponent<EnemySpawnPoint>().Enemy = item.Enemy;
            p.time = t;
            p.point = g;
            g.GetComponent<EnemySpawnPoint>().direction = (int)item.dir;
            enemyPoints.Add(p);
        }

        

    }
    private void Reset()
    {
        if (GetComponent<GamePlayManager>() )
            GPM = GetComponent<GamePlayManager>();
        else
            GPM = gameObject.AddComponent<GamePlayManager>();

    }

    public override void OnGameWon()
    {
        LoadScene("Main_Menu");
    }
    public override void OnGameLost()
    {
        LoadScene("Main_Menu");
    }
}
