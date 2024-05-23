using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void NewGame()
    {
        SceneManager.LoadScene("CharacterCreation");
    }
    public void LoadGame()
    {
        SceneManager.LoadScene("ContinuePlaying");
    }
    public void ExitGame()
    {
        Debug.Log("Thoát game...");
        Application.Quit();
    }
}
