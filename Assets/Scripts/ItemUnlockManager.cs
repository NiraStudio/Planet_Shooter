using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using ItemUnlock;

public class ItemUnlockManager : MonoBehaviour
{
    public static ItemUnlockManager Instance;
    public int currentUnlockLevel;
    public List<LevelUnlock> Data = new List<LevelUnlock>();
    public string ms;
    // Use this for initialization
    void Start()
    {
        Instance = this;
        print(IsItUnlocked(1, UnlockType.Skin));
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Data = JsonConvert.DeserializeObject<List<LevelUnlock>>(ms);
            JsonConvert.DeserializeObject(ms);
        }
    }

    public bool IsItUnlocked(int ID, UnlockType type)
    {
        switch (type)
        {
            case UnlockType.Skin:
                foreach (var data in Data.ToArray())
                {
                    foreach (var item in data.items.ToArray())
                    {
                        if (item.id == ID && item.type == type)
                        {
                            if (data.level <= currentUnlockLevel)
                                return true;
                        }
                    }
                }
                break;
            case UnlockType.Weapon:
                foreach (var data in Data.ToArray())
                {
                    foreach (var item in data.items.ToArray())
                    {
                        if (item.id == ID && item.type == type)
                        {
                            if (data.level <= currentUnlockLevel)
                                return true;
                        }
                    }
                }
                break;
        }
        return false;
    }



}


namespace ItemUnlock
{
    [System.Serializable]
    public class LevelUnlock
    {
        string namea = "ajda";
        public int level;
        public List<UnlockItem> items = new List<UnlockItem>();
    }
    [System.Serializable]
    public class UnlockItem
    {
        public UnlockType type;
        public int id;
    }

    [System.Serializable]
    public enum UnlockType
    {
        Skin,Weapon
    }
}
