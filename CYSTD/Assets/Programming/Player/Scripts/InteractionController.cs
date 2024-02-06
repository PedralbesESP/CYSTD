using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CapsuleCollider))]
public class InteractionController : MonoBehaviour
{
    [SerializeField]
    InputActionAsset _inputActions;
    string _grabHint;
    string _useHint;
    string _puzzleHint;
    string _exitPuzzleHint;
    string _grabIssue;
    string _missionIssue;
    string _itemsUsedBase;
    BaseMission _nearestMission;
    float _missionDistance = float.MaxValue;
    GameObject _nearestItemOnReach;
    float _itemDistance = float.MaxValue;
    bool _canGrab;
    bool _canUse;
    bool _canPuzzle;

    void Awake()
    {
        _inputActions.Enable();
        string binding = _inputActions.FindActionMap("Interaction").FindAction("MainInteraction").GetBindingDisplayString().ToUpper();
        string exitBinding = _inputActions.FindActionMap("Interaction").FindAction("ExitPuzzleInteraction").GetBindingDisplayString().ToUpper();
        _grabHint = $"Press " + binding + " to pick up";
        _useHint = $"Press " + binding + " to use items";
        _grabIssue = "Can't grab item";
        _missionIssue = "You don't have the necessary items";
        _itemsUsedBase = "Items used: ";
        _puzzleHint = "Press " + binding + " to use puzzle";
        _exitPuzzleHint = "Press " + exitBinding + " to use exit";
    }

    void Update()
    {
        if (_nearestMission != null && (_nearestMission.IsCompleted || !_nearestMission.IsEnabled() || !_nearestMission.enabled))
        {
            _nearestMission = null;
            _missionDistance = float.MaxValue;
            if (_canPuzzle) DeactivatePuzzle();
            if (_canUse) DeactivateUse();
            StartCoroutine(FindOtherActions());
        }
        if (_nearestItemOnReach != null && !_nearestItemOnReach.activeInHierarchy)
        {
            _nearestItemOnReach = null;
            _itemDistance = float.MaxValue;
            if (_canGrab) DeactivateGrab();
            StartCoroutine(FindOtherActions());
        }
    }

