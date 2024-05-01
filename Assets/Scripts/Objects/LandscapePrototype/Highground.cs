public class Highground : Landscape
{
    public Highground()
    {
        this.LandscapeName = "高地";
        this.LandscapeType = LandscapeType.Highground;
        this.EffectRange = -1;
    }

    public override void ChessStepOff(ref Chess stepchess)
    {
        stepchess.Movement += 1;
        stepchess.Vision -= 2;
    }

    public override void ChessStepOn(ref Chess stepchess)
    {
        stepchess.Movement -= 1;
        stepchess.Vision += 2;
    }

    public override void ConstructionDestoryed(ref Construction plantconstruction)
    {
    }

    public override void ConstructionPlantOn(ref Construction plantconstruction)
    {
    }
}
