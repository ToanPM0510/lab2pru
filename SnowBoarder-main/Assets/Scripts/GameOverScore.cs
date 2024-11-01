using TMPro;
using UnityEngine;

public class GameOverScore : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI currentScoreText;
    [SerializeField] private float scoreCountDuration = 2f;

    private float currentDisplayScore;
    private float targetScore;
    private float countTimer;
    private bool isCountingScore = false;

    private void Start()
    {
        targetScore = PlayerPrefs.GetFloat("lastGameScore", 0);

        StartScoreAnimation();

        PlayerPrefs.DeleteKey("lastGameScore");
    }

    private void Update()
    {
        if (isCountingScore)
        {
            AnimateScore();
        }
    }

    private void StartScoreAnimation()
    {
        currentDisplayScore = 0;
        countTimer = 0;
        isCountingScore = true;
    }

    private void AnimateScore()
    {
        if (countTimer < scoreCountDuration)
        {
            countTimer += Time.deltaTime;
            float progress = countTimer / scoreCountDuration;
            currentDisplayScore = Mathf.Lerp(0, targetScore, progress);
            currentScoreText.text = " : " + Mathf.RoundToInt(currentDisplayScore).ToString();
        }
        else
        {
            currentDisplayScore = targetScore;
            currentScoreText.text = " : " + targetScore.ToString();
            isCountingScore = false;
        }

    }
}
