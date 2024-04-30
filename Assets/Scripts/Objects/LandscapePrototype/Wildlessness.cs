using System;

[Serializable]
public class Wildlessness : Landscape
{
    public Wildlessness()
    {
        this.LandscapeName = "荒地";
        this.LandscapeType = LandscapeType.Wildlessness;
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
