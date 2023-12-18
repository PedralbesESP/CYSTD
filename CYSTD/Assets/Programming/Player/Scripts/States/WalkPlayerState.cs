using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMOD;
using FMOD.Studio;

public class WalkPlayerState : PlayerState
{
    private EventInstance _playerFootSteps;

    public override void Start(GameObject go, Rigidbody rb, PlayerMovement pm)
    {
        base.Start(go, rb, pm);
        _playerFootSteps = AudioManager.audioManager.CreateEventInstance(FMODEvents.instance.PlayerSteps);
        AudioManager.audioManager.PlayOneShot(FMODEvents.instance.PlayerSteps, _playerMovement.transform.position);

    }
    public override void Update()
    {
        if (_playerMovement.Direction != Vector3.zero)
        {
            Vector3 desiredVelocity = _playerMovement.Direction * _playerMovement.Speed;
            _rigidbody.velocity = Vector3.Lerp(_rigidbody.velocity, desiredVelocity, 0.01f);
            UpdateSound();
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
    private void UpdateSound()
    {
        PLAYBACK_STATE playbackState;
        _playerFootSteps.getPlaybackState(out playbackState);
        if(playbackState.Equals(PLAYBACK_STATE.STOPPED))
        {
            _playerFootSteps.start();
            return;
        }
        _playerFootSteps.stop(STOP_MODE.ALLOWFADEOUT);
    }

}
