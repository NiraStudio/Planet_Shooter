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

    public Button Slot, Ad_Slot;
    public List<PowerUpByTime> powerUpsByTime = new List<PowerUpByTime>();

    [SerializeField]
    List<PowerUpState> powerUpsState = new List<PowerUpState>();


    // Use this for initialization
    void Start () {
        var myEnumMemberCount = System.Enum.GetNames(typeof(PowerUpType)).Length;
        for (int i = 1; i < myEnumMemberCount; i++)
        {
            powerUpsState.Add(new PowerUpState((PowerUpType)i, false,0));
        }


        //repaint 1st Slot
        if (GameManager.Instance.SlotPowerUp != 0)
        {
            Slot.onClick.AddListener(() =>
            {
                Activate(GameManager.Instance.SlotPowerUp);
            });
            Slot.gameObject.SetActive(true);
            Slot.GetComponent<Image>().sprite = RewardManager.Instance.PowerUpIcon(GameManager.Instance.SlotPowerUp);
        }
        else
            Slot.gameObject.SetActive(false);

        //Repaint Ad Slot
        if (GameManager.Instance.AdSlotPowerUp != 0)
        {
            Ad_Slot.onClick.AddListener(() =>
            {
                Activate(GameManager.Instance.AdSlotPowerUp);
            });
            Ad_Slot.gameObject.SetActive(true);
            Ad_Slot.GetComponent<Image>().sprite = RewardManager.Instance.PowerUpIcon(GameManager.Instance.AdSlotPowerUp);

        }
        else
            Ad_Slot.gameObject.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
        foreach (var item in powerUpsState.ToArray())
        {
            if (item.time > 0)
                item.time -= Time.deltaTime;

            item.active = item.time > 0;
        }
	}


    public float getPowerUpTime(PowerUpType type)
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

    public bool Activate(PowerUpType type)
    {
        foreach (var item in powerUpsState.ToArray())
        {
            if (item.type == type)
            {
                item.time = getPowerUpTime(type);
            }
        }
        return false;
    }


    private void Reset()
    {
        powerUpsByTime = new List<PowerUpByTime>();
        var myEnumMemberCount = System.Enum.GetNames(typeof(PowerUpType)).Length;
        for (int i = 1; i < myEnumMemberCount; i++)
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
        public float time;

        public PowerUpState(PowerUpType t,bool a,float tt)
        {
            type = t;
            active = a;
            time = tt;
        }
    }

}

public enum PowerUpType
{
    Null,DoubleAttack,Shield,Slow,Bomb,JetPack,SpeedUp
}




