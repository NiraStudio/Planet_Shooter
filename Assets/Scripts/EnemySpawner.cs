using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {
    public GameObject Enemy;

    public Transform startPoint;
    GamePlayManager GPM;
    Transform ground;
	// Use this for initialization
	void Start () {
        ground = GameObject.FindWithTag("Ground").transform;
        GPM = GamePlayManager.GPM;
        StartCoroutine(spawn());

    }

    // Update is called once per frame
    void Update () {
		
	}
    IEnumerator spawn()
    {
        yield return new WaitUntil(() => GPM.gamePlayState == GamePlayState.Play);
         float a = Random.Range(-85, 85);
         a += 90;
         a = a * Mathf.Deg2Rad;
         Vector2 t = ground.position;
         t += new Vector2(Mathf.Cos(a), Mathf.Sin(a)) * Random.Range(5,8);
        Instantiate(Enemy, t, Quaternion.identity);
        yield return new WaitForSeconds(1);
        StartCoroutine(spawn());
    }
}
