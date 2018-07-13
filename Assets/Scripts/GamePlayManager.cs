using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayManager : MainBehavior {
    public static GamePlayManager Instance;

    public GamePlayState gamePlayState;
    GamePlayUI GPUI;
    GameManager gm;
    void Awake()
    {
        Instance = this;
        gm = GameManager.Instance;
    }
    // Use this for initialization
    public virtual void Start()
    {
        GPUI = GetComponent<GamePlayUI>();
        StartCoroutine(StartGame());
    }

    // Update is called once per frame
   

    

    IEnumerator StartGame()
    {
        for (int i = 3; i > 0; i--)
        {
            Debug.Log(i.ToString());
            yield return new WaitForSeconds(1);
        }
        print("Start");
        yield return new WaitForSeconds(1);
        gamePlayState = GamePlayState.Play;

    }

    public virtual void OnGameLost()
    {
        LoadScene("Main_Menu");
    }

    public virtual void OnGameWon()
    {
       
    }

}

public enum GamePlayState
{
    GetReady,Play,Finished
}
