using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerState
{
    protected GameObject _gameObject;
    protected Rigidbody _rigidbody;
    protected PlayerMovement _playerMovement;

    public virtual void Start(GameObject go, Rigidbody rb, PlayerMovement pm) 
    { 
        _gameObject = go;
        _rigidbody = rb;
        _playerMovement = pm;
    }

    public abstract void Update();

    public abstract PlayerState CheckTransition();
}
