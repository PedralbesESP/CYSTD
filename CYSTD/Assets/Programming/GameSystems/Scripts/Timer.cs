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

    void Start()
    {
        SetTime();
    }

    void Update()
    {
        _secondsToEnd -= Time.deltaTime;
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
}
