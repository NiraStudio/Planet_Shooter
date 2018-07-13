using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterHitScript : MainBehavior {

    Transform CH;
    public SpriteRenderer[] Objects;
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
            yield return new WaitForSeconds(0.2f);
            for (int j = 0; j < CH.transform.childCount; j++)
            {
                Objects[j].color = Color.white;
            }
            yield return new WaitForSeconds(0.2f);
        }
        gameObject.layer = 9;

    }
}
