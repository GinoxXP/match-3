using System;
using UnityEngine;

public class Score : MonoBehaviour
{
    private static readonly string keyBestScore = "BEST_SCORE";

    private int scorePoints;

    public int ScorePoints
    {
        private set
        {
            scorePoints = value;

            if (scorePoints > BestScore)
                BestScore = scorePoints;
        }

        get => scorePoints;
    }

    public int BestScore
    {
        private set => PlayerPrefs.SetInt(keyBestScore, value);

        get => PlayerPrefs.GetInt(keyBestScore);
    }

    public event Action ScorePointsChanged;

    public void CommitDestroyedChipsCount(int count)
    {
        if (count == 3)
            ScorePoints += count;
        else
            ScorePoints += count * count - 2;

        ScorePointsChanged?.Invoke();
    }
}
