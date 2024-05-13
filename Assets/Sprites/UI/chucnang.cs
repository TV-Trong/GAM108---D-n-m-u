using UnityEngine.SceneManagement;
using UnityEngine;

public class chucnang : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }
    public void Quit()
    {

    }

}
