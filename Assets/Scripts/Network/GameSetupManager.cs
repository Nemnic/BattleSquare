using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Photon.Pun;

public class GameSetupManager : MonoBehaviour
{
    void Start()
    {
        CreatePlayer();       
    }

    private void CreatePlayer()
    {
        Debug.Log("GameSetupManager: Creating Player");
        PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PhotonPlayer"), Vector3.zero, Quaternion.identity);
    }
}
