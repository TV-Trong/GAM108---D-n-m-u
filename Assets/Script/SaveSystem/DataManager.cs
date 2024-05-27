using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using System.IO;

public class DataManager : Singleton<DataManager>
{
    public List<PlayerFile> playersList = new List<PlayerFile>();
    public PlayerFile currentPlayer;
    private void Start()
    {
        StartCoroutine(LoadData());
    }
    public void SetCurrentPlayer(PlayerFile player)
    {
        currentPlayer = player;
    }
    IEnumerator LoadData()
    {
        string filePath = Application.persistentDataPath + "/Saves/" + "saveFile.sav"; 
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            PlayerList dataList = JsonConvert.DeserializeObject<PlayerList>(json);
            Debug.Log("Lấy dữ liệu thành công!");
            playersList = dataList.savedList;
            yield return null;
        }
        else
        {
            Debug.LogWarning("Không tìm thấy dữ liệu!");
            yield return null;
        }
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
    public PlayerFile(string name) //Danh cho tao nhan vat
    {
        playerName = name;
        health = 100;
        coin = 0;
        soul = 0;
        timeCreated = DateTime.Now;
        timePlayed = 0;
        lastPosition = new Vector2(-10.48f, -1.35f);
        lastCurrentScene = "Tutorial";
    }
    //public PlayerFile(string playerName, DateTime timeCreated, float timePlayed, float health, int coin, int soul, Vector2 lastPosition, string lastCurrentScene)
    //{
    //    this.playerName = playerName;
    //    this.timeCreated = timeCreated;
    //    this.timePlayed = timePlayed;
    //    this.health = health;
    //    this.coin = coin;
    //    this.soul = soul;
    //    this.lastPosition = lastPosition;
    //    this.lastCurrentScene = lastCurrentScene;
    //}
    public void TakeDamage(float damage)
    {
        health -= damage;
    }
    public void CoinUp()
    {
        coin += 1;
    }
    public void SetTimePlayed(float deltaTime)
    {
        timePlayed += deltaTime;
    }
}


[Serializable]
public class PlayerList
{
    public List<PlayerFile> savedList;
    public PlayerList()
    {
        savedList = DataManager.Instance.playersList;
    }
}