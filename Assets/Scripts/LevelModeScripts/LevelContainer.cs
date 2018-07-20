using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelContainer : MonoBehaviour {
    public LevelData data;
    public TextMeshProUGUI levelName;

    GameManager gm;
	// Use this for initialization
	void Start () {
        gm = GameManager.Instance;
        levelName.text = data.LevelName;
	}
	


    public void OnClick()
    {
        gm.CurrentLevel = data;
       gm.LoadScene("Level_Mode");
    }
}
