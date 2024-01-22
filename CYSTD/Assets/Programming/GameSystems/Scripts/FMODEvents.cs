using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FMODEvents : MonoBehaviour
{
    [field: Header("Player SFX")]
    [field: SerializeField] private EventReference _playerSteps;
    public EventReference PlayerSteps { get => _playerSteps; private set => _playerSteps = value; }

    [field: Header("Atmosphere Sounds")]
    [field: SerializeField] private EventReference _atmosphereHorror;

    public static FMODEvents instance { get; private set; }
    public EventReference AtmosphereHorror { get => _atmosphereHorror; set => _atmosphereHorror = value; }

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Encontrado más de un FMOD Events instance en la escena");
        }
        instance = this;
    }
}
