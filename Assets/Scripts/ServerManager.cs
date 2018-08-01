using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;


public class ServerManager : MonoBehaviour {
    public static ServerManager Instance;

    public string serverAddres;

    string GamePass="A8610";

    private void Awake()
    {
        Instance = this;
    }


    // Use this for initialization
    void Start () {

        #region Example

        /*
        SendData<ResultClass>("",Json Data, (result) =>
        {
            

        });
        */
        #endregion



    }

    // Update is called once per frame
    void Update () {
		
	}

    public void SendData<T>(string Address,string data, ResultDelegate<T> result)
    {
        StartCoroutine(SendDataCoroutine<T>(Address, data, result));
    }
    IEnumerator SendDataCoroutine<T>(string Address, string data, ResultDelegate<T> result)
    {
        WWWForm f = new WWWForm();
        f.AddField("gamepass", GamePass);
        f.AddField("data", data);
        // Start a download of the given URL
        using (WWW www = new WWW(serverAddres+Address,f))
        {
            // Wait for download to complete
            yield return www;

            // assign texture
            result(JsonUtility.FromJson<T>(www.text));
        }
    }

    public void SendData<T>(string Address, ResultDelegate<T> result)
    {
        StartCoroutine(SendDataCoroutine<T>(Address,  result));
    }

    IEnumerator SendDataCoroutine<T>(string Address,  ResultDelegate<T> result)
    {

        // Start a download of the given URL
        print(serverAddres + Address);
        using (WWW www = new WWW(serverAddres + Address))
        {
            // Wait for download to complete
            yield return www;

            // assign texture
            result(JsonUtility.FromJson<T>(www.text));
        }
    }











    
}

[System.Serializable]
public class ResultClass
{
    public int code;
    public string data;
}

public enum ResultCode
{
    WorngGamePass = -1, WorngUsernameOrPassword = 0, OK = 1, NotOk = 2
}
