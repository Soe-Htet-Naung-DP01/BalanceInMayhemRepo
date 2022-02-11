using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class RoomItem : MonoBehaviour
{
    public Text roomName;
    public LobbyManager manager;

    public void Start()
    {
        manager = FindObjectOfType<LobbyManager>();
    }
    public void SetRoomName(string _roomName)
    {
        roomName.text = _roomName;
    }

    public void OnClickJoinRoom()
    {
        manager.JoinRoom(roomName.text);
    }
}
