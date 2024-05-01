public class Canyon : Landscape
{
    public Canyon()
    {
        this.LandscapeName = "峡谷";
        this.LandscapeType = LandscapeType.Canyon;
        
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
