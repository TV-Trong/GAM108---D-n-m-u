using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : Singleton<PlayerData>
{
    public string playerName;
    public float health;
    public int coin;
    public int soul;
    public DateTime timeCreated;
    public float timePlayed;
    public Vector2 lastPosition;
    public string lastCurrentScene;

    public PlayerData GetAllData()
    {
        PlayerData data = new PlayerData();
        data.playerName = playerName;
        data.health = health;
        data.coin = coin;
        data.soul = soul;
        data.timeCreated = timeCreated;
        data.timePlayed = timePlayed;
        data.lastPosition = lastPosition;
        data.lastCurrentScene = lastCurrentScene;
        return data;
    }
    public string GetName()
    {
        return playerName;
    }
    public float GetHealth()
    {
        return health;
    }
    public int GetCoin()
    {
        return coin;
    }
    public int GetSoul()
    {
        return soul;
    }
    public DateTime GetDateCreated()
    {
        return timeCreated;
    }
    public float GetTimePlayed()
    {
        return timePlayed;
    }
    public Vector2 GetLastPosition()
    {
        return lastPosition;
    }
    public string GetLastScene()
    {
        return lastCurrentScene;
    }

    public void SetData(string playerName, DateTime timeCreated, float timePlayed, float health, int coin, int soul, Vector2 lastPosition, string lastCurrentScene)
    {
        this.playerName = playerName;
        this.health = health;
        this.coin = coin;
        this.soul = soul;
        this.timeCreated = timeCreated;
        this.timePlayed = timePlayed;
        this.lastPosition = lastPosition;
        this.lastCurrentScene = lastCurrentScene;
    }
}
