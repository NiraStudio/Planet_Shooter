using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class test : MonoBehaviour {
    public Texture2D[] frames;
    int framesPerSecond = 10;

    void Update() {
        int index =(int) Time.time * framesPerSecond;
       // index = index % frames.Length;
       // renderer.material.mainTexture = frames[index];
    }
}
