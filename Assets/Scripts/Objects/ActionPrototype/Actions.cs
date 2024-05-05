using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actions
{
    public int[] TargetPosition;
    public ActionType ActionType;
    public Chess CurrentChess;
    public Chess TargetChess;
    public Player CurrentPlayer;
    public int ActionPrice = 0;

    public Actions(int[] position, ActionType targetaction, Chess originchess, Player currentplayer)
    {
        this.TargetPosition = position;
        this.ActionType = targetaction;
        this.CurrentChess = originchess;
        this.CurrentPlayer = currentplayer;
    }

    public Actions(
        int[] position,
        ActionType targetaction,
        Chess originchess,
        Chess targetchess,
        Player currentplayer
    )
    {
        this.TargetPosition = position;
        this.ActionType = targetaction;
        this.CurrentChess = originchess;
        this.TargetChess = targetchess;
        this.CurrentPlayer = currentplayer;
    }

    public void Attack(bool isstanding)
    {
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
        if (!isstanding)
        {
            damage /= 2;
        }
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
