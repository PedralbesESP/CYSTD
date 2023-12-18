using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkPlayerState : PlayerState
{
    
    public override void Start(GameObject go, Rigidbody rb, PlayerMovement pm)
    {
        base.Start(go, rb, pm);
        AudioManager.audioManager.PlayOneShot(FMODEvents.instance.PlayerSteps, _playerMovement.transform.position);

    }
    public override void Update()
    {
        if (_playerMovement.Direction != Vector3.zero)
        {
            Vector3 desiredVelocity = _playerMovement.Direction * _playerMovement.Speed;
            _rigidbody.velocity = Vector3.Lerp(_rigidbody.velocity, desiredVelocity, 0.01f); 
        }
    }

    public override PlayerState CheckTransition()
    {
        if (_playerMovement.Direction == Vector3.zero)
        {
            return new IdlePlayerState();
        }
        return null;
    }
}
