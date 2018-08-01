using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GamePlayManager : MainBehavior {
    public static GamePlayManager GPM;

    public GamePlayState gamePlayState;
    LevelModeGamePlayUI GPUI;
    protected GameManager gm;
    public List<GameObject> enemies = new List<GameObject>();
    string path;
    void Awake()
    {
        GPM = this;
    }
    // Use this for initialization
    public virtual void Start()
    {

        StartCoroutine(StartGame());
    }

    // Update is called once per frame
   

    

    public IEnumerator StartGame()
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
    }

    public virtual void OnGameWon()
    {
    }

    IEnumerator share(string path)
    {
        yield return new WaitForSeconds(0.52f);
        new NativeShare().AddFile(path).SetText("I Nailed it").Share();
    }


}

public enum GamePlayState
{
    GetReady,Play,Finished
}
