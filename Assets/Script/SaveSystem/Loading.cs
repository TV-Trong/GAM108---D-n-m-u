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
    TextMeshProUGUI playerName, timeCreated;
    Button loadButton;
    [SerializeField] GameObject playerInfo;

    private void Start()
    {
        int count = 0;
        foreach (PlayerData player in CharacterCreation.playerList)
        {
            SetInstance();
            FindChildren();
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
    private void SetText(int count)
    {
        string name = CharacterCreation.playerList[count].playerName;
        DateTime created = CharacterCreation.playerList[count].timeCreated;
        playerName.text = name;
        timeCreated.text = created.ToShortDateString();
    }
    private void FindChildren()
    {
        playerName = GameObject.Find("Name").GetComponentInChildren<TextMeshProUGUI>();
        timeCreated = GameObject.Find("Created").GetComponentInChildren<TextMeshProUGUI>();
        loadButton = GameObject.Find("Panel").GetComponentInChildren<Button>();
        loadButton.onClick.AddListener(LoadGame);
    }
    private void ReplaceName()
    {
        playerName.gameObject.name = "name";
        timeCreated.gameObject.name = "time";
        loadButton.gameObject.name = "panel";
    }

    private void LoadGame()
    {
        SceneManager.LoadScene(CharacterCreation.playerList[0].lastCurrentScene);
    }
}
