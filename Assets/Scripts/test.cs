using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class test : MonoBehaviour {
    public Texture2D[] frames;
    int framesPerSecond = 10;

    public Arena.ItemAction A;
    private void Start()
    {
        print(gameObject.layer == 13);
    }
    void Update() {
        // index = index % frames.Length;
        // renderer.material.mainTexture = frames[index];
    }
}
