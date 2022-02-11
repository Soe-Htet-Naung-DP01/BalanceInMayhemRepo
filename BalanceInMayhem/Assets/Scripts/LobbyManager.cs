using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class LobbyManager : MonoBehaviourPunCallbacks
{

    public InputField lobbyNameInput;
    public GameObject lobbypanel;
    public GameObject roompanel;
    public Text roomName;
    

    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.JoinLobby();
    }

    public void OnClickCreateRoom()
    {
        if(lobbyNameInput.text.Length >= 1) //if room name for lobby isn't blank
        {
            PhotonNetwork.CreateRoom(lobbyNameInput.text, new RoomOptions() {  MaxPlayers =  2} ); //create room with max 2 players to join
        }
    }

    public override void OnJoinedRoom() //upon joining the room, switch panels and display room name
    {
        lobbypanel.SetActive(false);
        roompanel.SetActive(true);
        roomName.text = "Room Name: " + PhotonNetwork.CurrentRoom.Name;
    }
}
