public class Landscape : BasicUnit, ILandscape
{
    public string LandscapeName;
    public int MovementPrice;
    public int EffectRange;
    public int EffectVision;
    public float DefenceEffectPercent;
    public float AttackEffectPercent;
    public LandscapeType LandscapeType;
    public bool IsTroopersOnly = false;

    public void LoadLandscapeSprite()
    {
        SwapSprite(EssenitalDatumLoader.SpriteDictionary[LandscapeType]);
    }

    public virtual void ChessStepOff(ref Chess stepchess){}
    public virtual void ChessStepOn(ref Chess stepchess){}
    public virtual void ConstructionDestoryed(ref Construction plantconstruction){}
    public virtual void ConstructionPlantOn(ref Construction plantconstruction){}
}
