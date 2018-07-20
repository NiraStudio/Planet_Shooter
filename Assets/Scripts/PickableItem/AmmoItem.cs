using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoItem : PickableItem {
    public float AmmoAmount;
    public GameObject TextDeatil,vfx;

    bool picked;
    private IEnumerator Start()
    {
        yield return new WaitForSeconds(5);
       // Die();
    }

    // Use this for initialization
    public override void OnPick()
    {
        if (picked)
            return;

        vfx.SetActive(true);
        TextDeatil.SetActive(true);
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
