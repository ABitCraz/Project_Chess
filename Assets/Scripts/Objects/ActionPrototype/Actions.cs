using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public class Actions
{
    SlotCalculator slotcalculator = new();

    public void Hold(Chess CurrentChess)
    {
        Debug.Log("Holding");
        if (CurrentChess == null)
        {
            return;
        }
        CurrentChess.IsStanding = true;
        Debug.Log("Hold End");
    }

    public void Attack(ref Slot currentslot, ref Slot targetslot)
    {
        Debug.Log("Attacking");
        Chess TargetChess = targetslot.Chess ?? null;
        Chess CurrentChess = currentslot.Chess ?? null;
        if (targetslot.Chess != null)
        {
            TargetChess = targetslot.Chess;
        }
        if (CurrentChess == null)
        {
            return;
        }
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
                    * TypeAttackPercent(CurrentChess, TargetChess)
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

        AttackChangedLandscape(ref targetslot);

        if (TargetChess != null)
        {
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
                        * TypeAttackPercent(TargetChess, CurrentChess)
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

            AttackChangedLandscape(ref targetslot);
            CurrentChess.HealthPoint -= counterback;
            if (CurrentChess.HealthPoint <= 0)
            {
                CurrentChess = null;
            }
        }
        Debug.Log("Attack End");
    }

    private void AttackChangedLandscape(ref Slot targetslot)
    {
        if (targetslot.Landscape != null)
        {
            switch (targetslot.Landscape.LandscapeType)
            {
                case LandscapeType.Wildlessness:
                    Wildlessness targetwln = targetslot.Landscape as Wildlessness;
                    if (!targetwln.IsSandstorming)
                    {
                        targetwln.IsSandstorming = true;
                    }
                    break;
                case LandscapeType.Desert:

                    Desert targetdesert = targetslot.Landscape as Desert;
                    if (!targetdesert.IsQuicksand)
                    {
                        targetdesert.IsQuicksand = true;
                    }
                    break;
                case LandscapeType.Ruin:
                    Ruin targetruin = targetslot.Landscape as Ruin;
                    targetruin.GetAttacked(ref targetslot);
                    break;
                case LandscapeType.Ancient:
                    Ruin targetancient = targetslot.Landscape as Ruin;
                    targetancient.GetAttacked(ref targetslot);
                    break;
            }
        }
    }

    public IEnumerable<bool> Move(List<Slot> route, Slot currentslot)
    {
        Debug.Log("Moving");
        Chess CurrentChess = currentslot.Chess ?? null;
        while (route.Count > 0)
        {
            Debug.Log(route[0].Position.x + " " + route[0].Position.y);
            Debug.Log(CurrentChess);
            if (CurrentChess == null)
            {
                break;
            }
            CurrentChess.CurrentAction = ActionType.Move;
            CurrentChess.IsStanding = false;
            CurrentChess.IsMoving = true;
            CurrentChess.MoveToAnotherSlot(route[0]);
            route.RemoveAt(0);
            if (route.Count <= 0)
            {
                CurrentChess.IsMoving = false;
                break;
            }
            yield return false;
        }
        yield return true;
        Debug.Log("Move End");
    }

    public void Alert(List<Slot> route, ref Slot currentslot)
    {
        Debug.Log("Alerting");
        Chess CurrentChess = currentslot.Chess ?? null;
        if (CurrentChess == null)
        {
            return;
        }
        if (route.Count <= 0)
        {
            CurrentChess.IsStanding = true;
        }
        CurrentChess.OnAlert = true;
        Debug.Log("Alert End");
    }

    public void Alarm(ref Slot currentslot, ref SlotMap CurrentSlotMap, ref Player CurrentPlayer)
    {
        Chess CurrentChess = currentslot.Chess ?? null;
        Debug.Log("Alarming");
        Slot[] slotinrange = slotcalculator.CalculateSlotInAttackRange(
            ref CurrentChess.TheSlotStepOn,
            ref CurrentSlotMap.FullSlotDictionary,
            ref CurrentSlotMap.MapSize
        );
        for (int i = 0; i < slotinrange.Length; i++)
        {
            if (CurrentChess.AlertCounterBackTime <= 0)
            {
                CurrentChess.AttackedChessOnAlert.Clear();
                GameController.GetInstance().ChessesOnAlert.Remove(CurrentChess);
                break;
            }
            if (
                slotinrange[i].Chess != null
                && slotinrange[i].Chess.Owner != CurrentPlayer
                && slotinrange[i].Chess.IsMoving == true
                && CurrentChess.AlertCounterBackTime > 0
            )
            {
                Chess TargetChess = slotinrange[i].Chess;
                if (!CurrentChess.AttackedChessOnAlert.Contains(TargetChess))
                {
                    Attack(ref currentslot, ref slotinrange[i]);
                    CurrentChess.AttackedChessOnAlert.Add(TargetChess);
                }
                if (CurrentChess == null)
                {
                    return;
                }
                CurrentChess.AlertCounterBackTime--;
            }
        }
        Debug.Log("Alarm End");
    }

    public IEnumerable<bool> Push(
        List<Slot> route,
        Slot currentslot,
        SlotMap CurrentSlotMap,
        Player CurrentPlayer
    )
    {
        Chess CurrentChess = currentslot.Chess ?? null;
        Debug.Log("Pushing");
        CurrentChess.IsStanding = false;
        CurrentChess.IsMoving = true;
        while (route.Count > 0)
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
                    Attack(ref currentslot, ref slotinrange[j]);
                }
            }
            CurrentChess.MoveToAnotherSlot(route[0]);
            route.RemoveAt(0);
            yield return false;
        }
        yield return true;
        Debug.Log("Push End");
    }

    public void Repair(ref Slot currentslot)
    {
        Debug.Log("Repairing");
        Chess CurrentChess = currentslot.Chess ?? null;
        if (CurrentChess == null)
        {
            return;
        }
        CurrentChess.HealthPoint += 5;
        if (CurrentChess.HealthPoint > 10)
        {
            CurrentChess.HealthPoint = 10;
        }
        Debug.Log("Repair End");
    }

    public void Reinforce(ref Slot reinforceslot, ChessType reinforcechesstype)
    {
        Debug.Log("Reinforing");
        reinforceslot.InitializeOrSwapChess(reinforcechesstype);
        Debug.Log("Reinfore End");
    }

    public int TypeAttackPercent(Chess CurrentChess, Chess TargetChess)
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
