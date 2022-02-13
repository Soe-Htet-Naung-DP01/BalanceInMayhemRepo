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

    public RoomItem roomItemPrefab;
    List<RoomItem> roomItemList = new List<RoomItem>();
    public Transform contentObject;

    public float timeBetweenUpdates = 1.5f;
    float nextUpdateTime;

    public List<PlayerItem> playerItemList = new List<PlayerItem>();
    public PlayerItem playerItemPrefab;
    public Transform playerItemParent;

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
        UpdatePlayerList();
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList) //Get Room Updates from Photon Server and Update the in game list accordingly
    {
        if(Time.time >= nextUpdateTime)
        {
            UpdateRoomList(roomList);
            nextUpdateTime = Time.time + timeBetweenUpdates; //Set a cooldown time of 1.5 sec between each update
        }

    }

    public void UpdateRoomList(List<RoomInfo> list) //Update the lobby room list
    {

        //Clear the current existing list
        foreach (RoomItem item in roomItemList)
        {
            Destroy(item.gameObject);
        }
        roomItemList.Clear();

        //Create a new list with existing rooms and newly added rooms
        foreach(RoomInfo room in list)
        {
            RoomItem newRoom = Instantiate(roomItemPrefab, contentObject);
            newRoom.SetRoomName(room.Name);
            roomItemList.Add(newRoom);
        }
    }

    public void JoinRoom(string roomName) //Join the room that has the same name on button text
    {
        PhotonNetwork.JoinRoom(roomName);
    }

    public void OnClickLeaveRoom() //Leave the current room
    {
        PhotonNetwork.LeaveRoom();

    }

    public override void OnLeftRoom()//When leaving the room, switch panels
    {
        roompanel.SetActive(false);
        lobbypanel.SetActive(true);
    }

    public override void OnConnectedToMaster() //This is called here to rejoin the lobby successfully after leaving the room.
    {
        PhotonNetwork.JoinLobby();
    }

    public void UpdatePlayerList()
    {

        //Delete All the existing items in player item list
        foreach (PlayerItem item in playerItemList)
        {
            Destroy(item.gameObject);
        }
        playerItemList.Clear();

        //check if the room still exists
        if(PhotonNetwork.CurrentRoom == null)
        {
            return;
        }
        //Recreate the existing ones and the new ones
        foreach (KeyValuePair<int, Player> player in PhotonNetwork.CurrentRoom.Players)
        {
            PlayerItem newPlayerItem = Instantiate(playerItemPrefab, playerItemParent);
            newPlayerItem.SetPlayerInfo(player.Value);
            playerItemList.Add(newPlayerItem);
        }

    }

    public override void OnPlayerEnteredRoom(Player newPlayer) //when new player enters the current room
    {
        UpdatePlayerList();
    }

    public override void OnPlayerLeftRoom(Player otherPlayer) //when the existing player(s) left the current room
    {
        UpdatePlayerList();
    }
}
