/**[System.Serializable]
public class River : Landscape
{
    public River()
    {
        this.LandscapeName = "River";
//        this.LandscapeType = LandscapeType.River;
    }

    public override void EffectStepChess(ref Chess stepchess)
    {
        stepchess.Movement -= 1;
        stepchess.TakeDamagePercent += 50;
    }

    public override void EffectStepConstruction(ref Construction plantconstruction)
    {
        throw new System.NotImplementedException();
    }
}
**/