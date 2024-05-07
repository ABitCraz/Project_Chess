using System;
using System.Collections;
using System.Collections.Generic;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using EventCode = Tools.EventCode;
using Tools;
using UnityTemplateProjects.Tools;

public class NetworkEventManager : SingletonPunCallbacks<NetworkEventManager>, IOnEventCallback
{
    protected override void Awake()
    {
        base.Awake();
    }

    public override void OnEnable()
    {
        PhotonNetwork.AddCallbackTarget(this);
    }

    public override void OnDisable()
    {
        PhotonNetwork.RemoveCallbackTarget(this);
    }

    public void OnEvent(EventData photonEvent)
    {
        switch ((EventCode)photonEvent.Code)
        {
            case EventCode.ActionSend:
                ReceivePlayerAction(photonEvent);
                break;
            case EventCode.RoundState:
                ReceiveRoundState(photonEvent);
                break;
        }
    }

    #region reciever

    private void ReceivePlayerAction(EventData eventData)
    {
        Dictionary<byte, object> tmp_actionData = (Dictionary<byte, object>)eventData.CustomData;
        Photon.Realtime.Player sender = (Photon.Realtime.Player)tmp_actionData[0];
        Debug.Log("收到action消息 from" + sender.ActorNumber);
        List<PlanActions> tmp_actionsList = new();
        for (int i = 1; i < tmp_actionData.Count; i++)
        {
            tmp_actionsList.Add(
                JsonUtility.FromJson<PlanActions>((string)tmp_actionData[Convert.ToByte(i)])
            );
        }

        if (sender.IsMasterClient)
        {
            GameController.GetInstance().MasterActionList = tmp_actionsList;
            GameController.GetInstance().masterActionReady = true;
        }
        else
        {
            GameController.GetInstance().CustomerActionList = tmp_actionsList;
            GameController.GetInstance().customerActionReady = true;
        }
    }

    private void ReceiveRoundState(EventData eventData)
    {
        Dictionary<byte, object> tmp_roundStateData =
            (Dictionary<byte, object>)eventData.CustomData;
        Debug.Log("收到round消息:" + tmp_roundStateData[0]);
        switch ((int)tmp_roundStateData[0])
        {
            case 1:
                if (PhotonNetwork.LocalPlayer.IsMasterClient)
                {
                    GameController.GetInstance().customerRoundBeginReady = true;
                }
                break;
            case 2:
                Debug.Log("开始回合动");
                GameController.GetInstance().BeginTheRound();
                break;
            default:
                break;
        }
    }

    #endregion


    #region Sender

    public void SendPlayerActionEvent(List<PlanActions> actionsList)
    {
        if (!PhotonNetwork.IsConnected)
        {
            return;
        }

        Dictionary<byte, object> tmp_actionData = new Dictionary<byte, object>();

        tmp_actionData.Add(0, PhotonNetwork.LocalPlayer);
        for (int i = 0; i < actionsList.Count; i++)
        {
            tmp_actionData.Add(Convert.ToByte(i + 1), JsonUtility.ToJson(actionsList[i]));
        }

        RaiseEventOptions tmp_RaiseEventOptions = new RaiseEventOptions()
        {
            Receivers = ReceiverGroup.All
        };
        SendOptions tmp_SendOptions = SendOptions.SendReliable;
        PhotonNetwork.RaiseEvent(
            (byte)EventCode.ActionSend,
            tmp_actionData,
            tmp_RaiseEventOptions,
            tmp_SendOptions
        );
    }

    /// <summary>
    /// 发送对局状态
    /// </summary>
    /// <param name="set">状态代码（0-房间重置或者创建, 1-从机确认可以开始，2-对局开始，3-对局结束，4-回合结束,5,游戏结束）</param>
    public void SendRoundState(int set)
    {
        Dictionary<byte, object> tmp_RoundState = new Dictionary<byte, object>();
        tmp_RoundState.Add(0, set);
        RaiseEventOptions tmp_RaiseEventOptions = new RaiseEventOptions()
        {
            Receivers = ReceiverGroup.All
        };
        SendOptions tmp_SendOptions = SendOptions.SendReliable;
        PhotonNetwork.RaiseEvent(
            (byte)EventCode.RoundState,
            tmp_RoundState,
            tmp_RaiseEventOptions,
            tmp_SendOptions
        );
    }
    #endregion
}
