using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;


public class NetworkManager : MonoBehaviourPunCallbacks
{
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

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

    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        
        Application.Quit();
    }
    
}
