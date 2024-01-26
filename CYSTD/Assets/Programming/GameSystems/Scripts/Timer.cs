using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField]
    float _secondsToEnd;
    [SerializeField]
    TMP_Text _timerText; 
    bool _last10Seconds = false;
    float _maxSeconds;

    public static Timer Instance;

    public float TimePercentage { get => (_secondsToEnd / _maxSeconds) * 100; }

    void Start()
    {
        Instance = this;
        _maxSeconds = _secondsToEnd;
        SetTime();
    }

    void Update()
    {
        _secondsToEnd -= Time.deltaTime;
        if (_secondsToEnd < 30)
        {
            _timerText.color = Color.red;
        }
        if (_secondsToEnd < 10 && !_last10Seconds)
        {
            _last10Seconds = true;
            StartCoroutine(Last10SecondsRoutine());
        }
        if (_secondsToEnd < 0)
        {
            _secondsToEnd = 0;
        }
        SetTime();
    }

    void LateUpdate()
    {
        if (_secondsToEnd <= 0)
        {
            GameManager.Instance.Loose();
        }
    }

    void SetTime()
    {
        TimeSpan time = TimeSpan.FromSeconds(_secondsToEnd);
        _timerText.SetText(time.ToString(@"hh\:mm\:ss"));
    }

    IEnumerator Last10SecondsRoutine()
    {
        while (_last10Seconds && _secondsToEnd >= 1)
        {
            _timerText.enabled = !_timerText.enabled;
            yield return new WaitForSecondsRealtime(0.3f);
        }
        _timerText.enabled = true;
    }


    public void AddTime(float secondsToAdd)
    {
        if (_secondsToEnd > 0)
        {
            _secondsToEnd += secondsToAdd;
            if (_secondsToEnd > _maxSeconds) _secondsToEnd = _maxSeconds;
            if (_secondsToEnd >= 10)
            {
                _last10Seconds = true;
            }
            if (_secondsToEnd >= 30)
            {
                _timerText.color = Color.white;
            }
        }
    }
}
