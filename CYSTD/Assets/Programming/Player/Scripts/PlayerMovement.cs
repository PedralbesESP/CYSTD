using FMOD.Studio;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    const string MOVEMENT_ACTION_MAP = "Movement",
                 LEFT_RIGHT_ACTION = "LeftRight",
                 BACKWARD_FORWARD_ACTION = "BackwardForward",
                 JUMP_ACTION = "Jump",
                 HEAD_ACTION_MAP = "Head",
                 MOUSE_DELTA_ACTION = "MouseDelta";
    const float FORCE_SCALE_FACTOR = 100;

    [SerializeField] InputActionAsset _inputActions;
    [SerializeField] float _speed, _jumpForce;
    [SerializeField][Range(0, 1)] float _rotationSensitivity;
    InputAction _leftRightAction, _backwardForwardAction, _yDelta;
    Rigidbody _rigidbody;
    float _leftRight, _backwardForward, _yRotation;
    PlayerState _currentState;

    public PlayerState CurrentState
    {
        get => _currentState;
        set
        {
            _currentState = value;
            _currentState.Start(gameObject, _rigidbody, this);
        }
    }
    public float Speed { get => _speed; }
    public float JumpForce { get => _jumpForce * FORCE_SCALE_FACTOR; }
    public Vector3 Direction
    {
        get
        {
            Vector3 dir = new Vector3(_leftRight, 0f, _backwardForward);
            if (dir == Vector3.zero)
            {
                return dir;
            }
            return transform.TransformDirection(dir);
        }
    }

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        InputActionMap movementActions = _inputActions.FindActionMap(MOVEMENT_ACTION_MAP);
        _leftRightAction = movementActions.FindAction(LEFT_RIGHT_ACTION);
        _backwardForwardAction = movementActions.FindAction(BACKWARD_FORWARD_ACTION);
        _yDelta = _inputActions.FindActionMap(HEAD_ACTION_MAP).FindAction(MOUSE_DELTA_ACTION);
        movementActions.FindAction(JUMP_ACTION).performed += _Jump;

        CurrentState = new IdlePlayerState();
    }

    void Update()
    {
        _GetInput();
        _currentState.Update();
        _Rotate();
        Debug.Log("State ->" + _currentState.GetType().Name);
    }

    void LateUpdate()
    {
        PlayerState newState = _currentState.CheckTransition();
        if (newState != null)
        {
            CurrentState = newState;
        }
    }

    void OnEnable()
    {
        _inputActions.Enable();
    }

    void OnDisable()
    {
        _inputActions.FindActionMap(MOVEMENT_ACTION_MAP).FindAction(JUMP_ACTION).performed -= _Jump;
        _inputActions.Disable();
    }

    void _Jump(InputAction.CallbackContext ctx)
    {
        CurrentState = new JumpPlayerState(_rigidbody.velocity);
    }

    void _Rotate()
    {
        transform.Rotate(Vector3.up * _yRotation * _rotationSensitivity);
    }

    void _GetInput()
    {
        _leftRight = _leftRightAction.ReadValue<float>();
        _backwardForward = _backwardForwardAction.ReadValue<float>();
        _yRotation = _yDelta.ReadValue<Vector2>().x;
    }
}
