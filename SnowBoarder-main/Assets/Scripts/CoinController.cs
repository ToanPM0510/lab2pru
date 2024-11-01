using UnityEngine;

public class CoinController : MonoBehaviour
{
    [SerializeField] private int baseCoinValue = 1;
    private AudioManager audioManager;
    private PlayerController playerController;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            audioManager.PlaySFX(audioManager.coinClip);

            int coinAmount = GameManager.Instance.CalculateCoinValue(baseCoinValue);
            playerController.IncreaseCoinCount(coinAmount);

            Destroy(this.gameObject);
        }
    }
}
