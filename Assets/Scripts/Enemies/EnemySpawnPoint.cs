using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnPoint : MonoBehaviour {
    public GameObject Enemy;
    public int direction
    {
        get { return dir; }
        set { dir = value; }
    }
    int dir;
    GameObject g;
    private void Start()
    {
        g= Instantiate(Enemy, transform.position, Quaternion.identity);
        g.transform.SetParent(transform);
        g.SetActive(false);
        gameObject.SetActive(false);
    }
    // Use this for initialization
    public void Spawn()
    {
        g.SetActive(true);
        g.GetComponent<Enemy>().Direction = dir;
        g.transform.SetParent(null);
        Destroy(gameObject);
    }
    
}

