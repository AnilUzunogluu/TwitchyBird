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

    public int BronzeMedalScore { get; private set; }
    public int SilverMedalScore { get; private set; }
    public int GoldMedalScore { get; private set; }

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

    private void SetGameStart()
    {
        
    }

    public void SetGameOver()
    {
        foreach (var scrollingObject in _scrollingObjects)
        {
            scrollingObject.GameOver();
        }
        OnGameOver?.Invoke();
    }

    private void SetMedalScores()
    {
        BronzeMedalScore = 2;
        SilverMedalScore = 4;
        GoldMedalScore = 6;

    }
}
