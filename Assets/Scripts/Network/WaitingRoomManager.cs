using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;
using Photon.Realtime;

public class WaitingRoomManager : MonoBehaviourPunCallbacks
{
    // PhotonView must be attached to the object for sending rpc that updates the timer
    private PhotonView myPhotonView;

    [Header("Scene navigation indexes")]
    [SerializeField] private int multiplayerSceneIndex; // if not using by name
    [SerializeField] private int lobbySceneIndex; // when cancel

    [Header("Room Info")]
    [SerializeField] private int minPlayersToStart;
    private int playerCount;
    private int roomSize;

    [Header("Text Variables")]
    [SerializeField] TextMeshProUGUI text_PlayerName;
    [SerializeField] TextMeshProUGUI text_playerCount;
    [SerializeField] TextMeshProUGUI text_TimerToStart;

    // bool values for if the timer can count down
    private bool readyToCountDown;
    private bool readyToStart;
    private bool startingGame;
    // countdown timer variables
    private float timerToStartGame;
    private float notFullGameTimer;
    private float fullGameTimer;
    [Header("Reset values for timer")]
    [SerializeField] private float maxWaitTime;
    [SerializeField] private float maxFullGameTime;

    private void Start()        // just as good as onJoinedRoom     - incase we don´t want the waiting room
    {
        myPhotonView = GetComponent<PhotonView>();
        fullGameTimer = maxFullGameTime;
        notFullGameTimer = maxWaitTime;
        timerToStartGame = maxWaitTime;

        text_PlayerName.text = PhotonNetwork.NickName;

        PlayerCountUpdate();
    }

    void PlayerCountUpdate()
    {
        playerCount = PhotonNetwork.PlayerList.Length;
        roomSize = PhotonNetwork.CurrentRoom.MaxPlayers;
        text_playerCount.text = playerCount + "/" + roomSize;

        if (playerCount == roomSize)
        {
            readyToStart = true;
        }
        else if (playerCount >= minPlayersToStart)
        {
            readyToCountDown = true;
        }
        else
        {
            readyToCountDown = false;
            readyToStart = false;
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        PlayerCountUpdate();

        if (PhotonNetwork.IsMasterClient)
        {
            myPhotonView.RPC("RPC_SendTimer", RpcTarget.Others, timerToStartGame);
        }
    }

    [PunRPC]
    private void RPC_SendTimer(float timeIn)
    {
        //RPC for syncing the countdown timer to those that join after it has started the countdown
        timerToStartGame = timeIn;
        notFullGameTimer = timeIn;
        if (timeIn < fullGameTimer)
        {
            fullGameTimer = timeIn;
        }
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        PlayerCountUpdate();
    }

    private void ResetTimer()
    {
        timerToStartGame = maxWaitTime;
        notFullGameTimer = maxWaitTime;
        fullGameTimer = maxFullGameTime;
    }

    void WaitingForMorePlayers()
    {
        if (playerCount <= 1)
        {
            ResetTimer();
        }

        if (readyToStart)
        {
            fullGameTimer -= Time.deltaTime;
            timerToStartGame = fullGameTimer;
        }
        else if (readyToCountDown)
        {
            notFullGameTimer -= Time.deltaTime;
            timerToStartGame = notFullGameTimer;
        }

        string tempTimer = string.Format("{0:00}", timerToStartGame);
        text_TimerToStart.text = tempTimer;

        if (timerToStartGame <= 0f)
        {
            if (startingGame)
            {
                return;
            }
            StartGame();
        }
    }

    private void StartGame()
    {
        startingGame = true;
        if (!PhotonNetwork.IsMasterClient)
        {
            return;
        }
        PhotonNetwork.CurrentRoom.IsOpen = false;
        PhotonNetwork.LoadLevel(multiplayerSceneIndex);
    }

    private void Update()
    {
        WaitingForMorePlayers();
    }

    public void Cancel()
    {
        PhotonNetwork.LeaveRoom();
        SceneManager.LoadScene(lobbySceneIndex);
    }
}
