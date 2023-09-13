using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class TimerView : MonoBehaviour
{
    [SerializeField]
    private TMP_Text text;
    [SerializeField]
    private Slider slider;

    private Timer timer;

    private void OnTimeChanged()
    {
        text.text = $"{timer.TimeLeft} c.";
        slider.value = timer.TimeLeft / timer.MaxTime;
    }

    private void Start()
    {
        timer.TimeChanged += OnTimeChanged;
    }

    private void OnDestroy()
    {
        timer.TimeChanged -= OnTimeChanged;
    }

    [Inject]
    private void Init(Timer timer)
    {
        this.timer = timer;
    }
}
