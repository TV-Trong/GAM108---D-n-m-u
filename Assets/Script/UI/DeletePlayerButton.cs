using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DeletePlayerButton : MonoBehaviour
{
    Button yesButton, noButton;
    private void Start()
    {
        yesButton = GameObject.Find("YesButton").GetComponent<Button>();
        noButton = GameObject.Find("NoButton").GetComponent<Button>();
        yesButton.onClick.RemoveAllListeners();
        noButton.onClick.AddListener(ClickNo);
    }
    public void ClickYes(int i)
    {
        StartCoroutine(Wait(i));
    }
    IEnumerator Wait(int i)
    {
        yield return new WaitForSeconds(0.5f);
        yesButton.onClick.AddListener(() => DeletePlayer(i));
    }

    public void ClickNo()
    {
        gameObject.SetActive(false);
    }

    private void DeletePlayer(int i)
    {
        try
        {
            DataManager.Instance.playersList.RemoveAt(i);
        }
        catch 
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        JsonManager.Instance.SaveData();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
