using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Loading : MonoBehaviour
{
    TextMeshProUGUI playerName, timeCreated, timePlayed;
    Button loadButton;
    [SerializeField] GameObject playerInfo;
    GameObject loading;

    //Xoa nguoi choi
    Button deleteButton;
    GameObject deletePlayer;
    DeletePlayerButton deletePlayerButton;

    private void Start()
    {
        //Loading
        loading = GameObject.Find("BSLoading");
        loading.SetActive(false);

        //Delete player
        deletePlayerButton = FindObjectOfType<DeletePlayerButton>();
        deletePlayer = GameObject.Find("DeleteConfirmation");
        deletePlayer.SetActive(false);

        //Dem thu tu nguoi choi trong danh sach
        int count = 0;
        foreach (PlayerFile player in DataManager.Instance.playersList)
        {
            SetInstance();
            FindChildren(count);
            SetText(count);
            ReplaceName();
            count++;
        }
    }
    private void SetInstance()
    {
        GameObject instance = Instantiate(playerInfo, Vector3.zero, Quaternion.identity);
        instance.transform.SetParent(gameObject.transform);
        instance.transform.localScale = Vector3.one;
        instance.transform.localPosition = Vector3.zero;
        instance.transform.localRotation = Quaternion.identity;
    }
    private void SetText(int i)
    {
        string name = DataManager.Instance.playersList[i].playerName;
        DateTime created = DataManager.Instance.playersList[i].timeCreated;
        float played = DataManager.Instance.playersList[i].timePlayed / 60;
        playerName.text = name;
        timeCreated.text = created.ToShortDateString();
        timePlayed.text = played.ToString("00.0") + " phút";
    }
    private void FindChildren(int i)
    {
        playerName = GameObject.Find("Name").GetComponentInChildren<TextMeshProUGUI>();
        timeCreated = GameObject.Find("Created").GetComponentInChildren<TextMeshProUGUI>();
        timePlayed = GameObject.Find("Played").GetComponentInChildren<TextMeshProUGUI>();
        loadButton = GameObject.Find("PlayerPanel").GetComponentInChildren<Button>();
        loadButton.onClick.AddListener(() => StartLoadGame(i));
        deleteButton = GameObject.Find("DeleteButton").GetComponentInChildren<Button>();
        deleteButton.onClick.AddListener(() => CallDeletePlayer(i));
    }
    private void ReplaceName()
    {
        playerName.gameObject.name = "name";
        timeCreated.gameObject.name = "created";
        timePlayed.gameObject.name = "played";
        loadButton.gameObject.name = "panel";
        deleteButton.gameObject.name = "delete";
    }
    private void StartLoadGame(int i)
    {
        StartCoroutine(LoadGame(i));
    }
    IEnumerator LoadGame(int i)
    {
        loading.SetActive(true);
        
        yield return new WaitForSeconds(2f);
        DataManager.Instance.SetCurrentPlayer(DataManager.Instance.playersList[i]);
        Debug.Log(DataManager.Instance.currentPlayer.playerName);
        SceneManager.LoadScene(DataManager.Instance.playersList[i].lastCurrentScene);
    }

    public void CallDeletePlayer(int i)
    {
        deletePlayer.SetActive(true);
        deletePlayerButton.ClickYes(i);
    }
}
