using UnityEngine;

public static class PlayerStats
{
    
    private const string HIGH_SCORE_STRING = "HighScore";
        
    public static int HighScore
    {
        get => GetHighScore();
        set => SetHighScore(value);
    }

    private static int GetHighScore()
    {
        return PlayerPrefs.GetInt(HIGH_SCORE_STRING, 0);
    }

    private static void SetHighScore(int value)
    {
        PlayerPrefs.SetInt(HIGH_SCORE_STRING, value);
    }
}