using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameController : Singleton<GameController>
{
    public List<PlanActions> MasterActionList;
    public List<PlanActions> CustomerActionList;
    public List<Chess> ChessesOnAlert;
    public GameObject ConfirmButton;
    public bool IntoTheBreach;
    public int statement = 0;

    public bool isRoundBegin = false;
    public bool masterActionReady = false;
    public bool customerActionReady = false;
    public bool customerRoundBeginReady = false;

    protected override void Awake()
    {
        base.Awake();
        ConfirmButton.GetComponent<Button>().onClick.AddListener(LetUsBegin);
    }

    private void Update() { }

    private void LetUsBegin()
    {
        if (isRoundBegin)
        {
            return;
        }

        this.GetComponent<RaycastInGame>().IsInControl = false;
        isRoundBegin = true;
        Debug.Log(NetworkEventManager.IsInitialized);
        NetworkEventManager
            .GetInstance()
            .SendPlayerActionEvent(RaycastInGame.GetInstance().ActionList);
        StartCoroutine(WaitToStart());
    }

    private IEnumerator WaitToStart()
    {
        yield return new WaitUntil(() => masterActionReady && customerActionReady);
        if (PhotonNetwork.LocalPlayer.IsMasterClient)
        {
            yield return new WaitUntil(() => customerRoundBeginReady);
            NetworkEventManager.GetInstance().SendRoundState(2);
        }
        else
        {
            NetworkEventManager.GetInstance().SendRoundState(1);
        }
    }

    public IEnumerable<int> RoundBegin()
    {
        for (int i = 0; i < 3; i++)
        {
            PlanActions mp;
            try
            {
                mp = MasterActionList[i];
            }
            catch (Exception)
            {
                mp = null;
            }
            PlanActions ca;
            try
            {
                ca = CustomerActionList[i];
            }
            catch (Exception)
            {
                ca = null;
            }
            switch (statement)
            {
                case 0:
                    if (mp != null)
                    {
                        CheckHold(ref mp);
                    }
                    if (ca != null)
                    {
                        CheckHold(ref ca);
                    }
                    statement += 1;
                    break;
                case 1:
                    if (mp != null)
                    {
                        CheckAlert(ref mp);
                    }
                    if (ca != null)
                    {
                        CheckAlert(ref ca);
                    }
                    statement += 1;
                    break;
                case 2:
                    if (mp != null)
                    {
                        CheckAttack(ref mp);
                    }
                    if (ca != null)
                    {
                        CheckAttack(ref ca);
                    }
                    statement += 1;
                    break;
                case 3:
                    if (mp != null)
                    {
                        CheckRepair(ref mp);
                    }
                    if (ca != null)
                    {
                        CheckRepair(ref ca);
                    }
                    statement += 1;
                    break;
                case 4:
                    if (mp != null)
                    {
                        CheckAlertMovePush(ref mp);
                    }
                    if (ca != null)
                    {
                        CheckAlertMovePush(ref ca);
                    }
                    break;
            }
            yield return i;
        }
        RoundEnd();
    }

    private void RoundEnd()
    {
        isRoundBegin = false;
        RaycastInGame.GetInstance().IsInControl = true;
        masterActionReady = false;
        customerActionReady = false;
        customerRoundBeginReady = false;
    }

    private void CheckHold(ref PlanActions planaction)
    {
        if (planaction.ThisActionType == ActionType.Hold)
        {
            new Actions(planaction.CurrentChess, planaction.CurrentPlayer).Hold();
        }
    }

    private void CheckAlert(ref PlanActions planaction)
    {
        if ((planaction.ThisActionType == ActionType.Alert) && (planaction.RouteInPlan.Length <= 0))
        {
            new Actions(planaction.CurrentChess, planaction.CurrentPlayer).Alert(
                planaction.RouteInPlan
            );
            ChessesOnAlert.Add(planaction.CurrentChess);
        }
    }

    private void CheckAttack(ref PlanActions planaction)
    {
        if (planaction.ThisActionType == ActionType.Attack)
        {
            new Actions(planaction.CurrentChess, planaction.CurrentPlayer).Attack(planaction.TargetSlot);
        }
    }

    private void CheckRepair(ref PlanActions planaction)
    {
        if (planaction.ThisActionType == ActionType.Repair)
        {
            new Actions(planaction.CurrentChess, planaction.CurrentPlayer).Repair();
        }
    }

    private void CheckAlertMovePush(ref PlanActions planaction)
    {
        if (
            (planaction.ThisActionType == ActionType.Alert && planaction.RouteInPlan.Length >= 0)
            || planaction.ThisActionType == ActionType.Move
        )
        {
            foreach (
                bool result in new Actions(planaction.CurrentChess, planaction.CurrentPlayer).Move(
                    planaction.RouteInPlan
                )
            )
            {
                CallAlarm(ref planaction);
            }
            if (planaction.ThisActionType == ActionType.Alert)
            {
                new Actions(planaction.CurrentChess, planaction.CurrentPlayer).Alert(
                    planaction.RouteInPlan
                );
                ChessesOnAlert.Add(planaction.CurrentChess);
            }
        }
        else if (planaction.ThisActionType == ActionType.Push)
        {
            foreach (
                bool result in new Actions(planaction.CurrentChess, planaction.CurrentPlayer).Push(
                    planaction.RouteInPlan
                )
            )
            {
                CallAlarm(ref planaction);
            }
        }
    }

    private void CallAlarm(ref PlanActions planaction)
    {
        for (int i = 0; i < ChessesOnAlert.Count; i++)
        {
            new Actions(planaction.CurrentChess, planaction.CurrentPlayer).Alarm();
        }
    }
}
