using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles mission where plaer interacts with the object. Shows interaction hint when the player gets close to the object. 
/// If you override OnTriggerEnter or OnTriggerExit, call the base function first.
/// </summary>

[RequireComponent(typeof(BoxCollider))]
public class InteractionMissionObject : MissionObject
{
    protected bool _hasPlayerNear;

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (!_state.Equals(MissionState.DONE) && other.gameObject.CompareTag("Player"))
        {
            _hasPlayerNear = true;
            InteractionOptions.Instance.Activate(GetMissionInstruction());
        }
    }

    protected virtual void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _hasPlayerNear = false;
            InteractionOptions.Instance.Deactivate();
        }
    }
}
