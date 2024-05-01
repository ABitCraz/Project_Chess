public class Desert : Landscape
{
    public Desert()
    {
        this.LandscapeName = "沙漠";
        this.LandscapeType = LandscapeType.Desert;
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
