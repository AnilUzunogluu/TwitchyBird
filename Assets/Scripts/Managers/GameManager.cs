using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance => _instance;
    
    [SerializeField] private List<ScrollLeft> _scrollingObjects;

    private const int BRONZE_MEDAL_SCORE = 10;
    private const int SILVER_MEDAL_SCORE = 25;
    private const int GOLD_MEDAL_SCORE = 50;

    public int BronzeMedalScore => BRONZE_MEDAL_SCORE;
    public int SilverMedalScore => SILVER_MEDAL_SCORE;
    public int GoldMedalScore => GOLD_MEDAL_SCORE;

    public event Action OnGameOver;

    private void Start()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += GetScrollingObjects;
    }
    
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= GetScrollingObjects;
    }

    private void GetScrollingObjects(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Game")
        {
            _scrollingObjects = FindObjectsOfType<ScrollLeft>().ToList();
        }
    }

    public void SetGameOver()
    {
        foreach (var scrollingObject in _scrollingObjects)
        {
            scrollingObject.GameOver();
        }
        OnGameOver?.Invoke();
    }
}
