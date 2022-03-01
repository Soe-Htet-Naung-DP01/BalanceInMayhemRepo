using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ConnectToServer : MonoBehaviourPunCallbacks
{
    public InputField usernameInput;
    public Text buttonText;
    public Text warningText;

    public void OnClickConnect()
    {
        if(usernameInput.text.Length >= 1) //Check If username is not blank
        {
            PhotonNetwork.NickName = usernameInput.text; //Get Nickname
            buttonText.text = "Connecting...";
            PhotonNetwork.AutomaticallySyncScene = true; //Auto Sync Scenes of the players in Same Lobby, so that no one gets left beind.
            PhotonNetwork.ConnectUsingSettings(); //Photon's built in function to connect to photon server.
        }
        else //if username is blank
        {
            warningText.text = "A username is required to Connect to the Server!";
        }
    }

    public override void OnConnectedToMaster() //Upon establishing a successful connection
    {
        PhotonNetwork.LoadLevel("Lobby"); //Change to Lobby Scene.
    }

}
