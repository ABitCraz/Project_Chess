public class Ruin : Landscape
{
    public Ruin()
    {
        this.LandscapeName = "废墟";
        this.LandscapeType = LandscapeType.Ruin;
        
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
