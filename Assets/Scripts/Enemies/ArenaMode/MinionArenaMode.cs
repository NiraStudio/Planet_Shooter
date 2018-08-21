using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionArenaMode : ArenaModeEnemy {

    public override void Start()
    {
        base.Start();
        while (direction==0)
        {
            direction = Random.Range(-1, 2);
        }
    }
    public override void Update()
    {
        if (GPM.gamePlayState != GamePlayState.Play)
        {
            rg.velocity = Vector2.zero;
            return;
        }

        if (dazedTime > 0)
            dazedTime -= Time.deltaTime;
        if (stopTime > 0)
            stopTime -= Time.deltaTime;

        ground = Physics2D.Raycast(transform.position, transform.up * -1, GroundSizeDetection, GroundLayer);

        #region MoveAllow
        MoveAllow = (stopTime > 0 ? 0 : 1) * (dazedTime > 0 ? 0 : 1);

        #endregion

        if (j > 0)
            j -= Time.deltaTime;
        else if (ground)
            rg.velocity = transform.right * MoveAllow * direction * speed;

        #region Flip
        if (right && direction == -1)
            Flip();
        else if (!right && direction == 1)
            Flip();

        #endregion




        HpBar.value = hitPoint;
        anim.SetBool("Move", ground);
        OnUpdate();
    }

    public override void OnDie(Transform Killer)
    {
        //  Instantiate(AmmoPack, transform.position, Quaternion.identity).SendMessage("ChangeAmmo", ammoAmount);//Ammo
        Killer.GetComponent<CharacterHolder>().GetEnergy(ammoAmount);
        Instantiate(DieParticel, transform.position, transform.localRotation);//Die Particle
        Destroy(gameObject);
    }
}
