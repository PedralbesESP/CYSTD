using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FMODEvents : MonoBehaviour
{
    [field: Header("Player SFX")]
    [field: SerializeField] private EventReference playerSteps;
    public EventReference PlayerSteps { get => playerSteps; private set => playerSteps = value; }

    public static FMODEvents instance { get; private set; }

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Encontrado más de un FMOD Events instance en la escena");
        }
        instance = this;
    }
}
