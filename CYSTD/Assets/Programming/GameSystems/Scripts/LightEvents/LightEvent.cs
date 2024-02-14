using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LightEvent : MonoBehaviour
{
    bool isActive;
    public virtual void PlayEvent()
    {
        isActive = true;
    }

    public virtual void StopEvent()
    {
        isActive = false;
    }

    public bool IsActive()
    {
        return isActive;
    }
}
