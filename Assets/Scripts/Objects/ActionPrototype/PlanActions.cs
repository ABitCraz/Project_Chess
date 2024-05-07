using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlanActions
{
    public ActionType ThisActionType;
    public Vector2Int TargetPosition;
    public Vector2Int CurrentSlotPosition;
    public Player CurrentPlayer;
    public SlotMap CurrentSlotMap;
    public Vector2Int[] PositionToRouteInPlan;
    public ChessType ReinforceChessType;

    public PlanActions(
        Vector2Int targetposition,
        ActionType actiontype,
        Slot originslot,
        Player currentplayer
    )
    {
        this.TargetPosition = targetposition;
        this.CurrentPlayer = currentplayer;
        this.CurrentSlotPosition = originslot.Position;
        this.ThisActionType = actiontype;
    }

    public PlanActions Attack(Slot target)
    {
        ThisActionType = ActionType.Attack;
        TargetPosition = target.Position;
        return this;
    }

    public PlanActions Move(Vector2Int[] route)
    {
        ThisActionType = ActionType.Move;
        PositionToRouteInPlan = route;
        return this;
    }

    public PlanActions Alert(Vector2Int[] route)
    {
        ThisActionType = ActionType.Alert;
        PositionToRouteInPlan = route;
        return this;
    }

    public PlanActions Hold()
    {
        ThisActionType = ActionType.Hold;
        return this;
    }

    public PlanActions Push(Vector2Int[] route)
    {
        ThisActionType = ActionType.Push;
        PositionToRouteInPlan = route;
        return this;
    }

    public PlanActions Repair()
    {
        ThisActionType = ActionType.Repair;
        return this;
    }

    public PlanActions Reinforce(ref Vector2Int reinforceslot, ChessType reinforcechesstype)
    {
        TargetPosition = reinforceslot;
        ReinforceChessType = reinforcechesstype;
        return this;
    }
}
