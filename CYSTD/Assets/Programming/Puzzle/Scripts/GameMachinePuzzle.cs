using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameMachinePuzzle : Puzzle
{
    [SerializeField]
    Transform _endPosition;
    [SerializeField]
    GameObject _machineBase;
    [SerializeField]
    GameObject _hook;
    [SerializeField]
    GameObject _prize;
    [SerializeField] [Range(0.1f, 1f)]
    float _movementSpeed;
    bool _isLowering = false;
    Vector3 _startHookPosition;
    InputAction leftRight;
    InputAction forwardBackward;
    bool _isWon = false;

    void Awake()
    {
        PlacePrize();
    }

    protected override void Start()
    {
        base.Start();
        leftRight = _puzzleActions.FindAction("LeftRight");
        forwardBackward = _puzzleActions.FindAction("ForwardBackward");
        _puzzleActions.FindAction("Action").performed += LowerHook;
        _startHookPosition = _hook.transform.position;
        _hook.transform.rotation = transform.rotation;
    }

    void Update()
    {
        if (!IsActive) return;
        if (_isWon)
        {
            //GrabPrize();
            _prize.SetActive(false);
            Complete();
            return;
        }
        if (_isLowering)
        {
            bool? result = GetResult();
            if (result == null) MoveHook();
            else SetResult(result.Value);
            return;
        }
        MoveHook();
    }

    void PlacePrize()
    {
        Bounds endPositionBounds = _endPosition.gameObject.GetComponent<BoxCollider>().bounds;
        Vector3 pos;
        do
        {
            pos = GetRandomPosition();
        } while (endPositionBounds.Contains(pos));
        
        Quaternion rot = GetRandomRotation();
        _prize.transform.SetLocalPositionAndRotation(pos, rot);
    }

    Vector3 GetRandomPosition()
    {
        float xScale = _machineBase.transform.localScale.x / 2f;
        float yScale = _machineBase.transform.localScale.z / 2f;
        float x = Random.Range(-xScale, xScale);
        float z = Random.Range(-yScale, yScale);
        return new Vector3(x, _prize.transform.position.y, z);
    }

    Quaternion GetRandomRotation()
    {
        Vector3 v = new Vector3
            (
            _prize.transform.rotation.eulerAngles.x,
            Random.Range(-180, 180),
            _prize.transform.rotation.eulerAngles.z
            );
        return Quaternion.Euler(v.x, v.y, v.z);
    }

    Vector3 GetInput()
    {
        return new Vector3(leftRight.ReadValue<float>(), 0, forwardBackward.ReadValue<float>());
    }

    void MoveHook()
    {
        if (_isLowering) _hook.transform.Translate(0, -_movementSpeed * Time.deltaTime, 0);
        else 
        {
            Vector3 input = _movementSpeed * Time.deltaTime * GetInput();
            _hook.transform.Translate(-input.x, 0, -input.z);
        }
    }

    void LowerHook(InputAction.CallbackContext ctx)
    {
        if (_isLowering) return;
        else _isLowering = true;
    }

    bool? GetResult()
    {
        CapsuleCollider collider = _hook.GetComponent<CapsuleCollider>();
        List<Collider> overlappingColliders = new List<Collider>(Physics.OverlapCapsule(
            collider.bounds.center,
            collider.bounds.center + new Vector3(0f, collider.height, 0f),
            collider.radius
        ));
       
        if (overlappingColliders.Count == 0) return null;
        if (overlappingColliders.Any(c => c.gameObject.CompareTag(_prize.tag))) return true;
        else if (overlappingColliders.Any(c => c.gameObject.CompareTag(_machineBase.tag))) return false;
        else return null;
    }

    void GrabPrize()
    {
        if (!_isWon) 
        {
            _prize.transform.parent = _hook.transform;
            _isWon = true;
            return;
        }
        List<Collider> overlappingColliders = 
            new List<Collider>(Physics.OverlapBox(_endPosition.transform.position, new Vector3(2, 2, 0.1f), _machineBase.transform.rotation));
        if (overlappingColliders.Any(c => c.gameObject.CompareTag(_prize.tag)))
        {
            _prize.SetActive(false);
            Complete();
            return;
        }
        _hook.transform.position = Vector3.MoveTowards(_hook.transform.position, _endPosition.position, _movementSpeed * Time.deltaTime);
    }

    void SetResult(bool result)
    {
        if (result) GrabPrize();
        else Fail();
    }

    public override void ResetPuzzle()
    {
        base.ResetPuzzle();
        _isLowering = false;
        _isWon = false;
        _hook.transform.position = _startHookPosition;
    }
}
