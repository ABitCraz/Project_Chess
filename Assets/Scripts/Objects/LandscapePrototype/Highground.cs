[System.Serializable]
public class Highland : Landscape
{
    public Highland()
    {
        this.LandscapeName = "高地";
        this.LandscapeType = LandscapeType.Highground;
    }

    public override void EffectChess(ref Chess stepchess)
    {
        stepchess.AttackRange += 1;
        stepchess.Movement -= 1;
        stepchess.Vision += 2;
    }
}
