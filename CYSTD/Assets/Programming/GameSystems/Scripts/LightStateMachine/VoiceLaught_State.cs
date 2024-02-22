using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Light_State/SpookySounds")]
public class VoiceLaught_State : Light_State
{
    PlayerAudioEvents playerAudioEvents;

    protected override void Start()
    {
        playerAudioEvents = player.GetComponentInChildren<PlayerAudioEvents>();
        if (playerAudioEvents) playerAudioEvents.SpookySounds.Play();
    }
    public override Light_State CheckTransition(float sanity)
    {
        if (sanity < NextThreshhold)
        {
            
            return nextState;
        }
        if (sanity > PreviousThreshhold)
        {
            playerAudioEvents.heavyBreathingEmitter.Stop();
            return previousState;
        }
        return null;
    }

    public override void Update()
    {
        //throw new System.NotImplementedException();
    }
}
