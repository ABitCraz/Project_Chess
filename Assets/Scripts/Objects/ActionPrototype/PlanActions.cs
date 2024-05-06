using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanActions
{
    public ActionType ThisActionType;
    public Vector2Int TargetPosition;
    public Chess CurrentChess;
    public Slot TargetSlot;
    public Player CurrentPlayer;
    public SlotMap CurrentSlotMap;
    public Slot[] RouteInPlan;
    public ChessType ReinforceChessType;
    SlotCalculator slotcalculator;

    public PlanActions(
        Vector2Int position,
        ActionType targetaction,
        Chess originchess,
        Player currentplayer
    )
    {
        this.CurrentPlayer = currentplayer;
        this.TargetPosition = position;
        this.CurrentChess = originchess;
        CurrentChess.CurrentAction = targetaction;
    }

    public PlanActions Attack(Slot target)
    {
        ThisActionType = ActionType.Attack;
        TargetSlot = target;
        return this;
    }

    public PlanActions Move(Slot[] route)
    {
        ThisActionType = ActionType.Move;
        RouteInPlan = route;
        return this;
    }

    public PlanActions Alert(Slot[] route)
    {
        ThisActionType = ActionType.Alert;
        RouteInPlan = route;
        return this;
    }

    public PlanActions Hold()
    {
        ThisActionType = ActionType.Hold;
        return this;
    }

    public PlanActions Push(Slot[] route)
    {
        ThisActionType = ActionType.Push;
        RouteInPlan = route;
        return this;
    }

    public PlanActions Repair()
    {
        ThisActionType = ActionType.Repair;
        return this;
    }

    public PlanActions Reinforce(ref Slot reinforceslot, ChessType reinforcechesstype)
    {
        TargetSlot = reinforceslot;
        ReinforceChessType = reinforcechesstype;
        return this;
    }
}
