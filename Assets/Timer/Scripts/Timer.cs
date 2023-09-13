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

    private IEnumerator TimerCoroutine()
    {
        while (TimeLeft >= 0)
        {
            yield return new WaitForSeconds(timeStepInvoking);

            TimeLeft -= timeStepInvoking;
            TimeChanged?.Invoke();
        }

        TimeIsOver?.Invoke();
    }

    private void Start()
    {
        TimeLeft = maxTime;

        timerCoroutine = TimerCoroutine();
        StartCoroutine(timerCoroutine);
    }
}
