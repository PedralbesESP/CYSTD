using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField]private GameObject _player;
    [SerializeField] private GameObject GameOver;
    [SerializeField] private InputActionAsset _playerInput;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        NetworkManager.Instance.IsInGame();
    }

    public GameObject getPlayer()
    {
        return _player;
    }
    public void setPlayer(GameObject player)
    {
        _player = player;
    }

    public void Win()
    {
        SceneLoader.Instance.LoadScene("UI");
        DisableInputs();
    }

    public void Loose()
    {
        GameOver.SetActive(true);
        DisableInputs();
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void DisableInputs()
    {
        _playerInput.FindActionMap("Movement").Disable();
        _playerInput.FindActionMap("Head").Disable();
        _playerInput.FindActionMap("Interaction").Disable();

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}
