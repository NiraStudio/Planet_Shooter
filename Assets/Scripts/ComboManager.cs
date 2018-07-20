using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ComboManager : MonoBehaviour {
    public static ComboManager Instance;
    public TextMeshProUGUI comboText;
    public int Combo;
	// Use this for initialization
	void Start () {
        Instance = this;
	}
	
	// Update is called once per frame
	void Update () {
        comboText.gameObject.SetActive(Combo > 1);
        comboText.text = "X" + Combo;
	}
    public void AddCombo(int i)
    {
        Combo += i;
    }
    public void RemoveCombo()
    {
        Combo = 0;
    }
}
