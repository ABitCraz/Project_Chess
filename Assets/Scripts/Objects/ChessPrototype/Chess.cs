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
    public int Price;
    public bool OnAlert = false;
    public int AlertCounterBackTime = 2;
    public Slot TheSlotStepOn;
    public Player Owner;

    public void LoadChessSprite()
    {
        SwapSprite(EssenitalDatumLoader.SpriteDictionary[ChessType]);
        this.UnitGameObject.GetComponent<Animator>().runtimeAnimatorController =
            Resources.Load(ResourcePaths.TargetAnimators[ChessType]) as RuntimeAnimatorController;
    }

    public void MoveToAnotherSlot(ref Slot targetSlot)
    {
        TheSlotStepOn.Chess = null;
        TheSlotStepOn = targetSlot;
    }
}
