using System;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    
    
    private int _currentScore;
    public int CurrentScore => _currentScore;

    private int _highScore;
    public int HighScore => _highScore;

    private string HighScoreString = nameof(HighScore);
    public event Action brokenHighScore;

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

    private void IncreaseScore()
    {
        _currentScore++;
    }

    private void UpdateScoreText()
    {
        scoreText.text = _currentScore.ToString();
    }

    private void CheckHighScore()
    {
        if (!(_currentScore > PlayerPrefs.GetFloat(HighScoreString))) return;
        
        _highScore = _currentScore;
        PlayerPrefs.SetFloat(HighScoreString, HighScore);
        brokenHighScore?.Invoke();
    }
}
