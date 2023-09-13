using TMPro;
using UnityEngine;
using Zenject;

public class ScoreView : MonoBehaviour
{
    private Score score;

    [SerializeField]
    private TMP_Text text;

    private void OnScorePointsChanged()
    {
        text.text = score.ScorePoints.ToString();
    }

    private void Start()
    {
        score.ScorePointsChanged += OnScorePointsChanged;

        OnScorePointsChanged();
    }

    private void OnDestroy()
    {
        score.ScorePointsChanged -= OnScorePointsChanged;
    }

    [Inject]
    private void Init(Score score)
    {
        this.score = score;
    }
}