    void OnDestroy()
    {
        _inputActions.FindActionMap("Interaction").FindAction("MainInteraction").performed -= Grab;
        _inputActions.FindActionMap("Interaction").FindAction("MainInteraction").performed -= UseItem;
        _inputActions.Disable();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Grabbable g) && !Inventory.Instance.HasItem(g.ItemType))
        {
            float distance = Vector3.Distance(other.gameObject.transform.position, transform.position);
            if (_nearestItemOnReach == null || _itemDistance > distance)
            {
                _itemDistance = distance;
                _nearestItemOnReach = other.gameObject;
            }
            if (!_canUse && !_canPuzzle) ActivateGrab();
        }
        else if (other.gameObject.TryGetComponent(out UseItemMission useMission) && !useMission.IsCompleted)
        {
            float distance = Vector3.Distance(other.gameObject.transform.position, transform.position);
            if (_nearestMission == null || _missionDistance > distance)
            {
                _missionDistance = distance;
                _nearestMission = useMission;
            }
            ActivateUse();
            if (_canGrab) DeactivateGrab();
            if (_canPuzzle) DeactivatePuzzle();
        }
        else if (other.gameObject.TryGetComponent(out PuzzleMission puzzleMission) && !puzzleMission.IsCompleted)
        {
            float distance = Vector3.Distance(other.gameObject.transform.position, transform.position);
            if (_nearestMission == null || _missionDistance > distance)
            {
                _missionDistance = distance;
                _nearestMission = puzzleMission;
            }
            ActivatePuzzle();
            if (_canUse) DeactivateUse();
            if (_canGrab) DeactivateGrab();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Grabbable _))
        {
            _itemDistance = float.MaxValue;
            _nearestItemOnReach = null;
            DeactivateGrab();
            StartCoroutine(FindOtherActions());
        }
        else if (other.gameObject.TryGetComponent(out UseItemMission _))
        {
            _missionDistance = float.MaxValue;
            _nearestMission = null;
            DeactivateUse();
            StartCoroutine(FindOtherActions());
        }
        else if (other.gameObject.TryGetComponent(out PuzzleMission _))
        {
            _missionDistance = float.MaxValue;
            _nearestMission = null;
            DeactivateUse();
            StartCoroutine(FindOtherActions());
        }
    }

    IEnumerator FindOtherActions()
    {
        yield return new WaitForEndOfFrame();
        CapsuleCollider collider = GetComponent<CapsuleCollider>();
        Vector3 smallerCollider = collider.bounds.extents - Vector3.one * 0.2f;
        List<Collider> collidersInside = Physics.OverlapBox(collider.bounds.center, smallerCollider, collider.transform.rotation).ToList();
        if (collidersInside.Any(c => c.TryGetComponent(out UseItemMission m) && m.GetMissionState().Equals(MissionState.NOT_DONE)))
        {
            GameObject m = collidersInside.First(c => c.TryGetComponent(out UseItemMission m) && m.GetMissionState().Equals(MissionState.NOT_DONE)).gameObject;
            _missionDistance = Vector3.Distance(m.transform.position, transform.position);
            _nearestMission = m.GetComponent<UseItemMission>();
            ActivateUse();
        }
        else if (collidersInside.Any(c => c.TryGetComponent(out PuzzleMission m) && m.GetMissionState().Equals(MissionState.NOT_DONE)))
        {
            GameObject m = collidersInside.First(c => c.TryGetComponent(out PuzzleMission m) && m.GetMissionState().Equals(MissionState.NOT_DONE)).gameObject;
            _missionDistance = Vector3.Distance(m.transform.position, transform.position);
            _nearestMission = m.GetComponent<PuzzleMission>();
            ActivatePuzzle();
        }
        else if (collidersInside.Any(c => c.TryGetComponent(out Grabbable g) && !g.Grabbed))
        {
            GameObject i = collidersInside.First(c => c.TryGetComponent(out Grabbable g) && !g.Grabbed).gameObject;
            _missionDistance = Vector3.Distance(i.transform.position, transform.position);
            _nearestItemOnReach = i;
            ActivateGrab();
        }
    }

    void Grab(InputAction.CallbackContext ctx)
    {
        if (_nearestItemOnReach != null)
        {
            if (Inventory.Instance.AddItem(_nearestItemOnReach))
            {
                _nearestItemOnReach = null;
                _itemDistance = float.MaxValue;
                DeactivateGrab();
            }
            else
            {
                InteractionOptions.Instance.ActivateWithTime(_grabIssue, 2);
            }
        }
    }

    void UseItem(InputAction.CallbackContext ctx)
    {
        if (_nearestMission != null && _nearestMission is UseItemMission)
        {
            string itemsUsedStr;
            List<ItemType> itemsUsed = ((UseItemMission)_nearestMission).UseRequiredItems();
            if (itemsUsed != null && itemsUsed.Count > 0)
            {
                itemsUsedStr = string.Join(", ", itemsUsed);
                InteractionOptions.Instance.ActivateWithTime(_itemsUsedBase + itemsUsedStr, 4);
            }
            else
            {
                InteractionOptions.Instance.ActivateWithTime(_missionIssue, 4);
            }
            if (_nearestMission.IsCompleted)
            {
                DeactivateUse();
            }
        }
    }

    void UsePuzzle(InputAction.CallbackContext ctx)
    {
        if (_nearestMission != null && _nearestMission is PuzzleMission)
        {
            ((PuzzleMission)_nearestMission).StartMission();
            DeactivatePuzzle();
            _inputActions.FindActionMap("Interaction").FindAction("ExitPuzzleInteraction").performed += ExitPuzzle;
            InteractionOptions.Instance.ActivateWithTime(_exitPuzzleHint, 3);
        }
    }

    void ExitPuzzle(InputAction.CallbackContext ctx)
    {
        if (_nearestMission != null && _nearestMission is PuzzleMission)
        {
            ((PuzzleMission)_nearestMission).ExitMission();
            _inputActions.FindActionMap("Interaction").FindAction("ExitPuzzleInteraction").performed -= ExitPuzzle;
            InteractionOptions.Instance.Deactivate();
            StartCoroutine(FindOtherActions());
        }
    }

    void ActivateGrab()
    {
        _canGrab = true;
        InteractionOptions.Instance.Activate(_grabHint);
        _inputActions.FindActionMap("Interaction").FindAction("MainInteraction").performed += Grab;
    }

    void DeactivateGrab()
    {
        _canGrab = false;
        InteractionOptions.Instance.Deactivate();
        _inputActions.FindActionMap("Interaction").FindAction("MainInteraction").performed -= Grab;
    }

    void ActivateUse()
    {
        _canUse = true;
        InteractionOptions.Instance.Activate(_useHint);
        _inputActions.FindActionMap("Interaction").FindAction("MainInteraction").performed += UseItem;
    }

    void DeactivateUse()
    {
        _canUse = false;
        InteractionOptions.Instance.Deactivate();
        _inputActions.FindActionMap("Interaction").FindAction("MainInteraction").performed -= UseItem;
    }

    void ActivatePuzzle()
    {
        _canPuzzle = true;
        InteractionOptions.Instance.Activate(_puzzleHint);
        _inputActions.FindActionMap("Interaction").FindAction("MainInteraction").performed += UsePuzzle;
    }

    void DeactivatePuzzle()
    {
        _canPuzzle = false;
        InteractionOptions.Instance.Deactivate();
        _inputActions.FindActionMap("Interaction").FindAction("MainInteraction").performed -= UsePuzzle;
    }
}
