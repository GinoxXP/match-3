using System;
using UnityEngine;

public class Score : MonoBehaviour
{
    public int ScorePoints { private set; get; } = 0;

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
