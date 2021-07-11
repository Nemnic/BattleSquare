using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{

    public void ToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void Play()
    {
        SceneManager.LoadScene(1);
    }

    public void Lobby()
    {
        SceneManager.LoadScene("Lobby");
    }

    public void QuitGame()
    {
        Debug.Log("Quit button was pressed");
        Application.Quit();
    }
}
