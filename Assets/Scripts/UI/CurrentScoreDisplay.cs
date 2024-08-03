using TMPro;
using UnityEngine;
using System.Collections;

public class CurrentScoreDisplay : MonoBehaviour
{
    [SerializeField] TMP_Text currentScoreText;
    private int lastDisplayedScore;

    private void Start()
    {
        lastDisplayedScore = ScoreManager.Instance.GetCurrentScore();
        currentScoreText.text = lastDisplayedScore.ToString();
        StartCoroutine(UpdateScoreRoutine());
    }

    private IEnumerator UpdateScoreRoutine()
    {
        yield return new WaitUntil(() => ScoreManager.Instance.GetCurrentScore() != lastDisplayedScore);
        
        while (true)
        {
            int currentScore = ScoreManager.Instance.GetCurrentScore();
            if (currentScore != lastDisplayedScore)
            {
                lastDisplayedScore = currentScore;
                currentScoreText.text = currentScore.ToString();
            }
            yield return new WaitUntil(() => ScoreManager.Instance.GetCurrentScore() != lastDisplayedScore);
        }
    }
}
