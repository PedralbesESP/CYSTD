using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameControl : MonoBehaviour
{
    [SerializeField] private bool isGameRuninng;
    [SerializeField] private InputActionAsset _inputUI;
    private InputAction _pauseP, _pauseEsc;


    // Start is called before the first frame update
    void Start()
    {
        _pauseP = _inputUI.FindActionMap("Pause").FindAction("Pause_P");
        _pauseEsc = _inputUI.FindActionMap("Pause").FindAction("Pause_Esc");

        _pauseP.performed += ChangeGameRunningState;
        _pauseEsc.performed += ChangeGameRunningState;
        _pauseEsc.Enable();
        _pauseP.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeGameRunningState(InputAction.CallbackContext ctx)
    {
        isGameRuninng = !isGameRuninng;

        if (isGameRuninng)
        {
            //Game is running
            Debug.Log("Game Running");
        }
        else
        {
            //Game is paused
            Debug.Log("Game Paused");
        }
    }

    public bool IsGameRunning()
    {
        return isGameRuninng;
    }
}
