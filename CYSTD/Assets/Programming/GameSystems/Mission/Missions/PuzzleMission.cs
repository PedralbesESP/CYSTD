using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class PuzzleMission : BaseMission
{
    [SerializeField]
    Transform _positionToDoPuzzle;

    public void StartMission()
    {
        if (!_enabled) return;
        SetMissionState(MissionState.DOING);
        if (_positionToDoPuzzle != null)
        {
            GameManager.Instance.getPlayer().transform.SetPositionAndRotation(_positionToDoPuzzle.position, _positionToDoPuzzle.rotation);
            GameManager.Instance.getPlayer().GetComponent<PlayerMovement>().Input.FindActionMap("Movement").Disable();
        }
    }

    public void ExitMission()
    {
        SetMissionState(MissionState.NOT_DONE);
        GameManager.Instance.getPlayer().GetComponent<PlayerMovement>().Input.FindActionMap("Movement").Enable();
    }

    public override void Enable()
    {
        base.Enable();
        gameObject.GetComponent<BoxCollider>().enabled = true;
    }

    public override void Disable()
    {
        base.Disable();
        gameObject.GetComponent<BoxCollider>().enabled = false;
    }
}
