using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    private void Awake()
    {
        var sceneControllerCount = FindObjectsOfType<SceneController>().Count();

        if (sceneControllerCount > 1)
            Destroy(this);
        else
            DontDestroyOnLoad(this);
    }


    public void LoadNextScene()
    {
        if (SceneManager.GetActiveScene().buildIndex == 3)
            print("no more scene");
        else
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ReloadThisScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}