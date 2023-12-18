using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdlePlayerState : PlayerState
{
    public override void Update()
    {
        _rigidbody.velocity = Vector3.Lerp(_rigidbody.velocity, Vector3.zero, 0.01f);
    }

    public override PlayerState CheckTransition()
    {
        if (_playerMovement.Direction != Vector3.zero)
        {
            return new WalkPlayerState();
        }
        return null;
    }
}
