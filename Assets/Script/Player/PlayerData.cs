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
    public void SetName(string playerName)
    {
        this.playerName = playerName;
    }
    public void SetHealth(float health)
    {
        this.health = health;
    }
    public void SetCoin(int coin)
    {
        this.coin = coin;
    }
    public void SetSoul(int soul)
    {
        this.soul = soul;
    }
    public void SetTimePlayed(float timePlayed)
    {
        this.timePlayed = timePlayed;
    }
    public void SetLastPosition(Vector2 lastPosition)
    {
        this.lastPosition = lastPosition;
    }
    public void SetLastScene(string lastCurrentScene)
    {
        this.lastCurrentScene = lastCurrentScene;
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
