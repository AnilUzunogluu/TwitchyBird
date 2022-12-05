using System;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private Sprite[] scoreSprites;
    [SerializeField] private SpriteRenderer onesPlace;
    [SerializeField] private SpriteRenderer tensPlace;
    [SerializeField] private SpriteRenderer hundredsPlace;

    private int _currentScore;
    public int CurrentScore => _currentScore;

    private int _highScore;
    public int HighScore => _highScore;

    private string HighScoreString = nameof(HighScore);
    
    private int _scoreOnesPlace;
    private int _scoreTensPlace;
    private int _scoreHundredsPlace;

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
        GetScoreDigits(_currentScore);
        UpdateScoreSprites();
    }

    private void IncreaseScore()
    {
        _currentScore++;
    }

    private void GetScoreDigits(int score)
    {
             _scoreOnesPlace = score % 10;
             score /= 10;
             _scoreTensPlace = score % 10;
             score /= 10;
             _scoreHundredsPlace = score % 10;
    }

    private void UpdateScoreSprites()
    {
        onesPlace.sprite = scoreSprites[_scoreOnesPlace];
        tensPlace.sprite = scoreSprites[_scoreTensPlace];
        hundredsPlace.sprite = scoreSprites[_scoreHundredsPlace];
    }

    private void CheckHighScore()
    {
        if (!(_currentScore > PlayerPrefs.GetFloat(HighScoreString))) return;
        
        _highScore = _currentScore;
        PlayerPrefs.SetFloat(HighScoreString, HighScore);
        brokenHighScore?.Invoke();
    }
}
