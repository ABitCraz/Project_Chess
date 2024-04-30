using System;
[Serializable]
public class Canyon : Landscape
{
    public Canyon()
    {
        this.LandscapeName = "峡谷";
        this.LandscapeType = LandscapeType.Canyon;
        
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
