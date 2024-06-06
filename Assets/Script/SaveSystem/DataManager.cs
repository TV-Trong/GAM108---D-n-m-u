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
        LoadData();
    }
    public void SetCurrentPlayer(PlayerFile player)
    {
        currentPlayer = player;
    }
    void LoadData()
    {
        string filePath = Application.persistentDataPath + "/Saves/" + "saveFile.sav"; 
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            PlayerList dataList = JsonConvert.DeserializeObject<PlayerList>(json);
            playersList = dataList.savedList;
            Debug.Log("Lấy dữ liệu thành công!");
        }
        else
        {
            Debug.LogWarning("Không tìm thấy dữ liệu!");
        }
    }
    public void DeletePlayer()
    {
        playersList.Remove(currentPlayer);
        JsonManager.Instance.SaveData();
    }
}
public class PlayerFile
{
    public int life;
    public string playerName;
    public float health;
    public int coin;
    public int soul;
    public DateTime timeCreated;
    public float timePlayed;
    public Vector2 lastPosition;
    public string lastCurrentScene;
    public bool isWon;
    public PlayerFile(string name = "") //Danh cho tao nhan vat
    {
        playerName = name;
        life = 3;
        health = 100;
        coin = 0;
        soul = 0;
        timeCreated = DateTime.Now;
        timePlayed = 0;
        lastPosition = Vector2.zero;
        lastCurrentScene = "Map 1";
        isWon = false;
    }
    

    public void TakeDamage(float damage)
    {
        health -= damage;
    }
    public void LoseLife()
    {
        life -= 1;
    }
    public void ResetHP()
    {
        health = 100;
    }
    public void ResetPos()
    {
        lastPosition = Vector2.zero;
    }
    public void CoinUp()
    {
        coin += 1;
    }
    public void LifeUp()
    {
        if (coin >= 20)
        {
            coin -= 20;
            life += 1;

            Speaker speaker = GameObject.Find("Speaker").GetComponent<Speaker>();
            speaker.PlaySoundLifeUp();
        }
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