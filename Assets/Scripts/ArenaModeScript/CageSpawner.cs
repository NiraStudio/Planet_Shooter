using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CageSpawner : MonoBehaviour {
    public static CageSpawner Instance;

    public GameObject cage,enemy;
    public NumberRange distance;
    public Transform center;

    public Image RightPointer, LeftPointer;
    Transform currentCage;
    public NumberRange waitTime=new NumberRange(10f,12f);
    public NumberRange waitTimeCage=new NumberRange(10f,12f);
    float tr, wT, cageT,cageWT;

    ArenaModeGamePlayManager AM;
    // Use this for initialization
    IEnumerator Start () {
        Instance = this;
        AM = ArenaModeGamePlayManager.APM;
        yield return new WaitUntil(() => AM.gamePlayState == GamePlayState.Play);
        Spawn();
        SpawnEnemy();
    }

    // Update is called once per frame
    void Update () {
        if (AM.gamePlayState != GamePlayState.Play)
            return;

        tr += Time.deltaTime;
        if (tr >= wT)
            SpawnEnemy();

        cageT += Time.deltaTime;
        if (cageT >= cageWT)
            Spawn();

	}

    void SpawnEnemy()
    {
        for (int i = 0; i < Random.Range(7,10); i++)
        {
            float a = distance.RandomFloat;
            Vector2 t = (Random.insideUnitCircle * a) - (Vector2)center.position;
            t.Normalize();
            Instantiate(enemy, (Vector2)center.position + (t * a), Quaternion.identity);
        }
        

        tr = 0;
        wT = waitTime.RandomFloat;
    }
    public void Spawn()
    {

        float a = distance.RandomFloat;
        Vector2 t = (Random.insideUnitCircle * a) - (Vector2)center.position;
        t.Normalize();
       currentCage= Instantiate(cage,(Vector2) center.position + (t * a), Quaternion.identity).transform;
        cageWT = waitTimeCage.RandomFloat;
        cageT = 0;
    }
}
