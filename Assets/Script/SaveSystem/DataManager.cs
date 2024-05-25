using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : Singleton<DataManager>
{
    public List<PlayerFile> playersList = new List<PlayerFile>();
    public PlayerFile currentPlayer;
    public void SetCurrentPlayer(PlayerFile player)
    {
        currentPlayer = player;
    }
}
public class PlayerFile
{
    public string playerName;
    public float health;
    public int coin;
    public int soul;
    public DateTime timeCreated;
    public float timePlayed;
    public Vector2 lastPosition;
    public string lastCurrentScene;
    public PlayerFile(string name)
    {
        playerName = name;
        health = 0;
        coin = 0;
        soul = 0;
        timeCreated = DateTime.Now;
        timePlayed = 0;
        lastPosition = new Vector2(-10.48f, -1.35f);
        lastCurrentScene = "Tutorial";
    }
    public PlayerFile(string playerName, DateTime timeCreated, float timePlayed, float health, int coin, int soul, Vector2 lastPosition, string lastCurrentScene)
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
[System.Serializable]
public class PlayerList
{
    public List<PlayerFile> savedList;
    public PlayerList()
    {
        savedList = DataManager.Instance.playersList;
    }
}