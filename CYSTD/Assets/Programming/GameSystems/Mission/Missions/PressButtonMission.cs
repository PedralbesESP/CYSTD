using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PressButtonMission : InteractionMissionObject
{
    [SerializeField]
    InputActionAsset _inputActions;

    void OnEnable()
    {
        _inputActions.Enable();
    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        if (_hasPlayerNear)
        {
            _inputActions.FindActionMap("Interaction").FindAction("MainInteraction").performed += ButtonPressed;
        }
    }

    protected override void OnTriggerExit(Collider other)
    {
        if (_hasPlayerNear)
        {
            _inputActions.FindActionMap("Interaction").FindAction("MainInteraction").performed -= ButtonPressed;
        }
        base.OnTriggerExit(other);
    }

    void ButtonPressed(InputAction.CallbackContext ctx)
    {
        InteractionOptions.Instance.Deactivate();
        _CompleteMission();
    }
}
