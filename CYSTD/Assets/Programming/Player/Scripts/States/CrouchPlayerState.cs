using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrouchPlayerState : PlayerState
{
    public override void Update()
    {
        if (_playerMovement.Direction != Vector3.zero)
        {
            Vector3 desiredVelocity = _playerMovement.Direction * (_playerMovement.Speed * _playerMovement.CrouchDecrementFactor) * Time.deltaTime * 100;
            _rigidbody.velocity = Vector3.Lerp(_rigidbody.velocity, desiredVelocity, _playerMovement.LerpFactor);
            _rigidbody.velocity.SetY(-100);
        }
    }

    public override PlayerState CheckTransition()
    {
        if (!_playerMovement.IsCrouching)
        {
            return new WalkPlayerState();
        }
        if (_playerMovement.Direction == Vector3.zero)
        {
            return new IdlePlayerState();
        }
        return null;
    }
}