using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityTemplateProjects.Tools;

public class EndingUI : SingletonPunCallbacks<EndingUI>
{
    public GameObject VictoryUI;
    public GameObject LoseUI;

    protected override void Awake()
    {
        base.Awake();
        VictoryUI.SetActive(false);
        LoseUI.SetActive(false);
    }

    public void ShowEndingUI(bool set)
    {
        VictoryUI.SetActive(set);
        LoseUI.SetActive(!set);
    }

    public void OnButtonReturnToMenu()
    {
        PhotonNetwork.LeaveRoom();
    }

    public override void OnLeftRoom()
    {
        base.OnLeftRoom();
        PhotonNetwork.LoadLevel(0);
    }
}
