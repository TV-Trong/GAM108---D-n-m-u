using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UpdateUI : MonoBehaviour
{
    Slider healthSlider, shadowSlider;
    TextMeshProUGUI coin, name;


    private void Start()
    {
        healthSlider = GameObject.Find("SliderHealth").GetComponent<Slider>();
        shadowSlider = GameObject.Find("SliderShadow").GetComponent<Slider>();
        coin = GameObject.Find("CoinCount").GetComponent<TextMeshProUGUI>();
        name = GameObject.Find("Name").GetComponentInChildren<TextMeshProUGUI>();
        name.text = DataManager.Instance.currentPlayer.playerName;
        UpdateValue();
    }

    public void UpdateValue()
    {
        healthSlider.value = DataManager.Instance.currentPlayer.health / 100;
        coin.text = DataManager.Instance.currentPlayer.coin.ToString();
    }
}
