using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData
{
    public string username;
    public int health;
    public int coin;
    public int soul;
    public Vector2 pos;
    public DateTime timeCreated;
    public float timePlayed;

    public PlayerData (string name, DateTime time)
    {
       username = name;
       timeCreated = time;
    }
}
