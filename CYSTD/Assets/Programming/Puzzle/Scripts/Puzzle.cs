using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class Puzzle : MonoBehaviour
{
    [SerializeField]
    InputActionAsset _inputActions;
    [SerializeField]
    public Transform _positionToDoPuzzle;
    [SerializeField]
    protected string _hint;

    protected InputActionMap _puzzleActions;
    InputActionMap _movementActions;
    bool _isActive = false;
    protected PuzzleState _state = PuzzleState.NOT_DONE;

    public PuzzleState State { get => _state; set { _state = value; } }
    public bool IsActive { get => _isActive; set { _isActive = value; } }
    public bool IsCompleted { get => _state == PuzzleState.COMPLETED; }
    public bool IsFailed { get => _state == PuzzleState.FAILED; }

    protected virtual void Start()
    {
        _puzzleActions = _inputActions.FindActionMap("Puzzle");
        _movementActions = _inputActions.FindActionMap("Movement");
        _puzzleActions.Disable();
    }

    public virtual void Activate()
    {
        IsActive = true;
        GameManager.Instance.getPlayer().GetComponent<Rigidbody>().velocity = Vector3.zero;
        GameManager.Instance.getPlayer().transform.SetPositionAndRotation(_positionToDoPuzzle.position, _positionToDoPuzzle.rotation);
        _movementActions.Disable();
        _puzzleActions.Enable();
    }

    public virtual void Deactivate()
    {
        _puzzleActions.Disable();
        _movementActions.Enable();
        IsActive = false;
    }

    public virtual void Complete()
    {
        _state = PuzzleState.COMPLETED;
        Deactivate();
    }

    public virtual void Fail()
    {
        _state = PuzzleState.FAILED;
    }

    public virtual void ResetPuzzle()
    {
        _state = PuzzleState.NOT_DONE;
    }
}
