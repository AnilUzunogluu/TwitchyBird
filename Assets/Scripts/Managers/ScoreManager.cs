using System;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;

    private string ScoreText
    {
        set => scoreText.text = value;
    }

    public int CurrentScore { get; private set; }

    public event Action OnHighScoreChanged;

    private void OnEnable()
    {
        GameManager.Instance.OnGameOver += CheckHighScore;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnGameOver -= CheckHighScore;
    }
    
    public void GotPoint()
    {
        IncreaseScore();
        UpdateScoreText();
    }

    private void IncreaseScore(int amount = 1)
    {
        CurrentScore += amount;
    }

    private void UpdateScoreText()
    {
        ScoreText = CurrentScore.ToString();
    }

    private void CheckHighScore()
    {
        if (!(CurrentScore > PlayerStats.HighScore)) return;
        
        PlayerStats.HighScore = CurrentScore;
        OnHighScoreChanged?.Invoke();
    }
}