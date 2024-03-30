[System.Serializable]
public class River : Landscape
{
    public River()
    {
        this.LandscapeName = "River";
        this.LandscapeType = LandscapeType.River;
    }

    public override void EffectChess(ref Chess stepchess)
    {
        stepchess.Movement -= 1;
        stepchess.TakeDamagePercent += 50;
    }
}
