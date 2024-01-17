using FMOD.Studio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSound3D : MonoBehaviour
{

    private EventInstance Horror;
    // Este Script es solo para probar a reproducir algun sonido en un sitio concreto. 
    void Start()
    {
        Horror = AudioManager.audioManager.CreateEventInstance(FMODEvents.instance.AtmosphereHorror);
        Horror.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(this.gameObject));
        Horror.start();
    }

    private void OnDisable()
    {
        Horror.stop(STOP_MODE.IMMEDIATE);
    }
}
