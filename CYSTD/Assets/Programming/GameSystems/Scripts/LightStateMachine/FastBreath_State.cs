using FMOD.Studio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Light_State/FastBreath")]
public class FastBreath_State : Light_State
{
    PlayerAudioEvents playerAudioEvents;
    protected override void Start()
    {
        playerAudioEvents = player.GetComponentInChildren<PlayerAudioEvents>();
        if (playerAudioEvents) playerAudioEvents.heavyBreathingEmitter.Play();
        base.Start();
    }
    public override Light_State CheckTransition(float sanity)
    {
        if (sanity < NextThreshhold)
        {
            if (playerAudioEvents) playerAudioEvents.heavyBreathingEmitter.Stop();
            Debug.Log("Exit");
            return nextState;
        }
        if (sanity > PreviousThreshhold)
        {
            playerAudioEvents.heavyBreathingEmitter.Stop();
            Debug.Log("Exit");
            return previousState;
        }
        return null;
    }

    public override void Update()
    {
        //checkSound();
        //Ejecutar sonido de respiración rapida.
    }

}
