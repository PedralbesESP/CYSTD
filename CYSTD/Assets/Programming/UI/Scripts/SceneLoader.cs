using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader Instance;

    void Start()
    {
        Instance = this;
    }

    public void LoadScene(string SceneName)
    {
        Debug.Log("Loading scene");
        SceneManager.LoadScene(SceneName);
    }
}
