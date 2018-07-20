using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinDataBase :ScriptableObject  {
    public List<Skin> DB = new List<Skin>();

    public void AddSkin(Skin data)
    {
        DB.Add(data);
    }
    public int Count
    {
        get { return DB.Count; }
    }
    public Skin GiveByID(int id)
    {
        Skin data = CreateInstance(typeof(Skin)) as Skin;
        bool found = false;
        foreach (var item in DB.ToArray())
        {
            if (item.ID == id)
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
    public Skin GiveByIndex(int i)
    {
        return DB[i];
    }
    public void AddWeapon(Skin data)
    {
        DB.Add(data);
        setDirty();
    }
    public void RemoveWeapon(Skin data)
    {
        DB.Remove(data);
        setDirty();

    }
    public int IDGiver
    {
        get
        {
            int a = 0;

            bool b = false;
            if (DB.Count > 0)
            {
                while (b == false)
                {
                    b = true;
                    a = Random.Range(1000, 9999);

                    foreach (var item in DB.ToArray())
                    {
                        if (a == item.ID)
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

        foreach (var item in DB.ToArray())
        {
            if (item.ID == ID)
                return false;
        }
        return true;
    }

    public void CleanDataBase()
    {
        DB = new List<Skin>();
    }

    public void setDirty()
    {
#if UNITY_EDITOR
        UnityEditor.EditorUtility.SetDirty(this);
#endif
    }
}

