[System.Serializable]
public class Forest : Landscape
{
    public Forest()
    {
        this.LandscapeName = "Forest";
        this.LandscapeType = LandscapeType.Forest;
    }

    public override void EffectChess(ref Chess stepchess)
    {
        stepchess.Movement -= 1;
        stepchess.Vision -= 1;
        stepchess.TakeDamagePercent -= 50;
    }
}
