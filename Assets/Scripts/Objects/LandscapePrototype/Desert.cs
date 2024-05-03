public class Desert : Landscape
{
    public bool IsQuicksand = false;
    public Desert()
    {
        this.LandscapeName = "沙漠";
        this.LandscapeType = LandscapeType.Desert;

        this.MovementPrice = 2;
        this.DefenceEffectPercent = 50;
        this.IsTroopersOnly = true;
    }
}
