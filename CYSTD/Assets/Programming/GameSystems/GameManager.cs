using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private GameObject _player;

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
    }

    public void Loose()
    {
        SceneLoader.Instance.LoadScene("UI");
    }

    public void Exit()
    {
        Application.Quit();
    }
}
