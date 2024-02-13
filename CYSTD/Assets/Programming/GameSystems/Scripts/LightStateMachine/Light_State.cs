using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Light_State : ScriptableObject
{
    [SerializeField] protected Light_State nextState;
    [SerializeField] protected Light_State previousState;

    public abstract void Update();
    public abstract Light_State CheckTransition(float sanity);
}
