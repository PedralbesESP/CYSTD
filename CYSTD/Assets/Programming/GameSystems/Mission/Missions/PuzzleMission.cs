using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class PuzzleMission : BaseMission
{
    [SerializeField]
    Puzzle _puzzle;

    public void StartMission()
    {
        if (!_enabled) return;
        SetMissionState(MissionState.DOING);
        if (_puzzle != null)
        {
            _puzzle.Activate();
        }
    }

    void Update()
    {
        if (_state == MissionState.DOING || _state == MissionState.FAILED)
        {
            if (_puzzle.IsCompleted)
            {
                _CompleteMission();
                ExitMission();
                return;
            }
            if (_puzzle.IsFailed)
            {
                SetMissionState(MissionState.FAILED);
                ExitMission();
            }
        }
    }

    public void ExitMission()
    {
        if (!_puzzle.IsFailed) SetMissionState(MissionState.NOT_DONE);
        _puzzle.Deactivate();
    }

    public override void Enable()
    {
        base.Enable();
        gameObject.GetComponent<BoxCollider>().enabled = true;
        _puzzle.enabled = true;
    }

    public override void Disable()
    {
        base.Disable();
        gameObject.GetComponent<BoxCollider>().enabled = false;
        _puzzle.enabled = false;
    }

    public override void ResetMission()
    {
        base.ResetMission();
        _puzzle.ResetPuzzle();
    }
}
