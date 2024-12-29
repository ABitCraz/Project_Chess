using UnityEngine;

public class Landscape : BasicUnit, ILandscape
{
    public string LandscapeName;
    public int MovementPrice = 0;
    public int InfluenceRange = 0;
    public int RangeEffectInteger = 0;
    public int RangeEffectPercentage = 100;
    public int VisionRangeEffectInteger = 0;
    public int VisionRangeEffectPercentage = 100;
    public int AttackRangeEffectInteger = 0;
    public int AttackRangeEffectPercentage = 100;
    public int MovementAddonInteger = 0;
    public int MovementAddonPercentage = 100;
    public int StayMovementPaymentInteger = 0;
    public int StayMovementPaymentPercentage = 100;
    public int DefenseEffectInteger = 0;
    public int DefenseEffectPercent = 100;
    public int AttackEffectInteger = 0;
    public int AttackEffectPercent = 100;
    public LandscapeType LandscapeType;
    public bool IsTroopersOnly = false;
    public bool IsOvershadowed = false;
    public bool IsBlocked = false;
    public bool IsHidden = true;

    public override void LoadSpriteAndAnimation()
    {
        if (this.LandscapeType == LandscapeType.Empty)
            return;
        LoadSprite(EssentialDatumLoader.SpriteDictionary[LandscapeType]);
        this.UnitGameObject.GetComponent<Animator>().runtimeAnimatorController =
            Resources.Load(ResourcePaths.TargetAnimators[LandscapeType])
            as RuntimeAnimatorController;
    }

    public virtual void ChessStepOff(ref Chess step_chess) { }

    public virtual void ChessStepOn(ref Chess step_chess) { }

    public virtual void ConstructionDestroyed(ref Construction plant_construction) { }

    public virtual void ConstructionPlantOn(ref Construction plant_construction) { }
}
