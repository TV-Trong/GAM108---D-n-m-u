using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    GameObject loading;
    private void Start()
    {
        loading = GameObject.Find("BSLoading");
        loading.SetActive(false);

        inputName = GetComponentInChildren<TMP_InputField>();
    }

    public void ConfirmAndPlay()
    {
        playerName = inputName.text;
        timeCreated = DateTime.Now;
        if(DataManager.Instance.playersList.Count > 0)
        {
            foreach (var user in DataManager.Instance.playersList)
            {
                if (user.playerName == playerName)
                {
                    Debug.LogError("Tên đã tồn tại, hãy chọn tên khác!");
                    return;
                }
            }
        }

        if (playerName == "" || playerName == null)
        {
            Debug.LogError("Tên không được để trống!");
            return;
        }
        if (playerName.Count() > 16)
        {
            Debug.LogError("Tên không được dài quá 16 ký tự!");
            return;
        }

        StartNewCharacter();
    }

    void StartNewCharacter()
    {
        PlayerFile player = new PlayerFile(playerName);
        DataManager.Instance.playersList.Add(player);
        DataManager.Instance.SetCurrentPlayer(player);
        JsonManager.Instance.SaveData();
        StartCoroutine(LoadGame());
    }
    IEnumerator LoadGame()
    {
        loading.SetActive(true);
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(DataManager.Instance.currentPlayer.lastCurrentScene);
    }
}
