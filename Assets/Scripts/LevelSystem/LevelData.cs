using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelData : ScriptableObject
{
    public string LevelName;
    public float LevelHPMulti=1,LevelDmgMulti=1;
    public List<EnemyByTime> enemies = new List<EnemyByTime>();
    public List<Reward> winRewards = new List<Reward>();

    public int LevelGold
    {
        get
        {
            foreach (var item in winRewards.ToArray())
            {
                if (item.type == RewardType.Coin)
                    return item.amount;
            }
            return 0;
        }
    }

    public void setDirty()
    {
#if UNITY_EDITOR
        UnityEditor.EditorUtility.SetDirty(this);
#endif
    }
}

[System.Serializable]
public class EnemyByTime
{
    public GameObject Enemy;
    public float Time;
    public Vector2 SpawnPos = new Vector2();
    public Direction dir;


}
public enum Direction
{
    Right=1,Left=-1
}
