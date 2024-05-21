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
            if (user.username == playerName)
            {
                Debug.LogError("Tên đã tồn tại, hãy chọn tên khác!");
                return;
            }
            if (user.username == "" || user.username == null)
            {
                Debug.LogError("Tên không được để trống!");
                return;
            }
        }

        PlayerData player = new PlayerData(playerName, timeCreated);
        playerList.Add(player);
        SceneManager.LoadScene("Tutorial");
    }
}
