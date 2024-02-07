using FMOD.Studio;
using TMPro;
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
                 MOUSE_DELTA_ACTION = "MouseDelta",
                 RUN_ACTION = "Run",
                 CROUCH_ACTION = "Crouch";
    const float FORCE_SCALE_FACTOR = 100;

    private EventInstance _playerFootSteps;

    [SerializeField] InputActionAsset _inputActions;
    [SerializeField] float _speed, _jumpForce;
    [SerializeField] [Range(0, 1)] float _rotationSensitivity;
    [SerializeField] [Range(1, 3)] float _runIncrementFactor;
    [SerializeField] [Range(0.6f, 1)] float _crouchDecrementFactor;
    [SerializeField] float _maxRunTime;
    [SerializeField] LayerMask _walkableLayer;
    [SerializeField] TMP_Text _staminaTxt;
    InputAction _leftRightAction, _backwardForwardAction, _yDelta;
    Rigidbody _rigidbody;
    float _leftRight, _backwardForward, _yRotation;
    PlayerState _currentState;
    bool _isRunning;
    bool _isCrouching;
    float _currentRunTime;
    

    private void Awake()
    {
        GameManager.Instance.setPlayer(this.gameObject);
    }
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
    public float RunIncrementFactor { get => _runIncrementFactor; set => _runIncrementFactor = value; }
    public float CrouchDecrementFactor { get => _crouchDecrementFactor; set => _crouchDecrementFactor = value; }
    public bool IsRunning { get => _isRunning; set => _isRunning = value; }
    public bool IsCrouching { get => _isCrouching; set => _isCrouching = value; }
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
    public LayerMask WalkableLayer { get => _walkableLayer; }
    public float LerpFactor { get => 0.03f; }
    int StaminaPercentage { get => Mathf.RoundToInt((_currentRunTime / _maxRunTime) * 100); }
    public InputActionAsset Input { get => _inputActions; }

    void Start()
    {
        _currentRunTime = _maxRunTime;
        _rigidbody = GetComponent<Rigidbody>();
        InputActionMap movementActions = _inputActions.FindActionMap(MOVEMENT_ACTION_MAP);
        _leftRightAction = movementActions.FindAction(LEFT_RIGHT_ACTION);
        _backwardForwardAction = movementActions.FindAction(BACKWARD_FORWARD_ACTION);
        _yDelta = _inputActions.FindActionMap(HEAD_ACTION_MAP).FindAction(MOUSE_DELTA_ACTION);
        movementActions.FindAction(JUMP_ACTION).performed += _Jump;
        _playerFootSteps = AudioManager.audioManager.CreateEventInstance(FMODEvents.instance.PlayerSteps);
        _playerFootSteps.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(this.gameObject));
        InputAction runAction = movementActions.FindAction(RUN_ACTION);
        runAction.started += _StartRun;
        runAction.canceled += _StopRun;
        InputAction crouchAction = movementActions.FindAction(CROUCH_ACTION);
        crouchAction.started += _StartCrouch;
        crouchAction.canceled += _StopCrouch;

        CurrentState = new IdlePlayerState();
    }

    void Update()
    {
        _GetInput();
        _currentState.Update();
        _Rotate();
        _HandleRun();
        //Debug.Log(_currentRunTime);
        //Debug.Log("State ->" + _currentState.GetType().Name);
    }

    void LateUpdate()
    {
        PlayerState newState = _currentState.CheckTransition();
        if (newState != null)
        {
            CurrentState = newState;
        }
        checkState();
    }

    void checkState()
    {
        switch (CurrentState)
        {
            case WalkPlayerState walkPlayerState:
                UpdateSound(walkPlayerState);
                break;
            case IdlePlayerState idlePlayerState: UpdateSound(idlePlayerState); break;
            case JumpPlayerState jumpPlayerState: UpdateSound(jumpPlayerState); break;
        }
    }
    /*
     void UpdateSound1(PlayerState currentState)
    {
        if (_playerMovement.Direction != Vector3.zero)
        {
            PLAYBACK_STATE playbackState;
            _playerFootSteps.getPlaybackState(out playbackState);
            if (playbackState.Equals(PLAYBACK_STATE.STOPPED))
            {
                _playerFootSteps.start();
                return;
            }
        }
        else
        {
            _playerFootSteps.stop(STOP_MODE.IMMEDIATE);
        }
    }
    */
    void UpdateSound(PlayerState currentState)
    {
        _playerFootSteps.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(this.gameObject));
        if (currentState is WalkPlayerState)
        {
            PLAYBACK_STATE playbackState;
            _playerFootSteps.getPlaybackState(out playbackState);
            if (playbackState.Equals(PLAYBACK_STATE.STOPPED))
            {
                _playerFootSteps.start();
                return;
            }
        }
        else
        {
            _playerFootSteps.stop(STOP_MODE.IMMEDIATE);
        }
    }

    void OnEnable()
    {
        _inputActions.Enable();
    }

    void OnDestroy()
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

    void _HandleRun() 
    {
        if (IsRunning)
        {
            _currentRunTime -= Time.deltaTime;
            if (_currentRunTime <= 0)
            {
                _currentRunTime = 0;
                IsRunning = false;
            } 
        }
        else if (_currentRunTime < _maxRunTime)
        {
            _currentRunTime += Time.deltaTime;
            if (_currentRunTime > _maxRunTime)
            {
                _currentRunTime = _maxRunTime;
            }
        }
        _staminaTxt.SetText($"{StaminaPercentage}%");
    }

    void _StartRun(InputAction.CallbackContext ctx)
    {
        IsRunning = true;
    }

    void _StopRun(InputAction.CallbackContext ctx)
    {
        IsRunning = false;
    }

    void _StartCrouch(InputAction.CallbackContext ctx)
    {
        IsCrouching = true;
    }

    void _StopCrouch(InputAction.CallbackContext ctx)
    {
        IsCrouching = false;
    }

    private void OnDrawGizmos()
    {
        Vector3 size = new Vector3(0.4f, 0.1f, 0.4f);
        bool test = Physics.OverlapBox(gameObject.transform.position, size, Quaternion.identity, WalkableLayer).Length > 0;
        Gizmos.color = test ? Color.green : Color.red;
        Gizmos.DrawWireCube(gameObject.transform.position, size);
    }
}
