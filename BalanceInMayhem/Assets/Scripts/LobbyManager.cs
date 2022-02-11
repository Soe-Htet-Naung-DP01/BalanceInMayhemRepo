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

    public override void OnRoomListUpdate(List<RoomInfo> roomList) //Get Room Updates from Photon Server and Update the in game list accordingly
    {
        UpdateRoomList(roomList);
    }

    public void UpdateRoomList(List<RoomInfo> list)
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

    public void JoinRoom(string roomName)
    {
        PhotonNetwork.JoinRoom(roomName);
    }
}
