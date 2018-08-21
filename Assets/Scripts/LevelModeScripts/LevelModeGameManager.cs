using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LevelModeGameManager : GamePlayManager {

    #region Singleton
    public static LevelModeGameManager LPM;
   
    #endregion




    [System.Serializable]
    public class EnemyPoint
    {
        public float time;
        public GameObject point;
    }

    public LevelData levelData;
    public GameObject spawnPoint;
    public List<EnemyPoint> enemyPoints = new List<EnemyPoint>();
    LevelModeGamePlayUI GPUI;

    public float timeSpend=0;
    public float LevelMulti
    {
        get { return levelData.LevelHPMulti; }
    }
    float t;
    public int EnemyCoinDivide;
    public int coin;
    public float Score;
    // Use this for initialization
    public override void Start()
    {
        
        LPM = this;
        gm = GameManager.Instance;
        levelData = gm.CurrentLevel;
        InitializeData();
        base.Start();
    }

    // Update is called once per frame
    void Update () {
        if (gamePlayState != GamePlayState.Play)
            return;
        timeSpend += Time.deltaTime;

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
                   // CameraController.Instance.AddTarget(item.point);
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
        int a=0;
        GameObject g;
        EnemyPoint p;
        foreach (var item in levelData.enemies.ToArray())
        {
            p = new EnemyPoint();
            g = Instantiate(spawnPoint, item.SpawnPos, Quaternion.identity);g.GetComponent<EnemySpawnPoint>().Enemy = item.Enemy;
            p.time = item.Time ;
            a += item.Enemy.GetComponent<Enemy>().Data.CoindCapacity;
            p.point = g;
            g.GetComponent<EnemySpawnPoint>().direction = (int)item.dir;
            enemyPoints.Add(p);
        }

        EnemyCoinDivide = levelData.LevelGold / a;

    }
    private void Reset()
    {
        if (GetComponent<GamePlayManager>() )
            GPM = GetComponent<GamePlayManager>();
        else
            GPM = gameObject.AddComponent<GamePlayManager>();

    }
    public void AddScore(float score)
    {
        Score += score;
    }

    public override void OnGameWon()
    {
        base.OnGameWon();
        print(timeSpend);
        StartCoroutine(WonCoroutine());
        LoadScene("Main_Menu");
    }
    public override void OnGameLost()
    {
        base.OnGameLost();
        StartCoroutine(LostCoroutine());

    }

    IEnumerator LostCoroutine()
    {
        print("Time Spend: "+timeSpend);
        gamePlayState = GamePlayState.Finished;
        yield return new WaitForSeconds(4);
        LoadScene("Main_Menu");
    }

    IEnumerator WonCoroutine()
    {
        yield return null;
    }
}
