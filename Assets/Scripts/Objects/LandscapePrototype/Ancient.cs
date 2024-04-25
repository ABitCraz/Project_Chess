public class Ancient : Landscape
{
    public Ancient()
    {
        this.LandscapeName = "遗迹";
        this.LandscapeType = LandscapeType.Ancient;
    }

    public override void EffectChess(ref Chess stepchess) { }
}
