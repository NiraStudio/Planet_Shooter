using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using PowerUps;

public class PowerUpManager : MonoBehaviour {
    public static PowerUpManager Instance;
    private void Awake()
    {
        Instance = this;
    }

    public List<PowerUpByTime> powerUpsByTime = new List<PowerUpByTime>();


    List<PowerUpState> powerUpsState = new List<PowerUpState>();
    List<ChangeByTime> changes = new List<ChangeByTime>();


    // Use this for initialization
    void Start () {
        var myEnumMemberCount = System.Enum.GetNames(typeof(PowerUpType)).Length;
        for (int i = 0; i < myEnumMemberCount; i++)
        {
            powerUpsState.Add(new PowerUpState((PowerUpType)i, false));
        }
    }
	
	// Update is called once per frame
	void Update () {
        foreach (var item in changes.ToArray())
        {
            if(item.time>0)
            {
                item.time -= Time.deltaTime;
                if(item.time<=0)
                {
                    item.action.Invoke();
                    changes.Remove(item);
                }
            }
        }
	}


    float getPowerUpTime(PowerUpType type)
    {
        foreach (var item in powerUpsByTime.ToArray())
        {
            if (item.type == type)
            {
                return item.time;
            }
        }

        return 0;
    }


    class ChangeByTime
    {
        public float time;
        public UnityAction action;
        public ChangeByTime(float t,UnityAction a)
        {
            time = t;
            action = a;
        }
    }

    public bool IsActive(PowerUpType type)
    {
        foreach (var item in powerUpsState.ToArray())
        {
            if (item.type == type)
            {
                return item.active;
            }
        }
        return false;
    }

    private void Reset()
    {
        powerUpsByTime = new List<PowerUpByTime>();
        var myEnumMemberCount = System.Enum.GetNames(typeof(PowerUpType)).Length;
        for (int i = 0; i < myEnumMemberCount; i++)
        {
            powerUpsByTime.Add(new PowerUpByTime((PowerUpType)i, 0));
        }
    }
}


namespace PowerUps
{
    [System.Serializable]
    public class PowerUpByTime
    {
        public PowerUpType type;
        public float time;

        public PowerUpByTime(PowerUpType tt, float t)
        {
            type = tt;
            time = t;
        }

    }

    [System.Serializable]
    public class PowerUpState
    {
        public PowerUpType type;
        public bool active;

        public PowerUpState(PowerUpType t,bool a)
        {
            type = t;
            active = a;
        }
    }

}

public enum PowerUpType
{
    DoubleAttack,Shield,Slow,Bomb
}




