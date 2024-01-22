using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

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
