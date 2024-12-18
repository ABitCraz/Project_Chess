public class Ruin : Landscape
{
    public Ruin()
    {
        this.LandscapeName = "废墟";
        this.LandscapeType = LandscapeType.Ruin;

        this.MovementPrice = 2;
        this.DefenseEffectPercent = 150;
        this.VisionRangeEffectInteger = -1;
        this.IsTroopersOnly = true;
    }

    public void GetAttacked(ref Slot attackslot)
    {
        attackslot.InitializeOrSwapLandscape(LandscapeType.Wilderness);
    }
}
