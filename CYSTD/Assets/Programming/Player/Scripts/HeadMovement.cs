using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HeadMovement : MonoBehaviour
{
    const string HEAD_ACTION_MAP = "Head",
                 MOUSE_DELTA_ACTION = "MouseDelta";

    [SerializeField] InputActionAsset _inputActions;
    InputAction _mouseDelta;
    float _xRotation;

    void Start()
    {
        _mouseDelta = _inputActions.FindActionMap(HEAD_ACTION_MAP).FindAction(MOUSE_DELTA_ACTION);
    }

    void Update()
    {
        _ReadInput();
        //_MoveHead();
    }

    void OnEnable()
    {
        _inputActions.Enable();
    }

    private void OnDisable()
    {
        _inputActions.Disable();
    }

    void _ReadInput()
    {
        _xRotation = _mouseDelta.ReadValue<Vector2>().x;
        _xRotation = Mathf.Clamp(_xRotation, -90.0f, 90.0f);
    }

    void _MoveHead()
    {
        transform.localRotation = Quaternion.Euler(_xRotation, 0.0f, 0.0f);
    }
}
