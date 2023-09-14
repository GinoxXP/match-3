using UnityEngine;

public class LocalSaveLoadService : ISaveLoadService
{
    private static readonly string keyBestScore = "BEST_SCORE";

    public int LoadBestScore()
        => PlayerPrefs.GetInt(keyBestScore);

    public void SaveBestScore(int value)
        => PlayerPrefs.SetInt(keyBestScore, value);
}
