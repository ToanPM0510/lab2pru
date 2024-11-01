using UnityEngine;
using UnityEngine.SceneManagement;

public class CrashDetector : MonoBehaviour
{
    [SerializeField] private int level;
    [SerializeField] float delayTime = 1f;
    [SerializeField] ParticleSystem crashEffect;
    [SerializeField] AudioClip crashSFX;
    bool hasCrashed = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if ((other.tag == "Ground" || other.tag == "Enemy") && !hasCrashed)
        {
            var player = GameObject.FindGameObjectWithTag("Player");
            var controller = player.GetComponent<PlayerController>();
            var oldScore = PlayerPrefs.GetFloat("highest");
            var newScore = controller.RotationTime;

            if (newScore > oldScore)
            {
                PlayerPrefs.SetFloat("highest", newScore);
            }

            PlayerPrefs.SetFloat("lastGameScore", GameManager.totalCoins);

            GameManager.totalCoins = 0;
            hasCrashed = true;
            FindObjectOfType<PlayerController>().DisableControls();
            SceneSwitcher.currentLevel = level;
            if (crashEffect != null) crashEffect.Play();
            if (crashSFX != null) GetComponent<AudioSource>().PlayOneShot(crashSFX);

            Invoke("LoadGameOverScene", delayTime);
        }
    }

    void LoadGameOverScene()
    {
        SceneManager.LoadScene(SceneSwitcher.GAME_OVER_SCENE);
    }
}
