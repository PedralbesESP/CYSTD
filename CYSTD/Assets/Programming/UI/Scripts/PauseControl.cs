using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseControl : MonoBehaviour
{
    [SerializeField] private GameObject PauseMenu;
    [SerializeField] private InputActionAsset _inputUI;
    private bool isPaused;
    private InputAction _pauseP, _pauseEsc;

    private void Start()
    {
        _pauseP = _inputUI.FindActionMap("Pause").FindAction("Pause_P");
        _pauseEsc = _inputUI.FindActionMap("Pause").FindAction("Pause_Esc");

        _pauseP.performed += PauseGame;
        _pauseEsc.performed += PauseGame;
        _pauseEsc.Enable();
        _pauseP.Enable();
    }

    void Update()
    {

    }

    void PauseGame(InputAction.CallbackContext ctx)
    {
        Debug.Log("Llega");
        if (!isPaused)
        {
            PauseMenu.SetActive(true);
            isPaused = true;

            Time.timeScale = 0;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else if (isPaused)
        {
            Resume();
        }
    }

    public void Resume()
    {
        PauseMenu.SetActive(false);
        isPaused = false;
        Time.timeScale = 1;
        //Cursor.visible = false;
        //Cursor.lockState = CursorLockMode.Locked;
    }
    private void OnDestroy()
    {
        _pauseP.Disable();
        _pauseEsc.Disable();
    }
}
