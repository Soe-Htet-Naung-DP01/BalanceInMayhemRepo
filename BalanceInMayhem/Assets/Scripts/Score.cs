using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class Score : MonoBehaviour
{
    PhotonView view;
    public Text scoreDisplay;
    int score;
    // Start is called before the first frame update
    void Start()
    {
        view = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateScore()
    {
        view.RPC("UpdateScoreRPC", RpcTarget.All);
    }

    [PunRPC]
    void UpdateScoreRPC()
    {
        score++;
        scoreDisplay.text = score.ToString();
    }
}
