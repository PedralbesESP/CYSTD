using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class JumpingFrogPuzzle : Puzzle
{
    [SerializeField] GameObject _base;
    [SerializeField] List<JumpingFrogArm> _workingArms;
    [SerializeField] [Range(0.1f, 1f)] float turnSpeed;

    [SerializeField] [Range(10f, 50f)] float _armSpeed;
    [SerializeField] [Range(0f, 50f)] float _minArmAngle;
    [SerializeField] [Range(0f, 50f)] float _maxArmAngle;
    [SerializeField] [Range(1f, 2f)] float _forceScaleFactor;

    bool _isMoving;

    protected override void Start()
    {
        base.Start();
        _isMoving = false;
        _puzzleActions.FindAction("Action").performed += ToggleMoving;
        foreach (JumpingFrogArm arm in _workingArms) if (arm != null) arm.Init(_minArmAngle, _maxArmAngle, _forceScaleFactor);
    }

    void Update()
    {
        if (_isMoving) Move();
    }

    void ToggleMoving(InputAction.CallbackContext ctx)
    {
        ToggleMovingInternal();
    }

#if UNITY_EDITOR
    [ContextMenu("ToggleMovement")]
#endif
    void ToggleMovingInternal()
    {
        _isMoving = !_isMoving;
    }

    void Move()
    {
        Rotate();
        MoveArms();
    }

    void Rotate()
    {
        _base.transform.Rotate(Vector3.up, turnSpeed);
    }

    void MoveArms()
    {
        foreach (JumpingFrogArm arm in _workingArms) if (arm != null) arm.Move(_armSpeed);
    }
}
