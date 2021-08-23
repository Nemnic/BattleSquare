using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayersInfo : MonoBehaviour
{
    private PhotonView myPv;

    private void Start()
    {
        myPv = GetComponent<PhotonView>();
        if (myPv.IsMine)
        {
            myPv.RPC("RPC_SendName", RpcTarget.OthersBuffered, PhotonNetwork.NickName);
        }
    }

    [PunRPC]
    void RPC_SendName(string nameSent)
    {
        GameManager.instance.SetName(nameSent);
    }
}
