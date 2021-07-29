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

    public void PlayVsLocal()
    {
        Globals.instance.is_VS_AI = false;
        Debug.Log("We are startinging Local Game!, is_VS_AI = false");
        SceneManager.LoadScene(1);
    }

    public void PlayVsAI()
    {
        Globals.instance.is_VS_AI = true;
        Debug.Log("We are startinging VS AI Game!, is_VS_AI = true");
        SceneManager.LoadScene(1);
    }

    public void Lobby()
    {
        SceneManager.LoadScene("Lobby");
    }

    public void Settings()
    {
        SceneManager.LoadScene("Settings");
    }

    public void QuitGame()
    {
        Debug.Log("Quit button was pressed");
        Application.Quit();
    }
}
