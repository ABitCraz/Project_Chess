using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class NetworkManager : MonoBehaviourPunCallbacks
{

    public TextMeshProUGUI roomNum;
    
    private void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("登录成功");
        PhotonNetwork.JoinLobby();
    }
    
    public void JoinOrCreateButton()
    {
        if(roomNum.text.Equals(""))
            return;
        // loginUI.SetActive(false);
        RoomOptions roomOptions = new RoomOptions{MaxPlayers = 2 };
        PhotonNetwork.JoinOrCreateRoom(roomNum.text, roomOptions, default);
        Debug.Log("加入房间");
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log("加入房间失败----"+message);
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel(1);
    }
}
