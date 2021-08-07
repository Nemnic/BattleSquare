using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{

    public void ToMainMenu()
    {
        SFX.instance.PlayBack();
        SceneManager.LoadScene(0);
    }

    public void PlayVsLocal()
    {
        SFX.instance.PlaySelect();

        Globals.instance.is_VS_AI = false;
        Debug.Log("We are startinging Local Game!, is_VS_AI = false");
        SceneManager.LoadScene(1);
    }

    public void PlayVsAI()
    {
        SFX.instance.PlaySelect();

        Globals.instance.is_VS_AI = true;
        Debug.Log("We are startinging VS AI Game!, is_VS_AI = true");
        SceneManager.LoadScene(1);
    }

    public void Lobby()
    {
        SFX.instance.PlaySelect();

        SceneManager.LoadScene("Lobby");
    }

    public void LineMode()
    {
        SFX.instance.PlaySelect();

        SceneManager.LoadScene("Line Mode");
    }

    public void Settings()
    {
        SFX.instance.PlaySelect();

        SceneManager.LoadScene("Settings");
    }

    public void QuitGame()
    {
        SFX.instance.PlayLose();

        Debug.Log("Quit button was pressed");
        Application.Quit();
    }
}
