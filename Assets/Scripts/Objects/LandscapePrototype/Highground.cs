using System;

[Serializable]
public class Highground : Landscape
{
    public Highground()
    {
        this.LandscapeName = "高地";
        this.LandscapeType = LandscapeType.Highground;
    }

    public override void ChessStepOff(ref Chess stepchess)
    {
        stepchess.AttackRange -= 1;
        stepchess.Movement += 1;
        stepchess.Vision -= 2;
    }

    public override void ChessStepOn(ref Chess stepchess)
    {
        stepchess.AttackRange += 1;
        stepchess.Movement -= 1;
        stepchess.Vision += 2;
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
