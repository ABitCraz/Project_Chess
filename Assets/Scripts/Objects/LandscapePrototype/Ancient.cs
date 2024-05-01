public class Ancient : Landscape
{
    public Ancient()
    {
        this.LandscapeName = "遗迹";
        this.LandscapeType = LandscapeType.Ancient;
    }

    public override void ChessStepOff(ref Chess stepchess)
    {
        throw new System.NotImplementedException();
    }

    public override void ChessStepOn(ref Chess stepchess)
    {
        throw new System.NotImplementedException();
    }

    public override void ConstructionDestoryed(ref Construction plantconstruction)
    {
        throw new System.NotImplementedException();
    }

    public override void ConstructionPlantOn(ref Construction plantconstruction)
    {
        throw new System.NotImplementedException();
    }
}
