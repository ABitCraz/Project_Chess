public class Landscape : BasicUnit, ILandscape
{
    public string LandscapeName;
    public int MovementPrice;
    public int EffectRange = 0;
    public int EffectVision = 0;
    public float DefenceEffectPercent = 100;
    public float AttackEffectPercent = 100;
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
