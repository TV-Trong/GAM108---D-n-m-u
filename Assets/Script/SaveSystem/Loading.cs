using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Loading : MonoBehaviour
{
    TextMeshProUGUI playerName, timeCreated;
    public GameObject playerInfo;

    private void Start()
    {
        PlayerData playerData = new PlayerData("Username", DateTime.Now);
        CharacterCreation.playerList.Add(playerData);
        CharacterCreation.playerList.Add(playerData);
        //playerName = GameObject.Find("Name").GetComponent<TextMeshProUGUI>();
        //timeCreated = GameObject.Find("Created").GetComponent<TextMeshProUGUI>();
        foreach (PlayerData player in CharacterCreation.playerList)
        {
            GameObject instance = Instantiate(playerInfo, Vector3.zero, Quaternion.identity);
            instance.transform.SetParent(gameObject.transform);
            instance.transform.localScale = Vector3.one;
            instance.transform.localPosition = Vector3.zero;
            instance.transform.localRotation = Quaternion.identity;
        }
    }
}
