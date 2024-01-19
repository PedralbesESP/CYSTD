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
    string _grabIssue;
    string _missionIssue;
    string _itemsUsedBase;
    UseItemMission _nearestMission;
    float _missionDistance = float.MaxValue;
    GameObject _nearestItemOnReach;
    float _itemDistance = float.MaxValue;
    bool _canGrab;
    bool _canUse;

    void Awake()
    {
        _inputActions.Enable();
        string binding = _inputActions.FindActionMap("Interaction").FindAction("MainInteraction").GetBindingDisplayString().ToUpper();
        _grabHint = $"Press " + binding + " to pick up";
        _useHint = $"Press " + binding + " to use items";
        _grabIssue = "Can't grab item";
        _missionIssue = "You don't have the necessary items";
        _itemsUsedBase = "Items used: ";
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
            if (!_canUse)
            {
                ActivateGrab();
            }
        }
        else if (other.gameObject.TryGetComponent(out UseItemMission mission) && !mission.GetMissionState().Equals(MissionState.DONE))
        {
            float distance = Vector3.Distance(other.gameObject.transform.position, transform.position);
            if (_nearestMission == null || _missionDistance > distance)
            {
                _missionDistance = distance;
                _nearestMission = mission;
            }
            ActivateUse();
            if (_canGrab)
            {
                DeactivateGrab();
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Grabbable _))
        {
            _itemDistance = float.MaxValue;
            _nearestItemOnReach = null;
            DeactivateGrab();
        }
        else if (other.gameObject.TryGetComponent(out UseItemMission mission))
        {
            _missionDistance = float.MaxValue;
            _nearestMission = null;
            DeactivateUse();
            CapsuleCollider collider = GetComponent<CapsuleCollider>();
            List<Collider> collidersInside = Physics.OverlapBox(collider.bounds.center, collider.bounds.extents, collider.transform.rotation).ToList();
            if (collidersInside.Any(c => c.TryGetComponent(out Grabbable _)))
            {
                ActivateGrab();
            }
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
        if (_nearestMission != null)
        {
            string itemsUsedStr;
            List<ItemType> itemsUsed = _nearestMission.UseRequiredItems();
            if (itemsUsed != null && itemsUsed.Count > 0)
            {
                itemsUsedStr = string.Join(", ", itemsUsed);
                InteractionOptions.Instance.ActivateWithTime(_itemsUsedBase + itemsUsedStr, 4);
            }
            else
            {
                InteractionOptions.Instance.ActivateWithTime(_missionIssue, 4);
            }
            if (_nearestMission.GetMissionState().Equals(MissionState.DONE))
            {
                DeactivateUse();
            }
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
}
