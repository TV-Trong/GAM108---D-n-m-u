using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData
{
    public string playerName;
    public float health;
    public int coin;
    public int soul;
    public DateTime timeCreated;
    public float timePlayed;
    public Vector2 lastPosition;
    public string lastCurrentScene;

    public PlayerData (string playerName, DateTime timeCreated, float timePlayed, float health, int coin, int soul, Vector2 lastPosition, string lastCurrentScene)
    {
        this.playerName = playerName;
        this.timeCreated = timeCreated;
        this.timePlayed = timePlayed;
        this.health = health;
        this.coin = coin;
        this.soul = soul;
        this.lastPosition = lastPosition;
        this.lastCurrentScene = lastCurrentScene;
    }
}
