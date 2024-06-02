using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : Singleton<SceneLoader>
{
    PlayerMovement playerMovement;
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
        playerMovement = FindObjectOfType<PlayerMovement>();
        playerMovement.SavePosition();
        JsonManager.Instance.SaveData();
        SceneManager.LoadScene("MainMenu");
    }

    public void LoseALife()
    {
        if (DataManager.Instance.currentPlayer.life <= 0)
        {
            StartCoroutine(GameOver());
            JsonManager.Instance.SaveData();
        }
        else
        {
            StartCoroutine(DelayedReload());
            DataManager.Instance.currentPlayer.ResetHP();
            JsonManager.Instance.SaveData();
        }
    }
    IEnumerator DelayedReload()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    IEnumerator GameOver()
    {
        yield return new WaitForSeconds(2f);
        LoseGame();
    }

    public void LoadNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void WinGame()
    {
        SceneManager.LoadScene("Chienthang");
    }
    public void LoseGame()
    {
        SceneManager.LoadScene("Thatbai");
    }
    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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
