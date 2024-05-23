using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharacterCreation : MonoBehaviour
{
    TMP_InputField inputName;

    public string playerName;
    public DateTime timeCreated;

    public static List<PlayerData> playerList = new List<PlayerData>();
    private void Start()
    {
        inputName = GetComponentInChildren<TMP_InputField>();
    }

    public void ConfirmAndPlay()
    {
        playerName = inputName.text;
        timeCreated = DateTime.Now;
        foreach (PlayerData user in playerList)
        {
            if (user.playerName == playerName)
            {
                Debug.LogError("Tên đã tồn tại, hãy chọn tên khác!");
                return;
            }
            if (user.playerName == "" || user.playerName == null)
            {
                Debug.LogError("Tên không được để trống!");
                return;
            }
        }
        StartNewCharacter();
    }

    void StartNewCharacter()
    {
        float timePlayed = 0;
        float health = 100;
        int coin = 0;
        int soul = 0;
        Vector2 position = new Vector2(-10.48f, -1.35f);
        string scene = "Tutorial";
        PlayerData player = new PlayerData(playerName, timeCreated, timePlayed, health, coin, soul, position, scene);
        playerList.Add(player);
        SceneManager.LoadScene(scene);
    }
}
