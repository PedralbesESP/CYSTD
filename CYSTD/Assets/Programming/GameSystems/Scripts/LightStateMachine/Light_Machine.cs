using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Light_Machine : MonoBehaviour
{
    Light_State CurrentState { get; set; }

    [SerializeField] private Light_State EntryState;

    // Start is called before the first frame update
    void Start()
    {
        CurrentState = Instantiate(EntryState);
        CurrentState.Init(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        CurrentState.Update();
    }
    private void LateUpdate()
    {
        Light_State newState = CurrentState.CheckTransition(GetComponent<PlayerSanity>().Sanity);
        if (newState != null)
        {
            CurrentState = newState;
            CurrentState.Init(this.gameObject);
        }
    }
}
