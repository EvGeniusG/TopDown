using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    private int currentScore = 0;
    private const string HighScoreKey = "HighScore";

    public bool isHighScore {get; private set;} = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public int GetCurrentScore()
    {
        return currentScore;
    }

    public int GetHighScore()
    {
        return PlayerPrefs.GetInt(HighScoreKey, 0);
    }

    public void AddScore(int points)
    {
        currentScore += points;
        if (currentScore > GetHighScore())
        {
            isHighScore = true;
        }
    }

    public void SaveScore(){
        if (currentScore > GetHighScore())
        {
            PlayerPrefs.SetInt(HighScoreKey, currentScore);
        }
    }

    public void ResetScore()
    {
        currentScore = 0;
        isHighScore = false;
    }
}
