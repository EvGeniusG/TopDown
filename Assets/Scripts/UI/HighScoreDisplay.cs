using TMPro;
using UnityEngine;

public class HighScoreDisplay : MonoBehaviour
{
    [SerializeField] TMP_Text highScoreText;

    private void Start()
    {
        if (highScoreText != null)
        {
            highScoreText.text = ScoreManager.Instance.GetHighScore().ToString();
        }
    }
}
