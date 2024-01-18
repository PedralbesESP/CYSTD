using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Collider))]
public class InteractionController : MonoBehaviour
{
    [SerializeField]
    InputActionAsset _inputActions;
    string _grabHint;
    string _useHint;
    UseItemMission _nearestMission;
    float _missionDistance = float.MaxValue;
    GameObject _nearestItemOnReach;
    float _itemDistance = float.MaxValue;
    bool _canGrab;
    bool _canUse;

    void Awake()
    {
        _inputActions.Enable();
        string binding = _inputActions.FindActionMap("Interaction").FindAction("MainInteraction").GetBindingDisplayString();
        _grabHint = $"Press " + binding + " to pick up";
        _useHint = $"Press " + binding + " to use items";
    }

    void OnDestroy()
    {
        _inputActions.FindActionMap("Interaction").FindAction("MainInteraction").performed -= Grab;
        _inputActions.FindActionMap("Interaction").FindAction("MainInteraction").performed -= UseItem;
        _inputActions.Disable();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Grabbable _))
        {
            float distance = Vector3.Distance(other.gameObject.transform.position, transform.position);
            if (_nearestItemOnReach == null || _itemDistance > distance)
            {
                _itemDistance = distance;
                _nearestItemOnReach = other.gameObject;
            }
            if (!_canUse)
            {
                InteractionOptions.Instance.Activate(_grabHint);
                _inputActions.FindActionMap("Interaction").FindAction("MainInteraction").performed += Grab;
                _canGrab = true;
            }
        }
        else if (other.gameObject.TryGetComponent(out UseItemMission mission))
        {
            float distance = Vector3.Distance(other.gameObject.transform.position, transform.position);
            if (_nearestMission == null || _missionDistance > distance)
            {
                _missionDistance = distance;
                _nearestMission = mission;
            }
            InteractionOptions.Instance.Activate(_useHint);
            _canUse = true;
            if (_canGrab)
            {
                _inputActions.FindActionMap("Interaction").FindAction("MainInteraction").performed -= Grab;
            }
            _inputActions.FindActionMap("Interaction").FindAction("MainInteraction").performed += UseItem;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Grabbable _))
        {
            _itemDistance = float.MaxValue;
            _nearestItemOnReach = null;
            if (!_canUse)
            {
                InteractionOptions.Instance.Deactivate();
                _inputActions.FindActionMap("Interaction").FindAction("MainInteraction").performed -= Grab;
                _canGrab = false;
            }
        }
        else if (other.gameObject.TryGetComponent(out UseItemMission mission))
        {
            _missionDistance = float.MaxValue;
            _nearestMission = null;
            InteractionOptions.Instance.Deactivate();
            _canUse = false;
            _inputActions.FindActionMap("Interaction").FindAction("MainInteraction").performed -= UseItem;
            if (_canGrab)
            {
                _inputActions.FindActionMap("Interaction").FindAction("MainInteraction").performed += Grab;
                InteractionOptions.Instance.Activate(_grabHint);
            }
        }
    }

    void Grab(InputAction.CallbackContext ctx)
    {
        if (_nearestItemOnReach != null)
        {
            Inventory.Instance.AddItem(_nearestItemOnReach);
            InteractionOptions.Instance.Deactivate();
        }
    }

    void UseItem(InputAction.CallbackContext ctx)
    {
        if (_nearestMission != null)
        {
            _nearestMission.UseRequiredItems();
        }
    }
}
