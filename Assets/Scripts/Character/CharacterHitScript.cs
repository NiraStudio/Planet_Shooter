using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Anima2D;

public class CharacterHitScript : MainBehavior {

    Transform CH;
    public SpriteMeshInstance[] Objects;
    public GameObject hpUI;
	void Start()
    {
        CH = transform.GetChild(0);
    }

    public void Hited()
    {
        StartCoroutine(h());
    }
    IEnumerator h()
    {
        gameObject.layer = 12;
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < CH.transform.childCount; j++)
            {
                Objects[j].color = Color.clear; 
            }
            hpUI.SetActive(false);
            yield return new WaitForSeconds(0.2f);
            for (int j = 0; j < CH.transform.childCount; j++)
            {
                Objects[j].color = Color.white;
            }
            hpUI.SetActive(true);

            yield return new WaitForSeconds(0.2f);
        }
        gameObject.layer = 9;

    }
}
