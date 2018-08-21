using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using PowerUps;

public class PowerUpManager : MonoBehaviour {
    public static PowerUpManager Instance;

    private void Awake()
    {
        Instance = this;
    }
    public int ty;
    [Range(0, 100)]
    public float Chance;
    public NumberRange WaitTime;
    public float spawnDistance;
    public List<PowerUpDetails> PowerUpsDetails = new List<PowerUpDetails>();

    List<PowerUpState> powerUpsState = new List<PowerUpState>();
    float time;
    [SerializeField]
    Transform center;
    PowerUpDetails p;
    void Start() {
        var myEnumMemberCount = System.Enum.GetNames(typeof(PowerUpType)).Length;
       
        time = WaitTime.RandomInt;


        // Sorting by Percent >
        for (int i = 0; i < PowerUpsDetails.Count - 1; i++)
        {
            for (int j = i + 1; j < PowerUpsDetails.Count; j++)
            {
                if (PowerUpsDetails[i].percent < PowerUpsDetails[j].percent)
                {
                    p = PowerUpsDetails[i];
                    PowerUpsDetails[i] = PowerUpsDetails[j];
                    PowerUpsDetails[j] = p;
                }
            }
        }

        foreach (var item in PowerUpsDetails.ToArray())
        {
            item.UI.maxValue = item.time;
            item.UI.gameObject.SetActive(false);
        }


        foreach (var item in PowerUpsDetails.ToArray())
        {
            powerUpsState.Add(new PowerUpState(item.type, 0));

        }



    }

    // Update is called once per frame
    void Update() {
        for (int i = 0; i < powerUpsState.Count; i++)
        {
            if (powerUpsState[i].time > 0)
            {
                powerUpsState[i].time -= Time.deltaTime;
                PowerUpsDetails[i].UI.value = powerUpsState[i].time;
                if (powerUpsState[i].time <= 0)
                {
                    PowerUpsDetails[i].OnEnd.Invoke();
                    PowerUpsDetails[i].UI.gameObject.SetActive(false);
                }
            }
        }
       

        if (Input.GetKeyDown(KeyCode.Space)) {

            if (Random.Range(0, 101) < Chance)
                MakePowerUp();
            else
                print("Nothing For You Sorry!!");
        }

        if (time > 0)
            time -= Time.deltaTime;
        else
        {
            ty += 1;
            if (Random.Range(0, 101) < Chance)
                MakePowerUp();
            else
                print("Nothing For You Sorry!!");
            time = WaitTime.RandomInt;

        }

    }



    public float getPowerUpTime(PowerUpType type)
    {
        foreach (var item in PowerUpsDetails.ToArray())
        {
            if (item.type == type)
            {
                return item.time;
            }
        }

        return 0;
    }

    public void MakePowerUp()
    {

        
        float AllPercent=0;
        for (int i = 0; i < PowerUpsDetails.Count; i++)
        {
            AllPercent += PowerUpsDetails[i].percent;
        }
        float r = Random.Range(0, AllPercent);
        foreach (var item in PowerUpsDetails.ToArray())
        {
            if(r<=item.percent)
            {
                p = item;
                break;
            }
            r -= item.percent;
        }
        print("All : "+AllPercent + " R: " + r+" ty :"+ty+" Type :"+p.type);

        Vector2 t = Random.insideUnitCircle;
        t.Normalize();
        Instantiate(p.prefab, t * spawnDistance, Quaternion.identity);

    }


    public bool IsActive(PowerUpType type)
    {
        foreach (var item in powerUpsState.ToArray())
        {
            if (item.type == type)
            {
                return item.time > 0; ;
            }
        }

        return false;
    }

    public void Activate(PowerUpType type)
    {


        for (int i = 0; i < powerUpsState.Count; i++)
        {
            if (powerUpsState[i].type ==type)
            {
                powerUpsState[i].time = getPowerUpTime(type);
                PowerUpsDetails[i].OnStart.Invoke();
                PowerUpsDetails[i].UI.gameObject.SetActive(true);
                PowerUpsDetails[i].UI.value = PowerUpsDetails[i].UI.maxValue;
                print(powerUpsState[i].type + "   :_" + PowerUpsDetails[i].type);
            }
        }

       
    }


    private void Reset()
    {
        PowerUpsDetails = new List<PowerUpDetails>();
        var myEnumMemberCount = System.Enum.GetNames(typeof(PowerUpType)).Length;
        for (int i = 0; i < myEnumMemberCount; i++)
        {
            PowerUpsDetails.Add(new PowerUpDetails((PowerUpType)i, 0));
        }
    }
}


namespace PowerUps
{
    [System.Serializable]
    public class PowerUpDetails
    {
        public PowerUpType type;
        public float time;
        [Range(0,100)]
        public float percent;
        public Slider UI;
        public UnityEvent OnStart, OnEnd;

        public GameObject prefab;

        public PowerUpDetails(PowerUpType tt, float t)
        {
            type = tt;
            time = t;
        }

    }

    [System.Serializable]
    public class PowerUpState
    {
        public PowerUpType type;
        public float time;

        public PowerUpState(PowerUpType t,float tt)
        {
            type = t;
            time = tt;
        }
    }

}

public enum PowerUpType
{
    DoubleAttack,Shield,JetPack
}




