using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainBehavior : MonoBehaviour {
    public static LayerMask EnemyLayer = 1 << 10,GroundLayer=1<<8, characterLayer=1<<9;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void RestartScene()
    {
        SceneManager.LoadScene(1);
    }
    public void LoadScene(string Scene_Name)
    {
        SceneManager.LoadScene(Scene_Name);
    }
}
