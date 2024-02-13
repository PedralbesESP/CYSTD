using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastBreath_State : Light_State
{
    [SerializeField] private float NextThreshhold;
    [SerializeField] private float PreviousThreshhold;

    public override Light_State CheckTransition(float sanity)
    {
        if (sanity < NextThreshhold)
        {
            return nextState;
        }
        if(sanity > PreviousThreshhold)
        {
            return previousState;
        }
        return null;
    }

    public override void Update()
    {
        //Ejecutar sonido de respiración rapida.
    }
}
