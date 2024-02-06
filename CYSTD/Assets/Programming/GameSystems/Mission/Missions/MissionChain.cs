using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionChain : BaseMission
{
    [SerializeField]
    List<BaseMission> _orderedSubmissions;
    int _currentPos;

    void Start()
    {
        _orderedSubmissions ??= new List<BaseMission>();
        if (_orderedSubmissions.Count > 1) _orderedSubmissions.GetRange(1, _orderedSubmissions.Count - 1).ForEach(m => m.Disable());
        _currentPos = 0;
    }

    void Update()
    {
        if (!UpdateValidations()) return;
        BaseMission currentMission = _orderedSubmissions[_currentPos];
        if (currentMission.IsFailed) PreviousMission();
        else if (currentMission.IsCompleted) NextMission();
    }

    public override string GetMissionExplaination()
    {
        if (_currentPos >= _orderedSubmissions.Count) return null;
        return _orderedSubmissions[_currentPos].GetMissionExplaination();
    }

    void PreviousMission()
    {
        _orderedSubmissions[_currentPos].ResetMission();
        _orderedSubmissions[_currentPos].Disable();
        _currentPos--;
        _orderedSubmissions[_currentPos].Enable();
        _orderedSubmissions[_currentPos].ResetMission();
    }

    void NextMission() 
    {
        _currentPos++;
        if (_currentPos == _orderedSubmissions.Count)
        {
            _CompleteMission();
            return;
        }
        _orderedSubmissions[_currentPos].Enable();
    }

    public override void Enable()
    {
        base.Enable();
        if (_currentPos < _orderedSubmissions.Count) _orderedSubmissions[_currentPos].Enable();
    }

    public override void Disable()
    {
        base.Disable();
        if (_currentPos < _orderedSubmissions.Count) _orderedSubmissions[_currentPos].Disable();
    }
}
