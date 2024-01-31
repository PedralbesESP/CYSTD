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
        if (_orderedSubmissions[_currentPos].IsCompleted) 
        {
            _currentPos++;
            if (_currentPos == _orderedSubmissions.Count)
            {
                _CompleteMission();
                return;
            }
            _orderedSubmissions[_currentPos].Enable();
        }
    }

    public override string GetMissionExplaination()
    {
        if (_currentPos >= _orderedSubmissions.Count) return null;
        return _orderedSubmissions[_currentPos].GetMissionExplaination();
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
