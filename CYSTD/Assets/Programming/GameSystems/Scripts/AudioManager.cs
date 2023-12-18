using FMOD.Studio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager audioManager {  get; private set; }
    private List<EventInstance> eventInstances;
    private EventInstance musicEventInstance;

    private void Awake()
    {
        if (audioManager != null)
        {
            Debug.LogError("Hay más de 1 audioManager en la escena");
        }

        audioManager = this;
        eventInstances = new List<EventInstance>();

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private EventInstance CreateInstance(EventReference eventReference)
    {
        EventInstance eventInstance = RuntimeManager.CreateInstance(eventReference);
        eventInstances.Add(eventInstance);
        return eventInstance;
    }

    public void PlayOneShot(EventReference sound, Vector3 worldPosition)
    {
        RuntimeManager.PlayOneShot(sound, worldPosition);
    }
}
