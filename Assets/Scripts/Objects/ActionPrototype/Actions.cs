using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Actions
{
    public Vector2Int TargetPosition;
    public Chess CurrentChess;
    public Chess TargetChess;
    public Player CurrentPlayer;
    public SlotMap CurrentSlotMap;
    SlotCalculator slotcalculator;

    public void Attack()
    {
        if (CurrentChess == null)
        {
            return;
        }
        CurrentChess.CurrentAction = ActionType.Attack;
        if (TargetChess == null)
        {
            return;
        }
        int damage = (int)(
            CurrentChess.HealthPoint
            / 10
            * (
                CurrentChess.AttackPoint
                    * CurrentChess.TheSlotStepOn.Landscape.AttackEffectPercent
                    * TypeAttackPercent()
                    / 100
                - TargetChess.DefensePoint
                    * TargetChess.TheSlotStepOn.Landscape.DefenceEffectPercent
            )
            / 100
        );
        if (!CurrentChess.IsStanding)
        {
            damage /= 2;
        }

        TargetChess.HealthPoint -= damage;
        if (TargetChess.HealthPoint <= 0)
        {
            TargetChess.UnitGameObject.SetActive(false);
            TargetChess = null;
            return;
        }

        int counterback = (int)(
            TargetChess.HealthPoint
            / 10
            * (
                TargetChess.AttackPoint
                    * TargetChess.TheSlotStepOn.Landscape.AttackEffectPercent
                    * TypeAttackPercent()
                    / 100
                - CurrentChess.DefensePoint
                    * CurrentChess.TheSlotStepOn.Landscape.DefenceEffectPercent
            )
            / 100
        );
        if (!TargetChess.IsStanding)
        {
            counterback /= 2;
        }

        CurrentChess.HealthPoint -= counterback;

        if (CurrentChess.HealthPoint <= 0)
        {
            CurrentChess = null;
        }
    }

    public IEnumerator<bool> Move(Slot[] route)
    {
        if (CurrentChess == null)
        {
            yield return true;
        }
        CurrentChess.CurrentAction = ActionType.Move;
        CurrentChess.IsStanding = false;
        CurrentChess.IsMoving = true;
        for (int i = 0; i < route.Length; i++)
        {
            CurrentChess.MoveToAnotherSlot(ref route[i]);
            if (CurrentChess == null)
            {
                yield return true;
                break;
            }
            yield return false;
        }
        if (CurrentChess != null)
        {
            CurrentChess.IsMoving = false;
        }
        yield return true;
    }

    public void Alert(Slot[] route)
    {
        if (CurrentChess == null)
        {
            return;
        }
        CurrentChess.CurrentAction = ActionType.Alert;
        if (route.Length > 0)
        {
            Move(route);
        }
        else
        {
            CurrentChess.IsStanding = true;
        }
        CurrentChess.OnAlert = true;
    }

    public void Alarm()
    {
        if (CurrentChess.CurrentAction == ActionType.Alert)
        {
            Slot[] slotinrange = slotcalculator.CalculateSlotInAttackRange(
                ref CurrentChess.TheSlotStepOn,
                ref CurrentSlotMap.FullSlotDictionary,
                ref CurrentSlotMap.MapSize
            );
            for (int i = 0; i < slotinrange.Length; i++)
            {
                if (CurrentChess.AlertCounterBackTime <= 0)
                {
                    break;
                }
                if (
                    slotinrange[i].Chess != null
                    && slotinrange[i].Chess.Owner != CurrentPlayer
                    && slotinrange[i].Chess.IsMoving == true
                    && CurrentChess.AlertCounterBackTime > 0
                )
                {
                    TargetChess = slotinrange[i].Chess;
                    if (!CurrentChess.AttackedChessOnAlert.Contains(TargetChess))
                    {
                        Attack();
                        CurrentChess.AttackedChessOnAlert.Add(TargetChess);
                    }
                    if (CurrentChess == null)
                    {
                        return;
                    }
                    CurrentChess.AlertCounterBackTime--;
                }
            }
        }
    }

    public IEnumerator<bool> Push(Slot[] route)
    {
        CurrentChess.CurrentAction = ActionType.Push;
        int ActionPrice = 300;
        if (CurrentPlayer.Resource <= ActionPrice)
        {
            yield return true;
        }
        else
        {
            CurrentPlayer.Resource -= ActionPrice;
        }
        CurrentChess.IsStanding = false;
        CurrentChess.IsMoving = true;
        for (int i = 0; i < route.Length; i++)
        {
            Slot[] slotinrange = slotcalculator.CalculateSlotInAttackRange(
                ref CurrentChess.TheSlotStepOn,
                ref CurrentSlotMap.FullSlotDictionary,
                ref CurrentSlotMap.MapSize
            );
            for (int j = 0; j < slotinrange.Length; j++)
            {
                if (slotinrange[j].Chess != null && slotinrange[j].Chess.Owner != CurrentPlayer)
                {
                    Attack();
                }
            }
            CurrentChess.MoveToAnotherSlot(ref route[i]);
            yield return false;
        }
        yield return true;
    }

    public bool Repair()
    {
        CurrentChess.CurrentAction = ActionType.Repair;
        int ActionPrice = 300;
        if (CurrentPlayer.Resource <= ActionPrice)
        {
            return false;
        }
        else
        {
            CurrentPlayer.Resource -= ActionPrice;
        }
        CurrentChess.HealthPoint += 5;
        if (CurrentChess.HealthPoint > 10)
        {
            CurrentChess.HealthPoint = 10;
        }
        return true;
    }
    public bool Reinforce(ref Slot reinforceslot, ChessType reinforcechesstype)
    {
        CurrentChess.CurrentAction = ActionType.Reinforce;
        int ActionPrice = 300 + CurrentPlayer.ChessCostDictionary[reinforcechesstype];
        if (CurrentPlayer.Resource < ActionPrice)
        {
            reinforceslot.InitializeOrSwapChess(reinforcechesstype);
            return false;
        }
        if (reinforceslot.Chess != null)
        {
            return true;
        }
        return false;
    }

    public int TypeAttackPercent()
    {
        ChessType currenttype = CurrentChess.ChessType;
        ChessType targettype = TargetChess.ChessType;
        switch (currenttype)
        {
            case ChessType.Infantry:
                switch (targettype)
                {
                    case ChessType.Infantry:
                        return 105;
                    case ChessType.AA_Infantry:
                        return 125;
                    case ChessType.Light:
                        return 75;
                    case ChessType.Heavy:
                        return 50;
                    case ChessType.Mortar:
                        return 135;
                    case ChessType.Artillery:
                        return 135;
                    case ChessType.Commander:
                        return 50;
                }
                break;
            case ChessType.AA_Infantry:
                switch (targettype)
                {
                    case ChessType.Infantry:
                        return 85;
                    case ChessType.AA_Infantry:
                        return 105;
                    case ChessType.Light:
                        return 125;
                    case ChessType.Heavy:
                        return 125;
                    case ChessType.Mortar:
                        return 135;
                    case ChessType.Artillery:
                        return 150;
                    case ChessType.Commander:
                        return 50;
                }
                break;
            case ChessType.Light:
                switch (targettype)
                {
                    case ChessType.Infantry:
                        return 150;
                    case ChessType.AA_Infantry:
                        return 150;
                    case ChessType.Light:
                        return 105;
                    case ChessType.Heavy:
                        return 115;
                    case ChessType.Mortar:
                        return 135;
                    case ChessType.Artillery:
                        return 135;
                    case ChessType.Commander:
                        return 50;
                }
                break;
            case ChessType.Heavy:
                switch (targettype)
                {
                    case ChessType.Infantry:
                        return 150;
                    case ChessType.AA_Infantry:
                        return 150;
                    case ChessType.Light:
                        return 150;
                    case ChessType.Heavy:
                        return 105;
                    case ChessType.Mortar:
                        return 150;
                    case ChessType.Artillery:
                        return 150;
                    case ChessType.Commander:
                        return 50;
                }
                break;
            case ChessType.Mortar:
                switch (targettype)
                {
                    case ChessType.Infantry:
                        return 150;
                    case ChessType.AA_Infantry:
                        return 150;
                    case ChessType.Light:
                        return 135;
                    case ChessType.Heavy:
                        return 135;
                    case ChessType.Mortar:
                        return 135;
                    case ChessType.Artillery:
                        return 135;
                    case ChessType.Commander:
                        return 50;
                }
                break;
            case ChessType.Artillery:
                switch (targettype)
                {
                    case ChessType.Infantry:
                        return 150;
                    case ChessType.AA_Infantry:
                        return 150;
                    case ChessType.Light:
                        return 150;
                    case ChessType.Heavy:
                        return 150;
                    case ChessType.Mortar:
                        return 150;
                    case ChessType.Artillery:
                        return 135;
                    case ChessType.Commander:
                        return 50;
                }
                break;
            case ChessType.Commander:

                switch (targettype)
                {
                    case ChessType.Infantry:
                        return 50;
                    case ChessType.AA_Infantry:
                        return 50;
                    case ChessType.Light:
                        return 50;
                    case ChessType.Heavy:
                        return 50;
                    case ChessType.Mortar:
                        return 50;
                    case ChessType.Artillery:
                        return 50;
                    case ChessType.Commander:
                        return 100;
                }
                break;
        }
        return 100;
    }
}
