using System;

[Serializable]
public class Desert : Landscape
{
    public Desert()
    {
        this.LandscapeName = "沙漠";
        this.LandscapeType = LandscapeType.Desert;
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
