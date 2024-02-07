using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseControl : MonoBehaviour
{
    [SerializeField] private GameObject PauseMenu;
    [SerializeField] private InputActionAsset _inputUI;
    [SerializeField] private InputActionAsset _playerInput;
    [SerializeField] Canvas gameCanvas;
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
            gameCanvas.enabled = false;
            PauseMenu.SetActive(true);
            isPaused = true;
            _playerInput.FindActionMap("Movement").Disable();
            _playerInput.FindActionMap("Head").Disable();
            _playerInput.FindActionMap("Interaction").Disable();

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
        gameCanvas.enabled = true;
        PauseMenu.SetActive(false);
        isPaused = false;
        _playerInput.FindActionMap("Movement").Enable();
        _playerInput.FindActionMap("Head").Enable();
        _playerInput.FindActionMap("Interaction").Enable();
        //Cursor.visible = false;
        //Cursor.lockState = CursorLockMode.Locked;
    }
    private void OnDestroy()
    {
        _pauseP.Disable();
        _pauseEsc.Disable();
    }
}
