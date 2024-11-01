using TMPro;
using UnityEngine;

public class ScoreDiplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI finalScoreText;
    [SerializeField] private float scoreCountDuration = 2f;

    private float currentDisplayScore;
    private float targetScore;
    private float countTimer;
    private bool isCountingScore = false;

    private void Start()
    {
        targetScore = GameManager.Instance.GetTotalCoins();
        StartScoreAnimation();
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
            finalScoreText.text = " : " + Mathf.RoundToInt(currentDisplayScore).ToString();
        }
        else
        {
            currentDisplayScore = targetScore;
            finalScoreText.text = " : " + targetScore.ToString();
            isCountingScore = false;
        }
    }
}
