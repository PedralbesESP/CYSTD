using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkStagesManager : MonoBehaviour
{
    [SerializeField]
    Timer _timer;
    DarkStage _currentStage;

    void Start()
    {
        _currentStage = GetStage();
        _currentStage.StartStage();
    }

    void Update()
    {
        DarkStage nextStage = GetStage();
        if (nextStage.GetType() != _currentStage.GetType())
        {
            _currentStage.EndStage();
            _currentStage = nextStage;
            _currentStage.StartStage();
        }
        _currentStage.Update();
        Debug.Log(_currentStage.GetType());
    }

    DarkStage GetStage()
    {
        float percentage = _timer.TimePercentage;
        if (percentage < 10) return new Stage10();
        if (percentage < 20) return new Stage20();
        if (percentage < 30) return new Stage30();
        if (percentage < 40) return new Stage40();
        if (percentage < 50) return new Stage50();
        if (percentage < 60) return new Stage60();
        if (percentage < 70) return new Stage70();
        if (percentage < 80) return new Stage80();
        if (percentage < 90) return new Stage90();
        return new NormalStage();
    }
}
