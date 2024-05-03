public class Ruin : Landscape
{
    public Ruin()
    {
        this.LandscapeName = "废墟";
        this.LandscapeType = LandscapeType.Ruin;

        this.MovementPrice = 2;
        this.DefenceEffectPercent = 150;
        this.EffectVision = -1;
        this.IsTroopersOnly = true;
    }

    public void GetAttacked(ref Slot attackslot)
    {
        attackslot.InitializeOrSwapLandscape(LandscapeType.Wildlessness);
    }
}
