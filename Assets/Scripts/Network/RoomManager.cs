using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;      // if we want to use a separate waiting room scene

public class RoomManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private int multiplayerSceneIndex; // if not using by name  - direct to game
    [SerializeField] private int multiplayerWaitingSceneIndex; // if not using by name - to waiting room first

    public override void OnEnable()
    {
        PhotonNetwork.AddCallbackTarget(this);
    }

    public override void OnDisable()
    {
        PhotonNetwork.RemoveCallbackTarget(this);
    }

    private void StartGame()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            Debug.Log("RoomManager: Starting Game");
            PhotonNetwork.LoadLevel(multiplayerSceneIndex);
        }
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("RoomManager: Joined Room");
        //StartGame();
        WaitForAllPlayerToStartGame();
    }

    //Delayed start
    public void WaitForAllPlayerToStartGame()
    {
        // a version with separate waiting room
        SceneManager.LoadScene(multiplayerWaitingSceneIndex);

        // Do waiting stuff ...


    }
}
