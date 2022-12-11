using System;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private ScoreManager scoreManager;

    [Header("Score")]
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI highScoreText;
    [SerializeField] private GameObject newHighScoreLabel;

    [Header("Medal")]
    [SerializeField] private SpriteRenderer medalObjectSpriteRenderer;
    [SerializeField] private Animator medalAnimator;
    [SerializeField] private Sprite bronzeMedal;
    [SerializeField] private Sprite silverMedal;
    [SerializeField] private Sprite goldMedal;
    

    private void OnEnable()
    {
        GameManager.Instance.OnGameOver += SetGameOverScreen;
        scoreManager.OnHighScoreChanged += ActivateNewHighScoreChangedLabel;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnGameOver -= SetGameOverScreen;
        scoreManager.OnHighScoreChanged -= ActivateNewHighScoreChangedLabel;
    }
    
    private void SetGameOverScreen()
    {
        gameOverScreen.SetActive(true);
        SetScoreTexts();
        SetMedals();
    }

    private void SetMedals()
    {
        if (!GameManager.TryGetMedal(out var medal)) return;
        
        medalObjectSpriteRenderer.sprite = GetMedalSprite(medal);
        ActivateMedalAnimation();
    }
    
    private Sprite GetMedalSprite(Medal medal)
    {
        switch (medal)
        {
            case Medal.None:
                return null;
            case Medal.Bronze:
                return bronzeMedal;
            case Medal.Silver:
                return silverMedal;
            case Medal.Gold:
                return goldMedal;
            default:
                throw new ArgumentOutOfRangeException(nameof(medal), medal, null);
        }
    }
    
    private void ActivateMedalAnimation()
    {
        medalAnimator.SetTrigger("hasMedal");
    }

    private void SetScoreTexts()
    {
        scoreText.text = scoreManager.CurrentScore.ToString();
        highScoreText.text = PlayerStats.HighScore.ToString();
    }

    private void ActivateNewHighScoreChangedLabel()
    {
        newHighScoreLabel.SetActive(true);
    }
}
