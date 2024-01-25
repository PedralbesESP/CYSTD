using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdlePlayerState : PlayerState
{
    public override void Update()
    {
        _rigidbody.velocity = Vector3.Lerp(_rigidbody.velocity, Vector3.zero, _playerMovement.LerpFactor);
    }

    public override PlayerState CheckTransition()
    {
        if (_playerMovement.Direction != Vector3.zero)
        {
            if (_playerMovement.IsRunning)
            {
                return new RunPlayerState();
            }
            if (_playerMovement.IsCrouching)
            {
                return new CrouchPlayerState();
            }
            return new WalkPlayerState();
        }
        return null;
    }
}
