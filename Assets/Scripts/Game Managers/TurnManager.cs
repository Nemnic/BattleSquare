using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class TurnManager : MonoBehaviour
{
    #region Singleton
    public static TurnManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Warning! More then one instance of Globals found!");
            return;
        }

        instance = this;
    }

    #endregion

    [Header("Whos turns stuff")]
    [SerializeField] public bool isBluesTurn = true;
    [SerializeField] private GameObject directionArrow;

    [Header("Vs AI stuff")]
    [SerializeField] private GameObject blockPanel;

    private void Start()
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            BlockPlayer(true);
        }
    }


    public void SwapTurn()
    {
        isBluesTurn = !isBluesTurn;

        if (Globals.instance.is_VS_AI)
        {
            BlockPlayerDuringAIMove();
        }
        if (Globals.instance.is_Multiplayer)
        {
            BlockPlayersDuringMultiplayer();
        }
    }

    public void SetPointer()
    {
        if (isBluesTurn)
        {
            directionArrow.transform.rotation = Quaternion.Euler(0, 0, -90);
        }
        else
        {
            directionArrow.transform.rotation = Quaternion.Euler(0, 0, 90);
        }
    }

    private void Update()
    {
        SetPointer();

        if (Input.GetKeyDown(KeyCode.A))
        {
            blockPanel.SetActive(!blockPanel.activeSelf);
        }
    }

    public void BlockPlayerDuringAIMove()
    {
        if (isBluesTurn)
        {
            BlockPlayer(false);
        }
        else
        {
            BlockPlayer(true);
        }
    }

    public void BlockPlayersDuringMultiplayer()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            if (isBluesTurn)
            {
                BlockPlayer(false);
            }
            else
            {
                BlockPlayer(true);
            }
        }
        else
        {
            if (isBluesTurn)
            {
                BlockPlayer(true);
            }
            else
            {
                BlockPlayer(false);
            }
        }
    }

    public void BlockPlayer(bool active)
    {
        blockPanel.SetActive(active);
    }

    public Color GetCurrentColor()
    {
        if (isBluesTurn)
        {
            return Globals.instance.colorBlue;
        }
        else
        {
            return Globals.instance.colorOrange;
        }
    }
}
