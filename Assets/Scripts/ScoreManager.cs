using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private Sprite[] scoreSprites;
    [SerializeField] private SpriteRenderer onesPlace;
    [SerializeField] private SpriteRenderer tensPlace;
    [SerializeField] private SpriteRenderer hundredsPlace;

    private int _score;
    public int Score => _score;
    
    
    private int _scoreOnesPlace;
    private int _scoreTensPlace;
    private int _scoreHundredsPlace;


    public void GotPoint()
    {
        IncreaseScore();
        GetScoreDigits(_score);
        UpdateScoreSprites();
    }

    private void IncreaseScore()
    {
        _score++;
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
}
