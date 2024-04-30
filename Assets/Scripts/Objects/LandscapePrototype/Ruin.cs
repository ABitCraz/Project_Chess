using System;
[Serializable]
public class Ruin : Landscape
{
    public Ruin()
    {
        this.LandscapeName = "废墟";
        this.LandscapeType = LandscapeType.Ruin;
        
    }

    public override void ChessStepOff(ref Chess stepchess)
    {
        throw new NotImplementedException();
    }

    public override void ChessStepOn(ref Chess stepchess)
    {
        throw new NotImplementedException();
    }

    public override void ConstructionDestoryed(ref Construction plantconstruction)
    {
        throw new NotImplementedException();
    }

    public override void ConstructionPlantOn(ref Construction plantconstruction)
    {
        throw new NotImplementedException();
    }
}
