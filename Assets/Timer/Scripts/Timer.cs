using System;
using System.Collections;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField]
    private float maxTime;
    [SerializeField]
    private float timeStepInvoking;

    private IEnumerator timerCoroutine;

    public float TimeLeft { get; private set; }

    public float MaxTime => maxTime;

    public event Action TimeChanged;

    public event Action TimeIsOver;

    public event Action TimeAdded;

    public void AddTime(float time)
    {
        TimeLeft += time;

        StartTimer();
        TimeAdded?.Invoke();
        TimeChanged?.Invoke();
    }

    private void StartTimer()
    {
        if (timerCoroutine != null)
            return;

        timerCoroutine = TimerCoroutine();
        StartCoroutine(timerCoroutine);
    }

    private IEnumerator TimerCoroutine()
    {
        while (TimeLeft >= 0)
        {
            yield return new WaitForSeconds(timeStepInvoking);

            TimeLeft -= timeStepInvoking;
            TimeChanged?.Invoke();
        }

        TimeIsOver?.Invoke();

        timerCoroutine = null;
    }

    private void Start()
    {
        TimeLeft = maxTime;

        StartTimer();
    }
}
