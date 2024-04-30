using System;
using UnityEngine;

[Serializable]
public abstract class Landscape : BasicUnit, ILandscape
{
    public string LandscapeName;
    public LandscapeType LandscapeType;

    public void LoadLandscapeSprite()
    {
        SwapSprite(EssenitalDatumLoader.SpriteDictionary[LandscapeType]);
    }

    public abstract void ChessStepOff(ref Chess stepchess);
    public abstract void ChessStepOn(ref Chess stepchess);
    public abstract void ConstructionDestoryed(ref Construction plantconstruction);
    public abstract void ConstructionPlantOn(ref Construction plantconstruction);
}
