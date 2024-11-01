using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    //[HideInInspector] public static int Level;
    //public void LoadLevel(int levelToLoad)
    //{
    //    Level = levelToLoad;
    //    SceneManager.LoadScene(levelToLoad);
    //}

    public static int currentLevel; 
    public const int GAME_OVER_SCENE = 6; 
    public const int MAIN_MENU_SCENE = 0; 

    public void LoadLevel(int levelToLoad)
    {
        currentLevel = levelToLoad; 
        SceneManager.LoadScene(levelToLoad);
    }
}
