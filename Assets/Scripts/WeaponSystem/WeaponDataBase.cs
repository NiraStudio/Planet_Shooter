using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDataBase : ScriptableObject {
    public List<WeaponData> DataBase = new List<WeaponData>();
    public int Count
    {
        get { return DataBase.Count; }
    }
    public WeaponData GiveByID(int id)
    {
        WeaponData data = CreateInstance(typeof(WeaponData)) as WeaponData;
        bool found = false;
        foreach (var item in DataBase.ToArray())
        {
            if (item.id == id)
            {
                data = item;
                found = true;
                break;
            }
        }
        if (found)
            return data;
        else
            return null;
    }
    public WeaponData GiveByIndex(int i)
    {
        return DataBase[i];
    }
    public void AddWeapon(WeaponData data)
    {
        DataBase.Add(data);
        setDirty();
    }
    public void RemoveWeapon(WeaponData data)
    {
        DataBase.Remove(data);
        setDirty();

    }
    public int IDGiver
    {
        get
        {
            int a = 0;

            bool b = false;
            if (DataBase.Count > 0)
            {
                while (b==false)
                {
                    b = true;
                    a = Random.Range(1000, 9999);

                    foreach (var item in DataBase.ToArray())
                    {
                        if (a==item.id)
                        {
                            b = false;
                            break;
                        }
                    }
                }
                
                
            }
            return a;
        }
    }
    public bool ValidID(int ID)
    {
        
        foreach (var item in DataBase.ToArray())
        {
            if (item.id == ID)
                return false;
        }
        return true;
    }

    public void RemoveWeapon(int Index)
    {
        DataBase.Remove(DataBase[Index]);
    }
   /* public CharacterData GiveByRandom()
    {
        CharacterData d = CreateInstance(typeof(CharacterData)) as CharacterData;
        do
        {
            d = DataBase[Random.Range(0, DataBase.Count)];
        } while (GameManager.instance.DoesPlayerHasThisCharacter(d.id) == false);
        return d;
    }*/

    public List<WeaponData> GiveByQuality(ItemQuality type)
    {
        List<WeaponData> answer = new List<WeaponData>();
        foreach (var item in DataBase)
        {
            if (item.quality == type)
                answer.Add(item);
        }
        return answer;
    }
    public ItemQuality giveCharacterQuality(int id)
    {
        return GiveByID(id).quality;
    }
   /* public CharacterData GiveNewCharacter()
    {
        CharacterData d = null;
        List<CharacterData> data = new List<CharacterData>();
        foreach (var item in DataBase.ToArray())
        {
            if (!GameManager.instance.DoesPlayerHasThisCharacter(item.id))
            {
                data.Add(item);
            }
        }
        if (data.Count > 0)
        {
            d = data[Random.Range(0, data.Count)];
        }
        else
            d = DataBase[Random.Range(0, data.Count)];

        return d;
    }*/
    public void setDirty()
    {
#if UNITY_EDITOR
        UnityEditor.EditorUtility.SetDirty(this);
#endif
    }
}
