using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public List<PlanActions> MasterActionList;
    public List<PlanActions> CustomerActionList;
    public List<Chess> ChessesOnAlert;
    public GameObject ConfirmButton;
    public bool IntoTheBreach;
    public int statement = 0;

    private void Awake()
    {
        ConfirmButton.GetComponent<Button>().onClick.AddListener(LetUsBegin);
    }

    private void Update() { }

    private void LetUsBegin()
    {
        this.GetComponent<RaycastInGame>().IsInControl = false;
    }

    public IEnumerable<int> RoundBegin()
    {
        for (int i = 0; i < 3; i++)
        {
            PlanActions mp = MasterActionList[i];
            PlanActions ca = CustomerActionList[i];
            switch (statement)
            {
                case 0:
                    CheckHold(ref mp);
                    CheckHold(ref ca);
                    statement += 1;
                    break;
                case 1:
                    CheckAlert(ref mp);
                    CheckAlert(ref ca);
                    statement += 1;
                    break;
                case 2:
                    CheckRepair(ref mp);
                    CheckRepair(ref ca);
                    break;
                case 3:
                    CheckAlertMovePush(ref mp);
                    CheckAlertMovePush(ref ca);
                    break;
            }
            yield return i;
        }
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
