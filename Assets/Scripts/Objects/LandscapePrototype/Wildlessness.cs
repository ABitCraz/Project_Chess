
public class Wildlessness : Landscape
{
    public Wildlessness()
    {
        this.LandscapeName = "荒地";
        this.LandscapeType = LandscapeType.Wildlessness;
    }

    public override void ChessStepOff(ref Chess stepchess)
    {
    }

    public override void ChessStepOn(ref Chess stepchess)
    {
    }

    public override void ConstructionDestoryed(ref Construction plantconstruction)
    {
    }

    public override void ConstructionPlantOn(ref Construction plantconstruction)
    {
    }
}
