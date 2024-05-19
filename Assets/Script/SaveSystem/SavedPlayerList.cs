using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SavedPlayerList : MonoBehaviour
{
    TextMeshProUGUI playerName, timeCreated;

    private void Awake()
    {
        playerName = GameObject.Find("Name").GetComponentInChildren<TextMeshProUGUI>();
        timeCreated = GameObject.Find("Created").GetComponentInChildren<TextMeshProUGUI>();
    }
    public void SetText(string name, string created)
    {
        playerName.text = name;
        timeCreated.text = created;
    }
}
