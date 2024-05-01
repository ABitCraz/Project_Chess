public class City : Construction 
{ 
    public City()
    {
        this.ConstructionName = "城市";
        this.ConstructionType = ConstructionType.City;
    }

    public override void ChessStepOff(ref Chess stepchess)
    {
        throw new System.NotImplementedException();
    }

    public override void ChessStepOn(ref Chess stepchess)
    {
        throw new System.NotImplementedException();
    }

    public override void LandscapeDestoryedOn(ref Landscape plantlandscape)
    {
        throw new System.NotImplementedException();
    }

    public override void LandscapePlantOn(ref Landscape plantlandscape)
    {
        throw new System.NotImplementedException();
    }
}
