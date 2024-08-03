using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NewHighScoreDisplay : MonoBehaviour
{
    [SerializeField] GameObject HightScoreText;
    void Start()
    {
        HightScoreText.SetActive(ScoreManager.Instance.isHighScore);
    }
}
