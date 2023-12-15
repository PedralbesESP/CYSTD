using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    const string MOVEMENT_ACTION_MAP = "Movement",
                        LEFT_RIGHT_ACTION = "LeftRight",
                        BACKWARD_FORWARD_ACTION = "BackwardForward",
                        JUMP_ACTION = "Jump";
    const float SPEED_SCALE_FACTOR = 1000, FORCE_SCALE_FACTOR = 5000;

    [SerializeField] InputActionAsset _inputActions;
    [SerializeField] float _speed, _jumpForce;
    bool _isLocalPlayer = true;  // true FOR TESTING
    InputAction _leftRightAction, _backwardForwardAction;
    Rigidbody _rigidbody;
    float _leftRight, _backwardForward;
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
    public float Speed { get => _speed * SPEED_SCALE_FACTOR; }
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
        movementActions.FindAction(JUMP_ACTION).performed += Jump;

        CurrentState = new IdlePlayerState();
    }

    void Update() 
    {
        _GetInput();
        _currentState.Update();
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
        _inputActions.FindActionMap(MOVEMENT_ACTION_MAP).FindAction(JUMP_ACTION).performed -= Jump;
        _inputActions.Disable();
    }

    public void Jump(InputAction.CallbackContext ctx)
    {
        CurrentState = new JumpPlayerState(_rigidbody.velocity);
    }

    void _GetInput()
    {
        if (_isLocalPlayer)
        {
            _leftRight = _leftRightAction.ReadValue<float>();
            _backwardForward = _backwardForwardAction.ReadValue<float>();
        }
    }
}
