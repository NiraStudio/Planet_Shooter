using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShareManager : MonoBehaviour {

    public static ShareManager Instance;

    public Text t;

    public string GifFile;

	// Use this for initialization
	void Start () {
        Instance = this;
	}
	
	// Update is called once per frame
	void Update () {
        t.text ="FPS: " +
            ""+((int)( 1 / Time.deltaTime)).ToString();
	}


    public void Share()
    {

       
    }


}
