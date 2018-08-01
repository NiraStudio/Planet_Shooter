using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeStage.AntiCheat.ObscuredTypes;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class GameManager : MainBehavior
{
    public WeaponDataBase weaponDB;
    public LevelData CurrentLevel;
    #region Singleton
    public static GameManager Instance;
    void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    #endregion



    [Header("Power Up")]

    public PowerUpType SlotPowerUp;
    public PowerUpType AdSlotPowerUp;


    public int Coin
    {
        get { return _currencyData.Coin; }
        set {
            _currencyData.Coin = value;
            SaveCurrencyData();
        }
    }
    [SerializeField]
    CurrencyData _currencyData;
    [SerializeField]
    PlayerData _playerData;

    void Start()
    {
        LoadPlayerData();
        LoadCurrencyData();
        LoadScene("Main_Menu");
    }

    void Update()
    {

    }

    public void FirstTimeChanges()
    {
        _playerData = new PlayerData();
        _currencyData = new CurrencyData();
        SaveCurrencyData();
        SavePlayerData();
    }

    public WeaponData GetCurrentWeapon()
    {
        return weaponDB.GiveByID(1);
    }


    #region Data Changer Methods

    public void AddCoin(int amount)
    {
        LoadCurrencyData();
        _currencyData.Coin += amount;
        SaveCurrencyData();
    }
    public void AddNiraCoin(int amount)
    {
        LoadCurrencyData();
        _currencyData.NiraCoin += amount;
        SaveCurrencyData();
    }

    public void AddWeapon(int ID)
    {
        LoadPlayerData();
        _playerData.WeaponIDs.Add(ID);
        SavePlayerData();
    }
   

    public int CurrentWeapon
    {
        get { return _playerData.CurrentWeaponID; }
        set
        {
            LoadPlayerData();
            _playerData.CurrentWeaponID = value;
            SavePlayerData();
        }
    }

    public int CurrentSkin
    {
        get { return _playerData.CurrentSkinID; }
        set
        {
            LoadPlayerData();
            _playerData.CurrentWeaponID = value;
            SavePlayerData();
        }
    }



    #endregion

    #region Save & Load

    public void SaveCurrencyData()
    {
        BinaryFormatter bf = new BinaryFormatter();
        if (!Directory.Exists(Path.Combine(Application.persistentDataPath, "Data")))
        {
            Directory.CreateDirectory(Path.Combine(Application.persistentDataPath, "Data"));
        }
        FileStream File = new FileStream(Application.persistentDataPath + "/Data/CR.Alpha", FileMode.Create);
        bf.Serialize(File, _currencyData);

        File.Close();
    }
    public void SavePlayerData()
    {
        BinaryFormatter bf = new BinaryFormatter();
        if (!Directory.Exists(Path.Combine(Application.persistentDataPath, "Data")))
        {
            Directory.CreateDirectory(Path.Combine(Application.persistentDataPath, "Data"));
        }
        FileStream File = new FileStream(Application.persistentDataPath + "/Data/PD.Alpha", FileMode.Create);
        bf.Serialize(File, _playerData);
        File.Close();
    }
    public void LoadCurrencyData()
    {
        if (File.Exists(Application.persistentDataPath + "/Data/CR.Alpha"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream File = new FileStream(Application.persistentDataPath + "/Data/CR.Alpha", FileMode.Open);

            _currencyData = bf.Deserialize(File) as CurrencyData;
            File.Close();
        }
        else
        {
            print("File not Exist");
            SaveCurrencyData();
        }
    }
    public void LoadPlayerData()
    {
        if (File.Exists(Application.persistentDataPath + "/Data/PD.Alpha"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream File = new FileStream(Application.persistentDataPath + "/Data/PD.Alpha", FileMode.Open);

            _playerData = bf.Deserialize(File) as PlayerData;
            File.Close();
        }
        else
        {
            print("File not Exist");
            SavePlayerData();
        }
    }
    #endregion


}





[System.Serializable]
public struct UpgradeLevel
{
    public ObscuredInt AmmoAmount, Coin, HP,Damage;
}
public enum UpgradeType
{
   HP,Damage,Coin,AmmoAmount
}

[System.Serializable]
public class CurrencyData
{
    public ObscuredInt Coin;
    public ObscuredInt NiraCoin;
}

[System.Serializable]
public class PlayerData
{
    public int CurrentWeaponID, CurrentSkinID;
    public List<int> WeaponIDs = new List<int>();
    public List<int> SkinID = new List<int>();
}
