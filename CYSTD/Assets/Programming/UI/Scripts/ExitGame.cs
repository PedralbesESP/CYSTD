using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitGame : MonoBehaviour
{
    public void QuitGame()
    {
        GameManager.Instance.Exit();
        Application.Quit();
        UnityEditor.EditorApplication.isPlaying = false;
        Debug.Log("Game is exiting");
        //Just to make sure its working
    }
}
