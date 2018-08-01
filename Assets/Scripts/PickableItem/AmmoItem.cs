using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoItem : PickableItem {
    public float AmmoAmount;
    public GameObject vfx;


    public GameObject textDetail;
    bool picked;
    private void Start()
    {
        textDetail = Resources.Load<GameObject>("DetailText");
       // Die();
    }

    // Use this for initialization
    public override void OnPick()
    {
        if (picked)
            return;

        vfx.SetActive(true);
        Instantiate(textDetail, transform.position, transform.localRotation).GetComponent<DetailTextController>().Repaint("+1",Color.white);
        CharacterHolder.Instance.GetEnergy(AmmoAmount);
        GetComponent<SFX>().PlaySound("Sound 1");
        Die();
    }
    public void ChangeAmmo(float amount)
    {
        AmmoAmount = amount;
    }
    void Die()
    {
        picked = true;
        this.enabled = false;
        transform.GetChild(0).gameObject.SetActive(false);

        vfx.SetActive(true);
        Destroy(gameObject, 2);
    }

}
