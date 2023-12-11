using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    const string MOVEMENT_ACTION_MAP = "Movement",
                 LEFT_RIGHT_ACTION = "LeftRight",
                 BACKWARD_FORWARD_ACTION = "BackwardForward";
    const float speedMultiplicationFactor = 100;

    public InputActionAsset inputActions;
    public float speed;

    InputAction leftRightAction, backwardForwardAction;
    Rigidbody rb;
    float leftRight, backwardForward;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        InputActionMap movementActions = inputActions.FindActionMap(MOVEMENT_ACTION_MAP);
        leftRightAction = movementActions.FindAction(LEFT_RIGHT_ACTION);
        backwardForwardAction = movementActions.FindAction(BACKWARD_FORWARD_ACTION);
    }

    void Update()
    {
        ReadInput();
        Move(new Vector3(leftRight, 0f, backwardForward).normalized);
    }

    void ReadInput()
    {
        leftRight = leftRightAction.ReadValue<float>();
        backwardForward = backwardForwardAction.ReadValue<float>();
    }

    void Move(Vector3 direction)
    {
        Vector3 movement = direction * speed * speedMultiplicationFactor * Time.deltaTime;
        rb.velocity = Vector3.Lerp(rb.velocity, movement, 0.01f);
    }

    void OnEnable()
    {
        inputActions.Enable();
    }

    void OnDisable()
    {
        inputActions.Disable();
    }
}
