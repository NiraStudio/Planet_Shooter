using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdditionalInfoUI : MonoBehaviour {

   /* public enum OpenType
    {
        WidthThenHeight,HeightThenWidth, HeightAndWidth
    }

    public OpenType openType;*/
    public Image background;
    public Vector2 bgOpenSize, bgCloseSize;
    public AdditionalInfoUI[] childs;
    bool moving,open;

    
	// Use this for initialization
	void Start () {
        background.gameObject.SetActive(false);
        print(background.rectTransform.sizeDelta);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void Action()
    {
        if (moving)
            return;

        moving = true;
        if (open)
        {
            Close();

        }
        else
        {
            Open();
        }
        open = !open;

    }

    public void Close()
    {
        closeChilds();
        StartCoroutine(HeightAndWidthOpen(bgCloseSize));
    }
    public void Open()
    {
        background.rectTransform.sizeDelta = bgCloseSize;
        background.gameObject.SetActive(true);

        Vector2 t = background.rectTransform.sizeDelta;
        t.x = bgOpenSize.x;
        StartCoroutine(HeightAndWidthOpen(bgOpenSize));
    }

    public void closeChilds()
    {
        foreach (var item in childs)
        {
            if(item.gameObject.activeInHierarchy)
            item.Close();
        }
    }

    IEnumerator WidthOpen(Vector2 t)
    {

        while (Mathf.Abs(background.rectTransform.sizeDelta.x-t.x)>20)
        {
            background.rectTransform.sizeDelta = Vector2.Lerp(background.rectTransform.sizeDelta, t, 0.2f);
            yield return null;
            print(Mathf.Abs(background.rectTransform.sizeDelta.x)+" "+t.x);
        }

        Vector2 j = background.rectTransform.sizeDelta;
        j.y = bgOpenSize.y;
        StartCoroutine(HeightOpen(j));


    }

    IEnumerator HeightOpen(Vector2 t)
    {

        while (Mathf.Abs(background.rectTransform.sizeDelta.y - t.y) > 20f)
        {
            background.rectTransform.sizeDelta = Vector2.Lerp(background.rectTransform.sizeDelta, t, 0.1f);
            yield return null;
            print(Mathf.Abs(background.rectTransform.sizeDelta.y) + " " + t.y);
        }
        moving = false;

    }

    IEnumerator HeightAndWidthOpen(Vector2 t)
    {

        while (Vector2 .Distance(background.rectTransform.sizeDelta, t) >20f)
        {
            background.rectTransform.sizeDelta = Vector2.Lerp(background.rectTransform.sizeDelta, t, 0.1f);
            yield return null;
        }
        moving = false;
        if(t==bgCloseSize)
            background.gameObject.SetActive(false);

    }


}
