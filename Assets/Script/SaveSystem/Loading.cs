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
    private void Start()
    {
        loading = GameObject.Find("BSLoading");
        loading.SetActive(false);
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
        float played = DataManager.Instance.playersList[i].timePlayed;
        playerName.text = name;
        timeCreated.text = created.ToShortDateString();
        timePlayed.text = played.ToString("00.0") + " h";
    }
    private void FindChildren(int i)
    {
        playerName = GameObject.Find("Name").GetComponentInChildren<TextMeshProUGUI>();
        timeCreated = GameObject.Find("Created").GetComponentInChildren<TextMeshProUGUI>();
        timePlayed = GameObject.Find("Played").GetComponentInChildren<TextMeshProUGUI>();
        loadButton = GameObject.Find("Panel").GetComponentInChildren<Button>();
        loadButton.onClick.AddListener(() => StartLoadGame(i));
    }
    private void ReplaceName()
    {
        playerName.gameObject.name = "name";
        timeCreated.gameObject.name = "created";
        timePlayed.gameObject.name = "played";
        loadButton.gameObject.name = "panel";
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
}
