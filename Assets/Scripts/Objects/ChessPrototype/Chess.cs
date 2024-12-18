using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class Chess : BasicUnit, IChess
{
    public string ChessName;
    public ChessType ChessType;
    public float HealthPoint = 10f;
    public float AttackPoint = 10f;
    public float DefensePoint = 10f;
    public int Movement;
    public int[] AttackRange;
    public int VisionInRange;
    public int VisionBlindRange = -1;
    public int TakeDamagePercent = 100;
    public ActionType CurrentAction = ActionType.Hold;
    public List<Chess> AttackedChessOnAlert = new();
    public bool OnAlert = false;
    public int AlertCounterBackTime = 2;
    public Slot TheSlotStepOn;
    public Player Owner;
    public bool IsStanding = true;
    public bool IsMoving = true;
    public bool IsCloaking = false;

    public Chess()
    {
        this.CurrentAction = ActionType.Hold;
    }

    public override void LoadSpriteAndAnimation()
    {
        LoadSprite(EssentialDatumLoader.SpriteDictionary[ChessType]);
        this.UnitGameObject.GetComponent<Animator>().runtimeAnimatorController =
            Resources.Load(ResourcePaths.TargetAnimators[ChessType]) as RuntimeAnimatorController;
    }

    public void MoveToAnotherSlot(Slot target_slot)
    {
        if (TheSlotStepOn != null)
        {
            TheSlotStepOn.Chess = null;
        }
        if (target_slot.Chess == null)
        {
            TheSlotStepOn = target_slot;
            target_slot.Chess = this;
            this.PutToSlotPosition(ref target_slot.SlotGameObject);
            this.UnitGameObject.transform.SetParent(target_slot.SlotGameObject.transform);
        }
    }
}
