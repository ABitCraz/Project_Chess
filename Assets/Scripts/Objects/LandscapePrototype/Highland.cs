[System.Serializable]
public class Highland : Landscape
{
    public Highland()
    {
        this.LandscapeName = "Highland";
        this.LandscapeType = LandscapeType.Highland;
    }

    public override void EffectChess(ref Chess stepchess)
    {
        stepchess.AttackRange += 1;
        stepchess.Movement -= 1;
        stepchess.Vision += 2;
    }
}
