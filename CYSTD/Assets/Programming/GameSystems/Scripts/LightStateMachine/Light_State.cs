using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Light_State : ScriptableObject
{
    [SerializeField] protected Light_State nextState;
    [SerializeField] protected Light_State previousState;
    [SerializeField] protected List<Light_State> continuosPlayedStates;

    [SerializeField] protected float NextThreshhold;
    [SerializeField] protected float PreviousThreshhold;

    protected GameObject player;

    public void Init(GameObject go)
    {
        player = go;
        Start();
    }
    protected virtual void Start()
    {
        continuosPlayedStates = new List<Light_State>();
    }
    public abstract void Update();
    public abstract Light_State CheckTransition(float sanity);
}
