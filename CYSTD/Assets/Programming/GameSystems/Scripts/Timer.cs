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

    public static Timer Instance;

    void Start()
    {
        Instance = this;
        SetTime();
    }

    void Update()
    {
        _secondsToEnd -= Time.deltaTime;
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

    public void AddTime(float secondsToAdd)
    {
        if (_secondsToEnd > 0)
        {
            _secondsToEnd += secondsToAdd;
        }
    }
}
