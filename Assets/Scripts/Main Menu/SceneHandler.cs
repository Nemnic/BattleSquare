using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class SceneHandler : MonoBehaviour
{

    public void ToMainMenu()
    {
        ResetModeSet();

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

        Globals.instance.is_Multiplayer = true;
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

    private void ResetModeSet()
    {
        Globals.instance.is_Multiplayer = false;
        Globals.instance.is_VS_AI = false;
    }

    // Temp untill system is made completely
    
    public void SingelPlayerLobby()
    {
        SFX.instance.PlaySelect();

        SceneManager.LoadScene("Lobby SingelPlayer");
    }


    // Multiplayer related /Photon

    public void DisconnectPhoton()
    {
        Debug.Log("SceneHandler: We are Disconnecting Photon");
        PhotonNetwork.Disconnect();
    }

    public void DisconnectFromRoom()
    {
        Debug.Log("SceneHandler: We are trying to leaving the room");
        PhotonNetwork.LeaveRoom();
        
    }

    public void DisconnectFromLobby()
    {
        Debug.Log("SceneHandler: We are trying to leaving the Lobby");
        PhotonNetwork.LeaveLobby();
    }
}
