using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelData : ScriptableObject
{
    public string LevelName;
    public float LevelMulti;
    public List<EnemyByTime> enemies = new List<EnemyByTime>();
    public List<Reward> winRewards = new List<Reward>();
    public List<Reward> lostRewards = new List<Reward>();
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
