using UnityEngine.SceneManagement;
using UnityEngine;

public class chucnang : MonoBehaviour
{
    public GameObject pauseMenuScreen;
    public void MenuPlay()
    {
        SceneManager.LoadScene("SceneManager.GetActiveScene().buildIndex + 1");

    }
    public void Quit()
    {
        

    }
    public void Pause()
    {
        Time.timeScale = 0;
        pauseMenuScreen.SetActive(true);


    }
    public void ResumeGame()
    {
        Time.timeScale = 1;
        pauseMenuScreen.SetActive(false);

    }
    public void GoToMenu()
    {
        SceneManager.LoadScene("MenuPlay");

    }

}
