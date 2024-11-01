using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [System.Serializable]
    public class LevelMultiplier
    {
        public string sceneName;
        public int coinMultiplier;
        public int levelNumber;
    }

    [SerializeField] private LevelMultiplier[] levelMultipliers;
    private int currentLevelNumber = 1;
    private string currentSceneName;

    public static int totalCoins = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        currentSceneName = scene.name;
        UpdateCurrentLevel();

        var player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            var playerController = player.GetComponent<PlayerController>();
            if (playerController != null)
            {
                playerController.UpdateCoinDisplay(totalCoins);
            }
        }
    }

    private void UpdateCurrentLevel()
    {
        foreach (LevelMultiplier lm in levelMultipliers)
        {
            if (lm.sceneName == currentSceneName)
            {
                currentLevelNumber = lm.levelNumber;
                return;
            }
        }
    }

    public int GetCoinMultiplier()
    {
        foreach (LevelMultiplier lm in levelMultipliers)
        {
            if (lm.sceneName == currentSceneName)
            {
                return lm.coinMultiplier;
            }
        }
        return 1;
    }

    public void AddCoins(int amount)
    {
        totalCoins += amount;

        var player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            var playerController = player.GetComponent<PlayerController>();
            if (playerController != null)
            {
                playerController.UpdateCoinDisplay(totalCoins);
            }
        }
    }

    public int GetTotalCoins()
    {
        return totalCoins;
    }

    public void LoadNextLevel()
    {
        int nextLevelNumber = currentLevelNumber + 1;
        string nextSceneName = "";

        foreach (LevelMultiplier lm in levelMultipliers)
        {
            if (lm.levelNumber == nextLevelNumber)
            {
                nextSceneName = lm.sceneName;
                break;
            }
        }

        if (!string.IsNullOrEmpty(nextSceneName))
        {
            SceneManager.LoadScene(nextSceneName);
        }
        else
        {
            Debug.Log("No more levels!");
        }
    }

    public int CalculateCoinValue(int baseCoinValue)
    {
        return baseCoinValue * GetCoinMultiplier();
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}