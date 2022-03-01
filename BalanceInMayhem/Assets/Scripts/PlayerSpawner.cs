using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerSpawner : MonoBehaviour
{
    public GameObject[] playerPrefabs;
    public Transform[] spwanPoints;

    // Start is called before the first frame update
    void Start()
    {
        int rndNum = Random.Range(0, spwanPoints.Length);
        Transform spwanPoint = spwanPoints[rndNum];
        GameObject playerToSpwan = playerPrefabs[(int)PhotonNetwork.LocalPlayer.CustomProperties["playerAvatar"]];
        PhotonNetwork.Instantiate(playerToSpwan.name, spwanPoint.position, Quaternion.identity);
    }

}
