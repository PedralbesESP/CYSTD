using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMOD.Studio;

public class WalkPlayerState : PlayerState
{
    //private EventInstance _playerFootSteps;

    public override void Start(GameObject go, Rigidbody rb, PlayerMovement pm)
    {
        base.Start(go, rb, pm);
        //_playerFootSteps = AudioManager.audioManager.CreateEventInstance(FMODEvents.instance.PlayerSteps);
        //_playerFootSteps.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(go));

    }
    public override void Update()
    {
        if (_playerMovement.Direction != Vector3.zero)
        {
            Vector3 desiredVelocity = _playerMovement.Direction * _playerMovement.Speed * Time.deltaTime * 100;
            _rigidbody.velocity = Vector3.Lerp(_rigidbody.velocity, desiredVelocity, 0.01f);
            _rigidbody.velocity.SetY(-100); 
        }
        //UpdateSound();

    }

    public override PlayerState CheckTransition()
    {
        if (_playerMovement.Direction == Vector3.zero)
        {
            //UpdateSound();
            return new IdlePlayerState();
        }
        return null;
    }
    

}
