using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject gameOverScreen;
    
    [Header("Score")]
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI highScoreText;
    [SerializeField] private GameObject newHighScoreLabel;

    [Header("Medal")]
    [SerializeField] private GameObject medalObject;
    [SerializeField] private GameObject medalAnimationObject;
    [SerializeField] private Sprite bronzeMedal;
    [SerializeField] private Sprite silverMedal;
    [SerializeField] private Sprite goldMedal;

    
    private ScoreManager _scoreManager;

    private void Awake()
    {
        _scoreManager = FindObjectOfType<ScoreManager>();
    }

    private void OnEnable()
    {
        GameManager.Instance.OnGameOver += SetGameOverScreen;
        _scoreManager.brokenHighScore += ActivateNewHighScoreLabel;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnGameOver -= SetGameOverScreen;
        _scoreManager.brokenHighScore -= ActivateNewHighScoreLabel;
    }


    private void SetGameOverScreen()
    {
        if (gameOverScreen == null) return;
       
        gameOverScreen.SetActive(true);
        SetScoreTexts();
        CheckMedals();

    }

    private void CheckMedals()
    {
        if (PlayerPrefs.GetFloat("HighScore") > GameManager.Instance.GoldMedalScore)
        {
            medalObject.GetComponent<SpriteRenderer>().sprite = goldMedal;
            ActivateMedalAnimation();
        }
        else if (PlayerPrefs.GetFloat("HighScore") > GameManager.Instance.SilverMedalScore)
        {
            medalObject.GetComponent<SpriteRenderer>().sprite = silverMedal;
            ActivateMedalAnimation();
        }
        else if (PlayerPrefs.GetFloat("HighScore") > GameManager.Instance.BronzeMedalScore)
        {
            medalObject.GetComponent<SpriteRenderer>().sprite = bronzeMedal;
            ActivateMedalAnimation();
        }
    }

    private void ActivateMedalAnimation()
    {
        medalAnimationObject.GetComponent<Animator>().SetTrigger("hasMedal");
    }

    private void SetScoreTexts()
    {
        scoreText.text = _scoreManager.CurrentScore.ToString();
        highScoreText.text = PlayerPrefs.GetFloat("HighScore").ToString();
    }

    private void ActivateNewHighScoreLabel()
    {
        newHighScoreLabel.SetActive(true);
    }
}
