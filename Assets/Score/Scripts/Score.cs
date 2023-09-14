using System;
using UnityEngine;
using Zenject;

public class Score : MonoBehaviour
{
    private int scorePoints;

    private ISaveLoadService saveLoadService;

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
        private set => saveLoadService.SaveBestScore(value);

        get => saveLoadService.LoadBestScore();
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

    [Inject]
    private void Init(ISaveLoadService saveLoadService)
    {
        this.saveLoadService = saveLoadService;
    }
}
