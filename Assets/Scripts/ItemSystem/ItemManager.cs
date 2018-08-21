using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Items;

public class ItemManager : MonoBehaviour {
    public static ItemManager Instance;
    public CharacterHolder characterTransform;
    public Transform itemPos;
    public ItemData tempdata;
	// Use this for initialization
	void Start () {
        Instance = this;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Space))
        {
            MakeItem(tempdata);
        }
	}


    void MakeItem(ItemData data)
    {
        itemPos.localPosition = data.instasiatePos;
        GameObject g= Instantiate(data.prefab, itemPos.position, Quaternion.identity);
        switch (data.id)
        {
            case 2:
                g.GetComponent<BowlingBallItem>().dir = characterTransform.direction;
                g.GetComponent<BowlingBallItem>().dmg = characterTransform.damage;
                break;
        }
    }
}
