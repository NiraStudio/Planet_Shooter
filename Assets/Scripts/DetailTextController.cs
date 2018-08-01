using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Alpha.Localization;

public class DetailTextController : MonoBehaviour {

    public LocalizedDynamicText t;

    private void Start()
    {
        t = GetComponent<LocalizedDynamicText>();
    }

    public void Repaint(string text,Color color)
    {
        t.text = text;
        t.textMesh.color = color;
    }
    public void Des()
    {
        Destroy(gameObject);
    }
}
