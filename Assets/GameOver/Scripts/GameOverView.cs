using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class GameOverView : MonoBehaviour
{
    [SerializeField]
    private GameObject panel;
    [SerializeField]
    private TMP_Text currentScore;
    [SerializeField]
    private TMP_Text bestScore;

    private Timer timer;
    private Score score;

    public void Restart()
    {
        var scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    private void OnTimeIsOver()
    {
        panel.SetActive(true);

        currentScore.text = score.ScorePoints.ToString();
        bestScore.text = score.BestScore.ToString();
    }

    private void OnDestroy()
    {
        timer.TimeIsOver -= OnTimeIsOver;
    }

    private void Start()
    {
        panel.SetActive(false);
        timer.TimeIsOver += OnTimeIsOver;
    }

    [Inject]
    private void Init(Timer timer, Score score)
    {
        this.timer = timer;
        this.score = score;
    }
}
