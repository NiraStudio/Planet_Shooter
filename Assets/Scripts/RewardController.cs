using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardController : MonoBehaviour
{
    public static RewardController Instance;
    public Sprite CoinIcon, NiraCoinIcon, XpIcon;

    private void Awake()
    {
        Instance = this;
    }


    GameManager GM;
    // Use this for initialization
    void Start()
    {
        GM = GetComponent<GameManager>();
    }

    public Sprite RewardIcon(RewardType type)
    {
        switch (type)
        {
            case RewardType.Coin:
                return CoinIcon;
            case RewardType.XP:
                return XpIcon;
            case RewardType.NiraCoin:
                return NiraCoinIcon;
            default:
                return null;
        }
    }
    // Update is called once per frame

    public Reward MakeReward(int amount,RewardType type,bool AutoAdd)
    {
        Reward answer = new Reward();
        answer.icon = RewardIcon(type);
        answer.amount = amount;
        answer.type = type;

        if (AutoAdd)
            AddReward(answer);
        return answer;
    }

    public Reward MakeReward( RewardType type,int ID, bool AutoAdd)
    {
        Reward answer = new Reward();
        answer.icon = RewardIcon(type);
        answer.type = type;
        switch (type)
        {
            case RewardType.Weapon:
                answer.weaponID = ID;
                break;
            case RewardType.Skin:
                answer.skinID = ID;
                break;
            default:
                break;
        }

        if (AutoAdd)
            AddReward(answer);
        return answer;
    }

    



    public void AddReward(Reward r)
    {
        switch (r.type)
        {
            case RewardType.Coin:
                GM.Coin += r.amount;
                break;
            case RewardType.XP:
                break;
            case RewardType.NiraCoin:
                GM.AddNiraCoin(r.amount);
                break;
            case RewardType.Weapon:
                GM.AddWeapon(r.weaponID);
                break;
            default:
                break;
        }
    }
}

[System.Serializable]
public class Reward
{
    public Sprite icon;
    public RewardType type;
    public int amount;
    public int weaponID;
    public int skinID;
}

public enum RewardType
{
    Coin,XP,NiraCoin,Weapon,Skin
}
