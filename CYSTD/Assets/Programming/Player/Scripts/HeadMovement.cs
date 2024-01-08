using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HeadMovement : MonoBehaviour
{
    const string HEAD_ACTION_MAP = "Head",
                 MOUSE_DELTA_ACTION = "MouseDelta";

    [SerializeField] InputActionAsset _inputActions;
    [SerializeField] [Range(0, 1)] float _rotationSensitivity;
    [SerializeField] [Range(0, 85)] float _maxUpAngle, _maxDownAngle;
    InputAction _mouseDelta;
    float _xRotation;

    void Start()
    {
        _mouseDelta = _inputActions.FindActionMap(HEAD_ACTION_MAP).FindAction(MOUSE_DELTA_ACTION);
    }

    void Update()
    {
        _ReadInput();
        _MoveHead();
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
        _xRotation = _mouseDelta.ReadValue<Vector2>().y;
    }

    void _MoveHead()
    {
        transform.Rotate(Vector3.right * -_xRotation * _rotationSensitivity);
        transform.rotation = transform.rotation.ClampRotationX(-_maxUpAngle, _maxDownAngle);
        transform.rotation = transform.rotation.SetZ(0);
    }
}
