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
        //Get a random number and spawn players at that point of the transform array
        int rndNum = Random.Range(0, spwanPoints.Length);
        Transform spwanPoint = spwanPoints[rndNum];
        GameObject playerToSpwan;
        //if the player did not choose anything (the very first one is just the image of character and is not being counted as 0 of the array)
        if(PhotonNetwork.LocalPlayer.CustomProperties["playerAvatar"] == null)
        {
            playerToSpwan = playerPrefabs[0];
        }
        else //if player did choose, give them that avatar
        {
             playerToSpwan = playerPrefabs[(int)PhotonNetwork.LocalPlayer.CustomProperties["playerAvatar"]];
        }
        PhotonNetwork.Instantiate(playerToSpwan.name, spwanPoint.position, Quaternion.identity);
    }

}
