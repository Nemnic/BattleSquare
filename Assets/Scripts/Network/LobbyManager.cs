using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject buttonStart;
    [SerializeField] private GameObject buttonCancel;

    private int roomSize = 2;

    public override void OnConnectedToMaster()
    {
        buttonStart.SetActive(true);
    }

    public void StartGame()
    {
        buttonStart.SetActive(false);
        buttonCancel.SetActive(true);
        PhotonNetwork.JoinRandomRoom();
        Debug.Log("Lobby Manager: Starting game");
    }

    void CreateRoom()
    {
        int randomRoomNumber = Random.Range(0, 10000);
        Debug.Log("Lobby Manager: Creating room now, Rooms name: Room" + randomRoomNumber);
        RoomOptions roomOptions = new RoomOptions() { IsVisible = true, IsOpen = true, MaxPlayers = (byte)roomSize };
        PhotonNetwork.CreateRoom("Room" + randomRoomNumber, roomOptions);
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("Lobby Manager: Failed to join room");
        CreateRoom();
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("Lobby Manager: Failed to create room... trying again");
        CreateRoom();
    }

    public void CancelGame()
    {
        buttonStart.SetActive(true);
        buttonCancel.SetActive(false);
        PhotonNetwork.LeaveRoom();
    }
}
