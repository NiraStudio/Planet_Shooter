using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;



public class ArenaModeGamePlayManager : GamePlayManager {

    public static ArenaModeGamePlayManager APM;

    public GoldenFairyCollector PlayerCollector, OpponentCollector;
    public TextMeshProUGUI PlayerFairiesCount, OpponentFairiesCount, timeText;
    [SerializeField]
    CageSpawner cg;

    public float matchTime;

    public override void Start()
    {
        APM = this;
        base.Start();

        OpponentFairiesCount.text = "X" + OpponentCollector.fairies;
        PlayerFairiesCount.text = "X" + PlayerCollector.fairies;
        matchTime -= Time.deltaTime;
        if(matchTime<=0)
            
        ChangeTimeText(matchTime);
    }

    private void Update()
    {
        if (gamePlayState != GamePlayState.Play)
            return;
        OpponentFairiesCount.text = "X"+OpponentCollector.fairies;
        PlayerFairiesCount.text = "X"+ PlayerCollector.fairies;
        matchTime -= Time.deltaTime;
        if (matchTime <= 0)
            GameFinished();
        ChangeTimeText(matchTime);
    }

    void ChangeTimeText(float time)
    {
        float tt = time;
        string r = "";
        r += ((int)tt / 3600).ToString() + ":";
        tt -= ((int)tt / 3600) * 3600;
        r += ((int)tt / 60).ToString("00") + ":";
        r += (tt % 60).ToString("00");
        timeText.text = r;
    }

    public override void OnGameLost()
    {
        
    }

    public void GameFinished()
    {
        StartCoroutine(gameFinished());
    }

    IEnumerator gameFinished()
    {
        gamePlayState = GamePlayState.Finished;
        yield return new WaitForSeconds(3);
        LoadScene("Main_Menu");
    }
}
