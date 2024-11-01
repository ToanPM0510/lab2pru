using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    //public void PlayGame()
    //{
    //    SceneManager.LoadScene(1);
    //}

    //public void QuitGame()
    //{
    //    Application.Quit();
    //}

    //public void Back()
    //{
    //    SceneManager.LoadScene(0);
    //}
    //public void StartAgain()
    //{
    //    SceneManager.LoadScene(SceneSwitcher.Level);
    //}
    public void PlayGame()
    {
        SceneManager.LoadScene(1);
        GameManager.totalCoins = 0;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Back()
    {
        SceneManager.LoadScene(SceneSwitcher.MAIN_MENU_SCENE);
    }

    public void StartAgain()
    {
        if (SceneSwitcher.currentLevel > 0)
        {
            SceneManager.LoadScene(SceneSwitcher.currentLevel);
        }
        else
        {
            SceneManager.LoadScene(1);
        }
    }
}
