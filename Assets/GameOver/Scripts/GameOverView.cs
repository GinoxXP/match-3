using TMPro;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

[RequireComponent(typeof(CanvasGroup))]
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
    private CanvasGroup canvasGroup;

    public void Restart()
    {
        var scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    private void OnTimeIsOver()
    {
        canvasGroup.alpha = 0;
        canvasGroup.DOFade(1, 0.2f);

        panel.SetActive(true);

        currentScore.text = score.ScorePoints.ToString();
        bestScore.text = score.BestScore.ToString();
    }

    private void OnTimeAdded()
    {
        panel.SetActive(false);
    }

    private void OnDestroy()
    {
        timer.TimeIsOver -= OnTimeIsOver;
    }

    private void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();

        panel.SetActive(false);
        timer.TimeIsOver += OnTimeIsOver;
        timer.TimeAdded += OnTimeAdded;
    }

    [Inject]
    private void Init(Timer timer, Score score)
    {
        this.timer = timer;
        this.score = score;
    }
}
