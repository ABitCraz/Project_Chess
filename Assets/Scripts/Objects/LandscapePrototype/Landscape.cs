public class Landscape : BasicUnit, ILandscape
{
    public string LandscapeName;
    public int EffectRange;
    public LandscapeType LandscapeType;

    public void LoadLandscapeSprite()
    {
        SwapSprite(EssenitalDatumLoader.SpriteDictionary[LandscapeType]);
    }

    public virtual void ChessStepOff(ref Chess stepchess){}
    public virtual void ChessStepOn(ref Chess stepchess){}
    public virtual void ConstructionDestoryed(ref Construction plantconstruction){}
    public virtual void ConstructionPlantOn(ref Construction plantconstruction){}
}
