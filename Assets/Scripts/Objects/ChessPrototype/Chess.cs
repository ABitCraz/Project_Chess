using System;
using System.Collections.Generic;
using UnityEngine;

public class Chess : BasicUnit, IChess
{
    public string ChessName;
    public ChessType ChessType;
    public float HealthPoint = 10f;
    public float AttackPoint = 10f;
    public float DefensePoint = 10f;
    public int Movement;
    public int[] AttackRange;
    public int Vision;
    public int TakeDamagePercent = 100;
    public ActionType CurrentAction = ActionType.Hold;
    public List<Chess> AttackedChessOnAlert = new();
    public bool OnAlert = false;
    public int AlertCounterBackTime = 2;
    public Slot TheSlotStepOn;
    public Player Owner;
    public bool IsStanding = true;
    public bool IsMoving = true;

    public void LoadChessSprite()
    {
        SwapSprite(EssenitalDatumLoader.SpriteDictionary[ChessType]);
        this.UnitGameObject.GetComponent<Animator>().runtimeAnimatorController =
            Resources.Load(ResourcePaths.TargetAnimators[ChessType]) as RuntimeAnimatorController;
    }

    public void MoveToAnotherSlot(Slot targetslot)
    {
        if (TheSlotStepOn != null)
        {
            TheSlotStepOn.Chess = null;
        }
        if (targetslot.Chess == null)
        {
            TheSlotStepOn = targetslot;
            targetslot.Chess = this;
            this.PutToSlotPosition(ref targetslot.SlotGameObject);
            this.UnitGameObject.transform.SetParent(targetslot.SlotGameObject.transform);
        }
    }

    public void TakeBrake()
    {
        this.CurrentAction = ActionType.Hold;
    }
}
