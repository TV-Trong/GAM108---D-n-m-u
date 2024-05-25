using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    PlayerMovement pM;
    private void Start()
    {
        pM = FindObjectOfType<PlayerMovement>();
    }
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
    public void GotoMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void SaveAndGotoMain()
    {
        pM.SavePosition();
        SceneManager.LoadScene("MainMenu");
    }
    
    public void StopTime()
    {
        Time.timeScale = 0f;
    }
    public void UnstopTime()
    {
        Time.timeScale = 1f;
    }
}
