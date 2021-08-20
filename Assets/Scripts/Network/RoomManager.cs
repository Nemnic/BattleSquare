using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class RoomManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private int multiplayerSceneIndex; // if not using by name

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
        StartGame();
    }
}
