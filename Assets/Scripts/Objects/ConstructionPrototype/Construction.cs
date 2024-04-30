using System;

[Serializable]
public abstract class Construction : BasicUnit, IConstruction
{
    public string ConstructionName;
    public ConstructionType ConstructionType;

    public void LoadConstructionSprite()
    {
        SwapSprite(EssenitalDatumLoader.SpriteDictionary[ConstructionType]);
    }

    public abstract void ChessStepOff(ref Chess stepchess);
    public abstract void ChessStepOn(ref Chess stepchess);
    public abstract void LandscapeDestoryedOn(ref Landscape plantlandscape);
    public abstract void LandscapePlantOn(ref Landscape plantlandscape);
}
