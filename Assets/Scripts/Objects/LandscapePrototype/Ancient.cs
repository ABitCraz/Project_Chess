public class Ancient : Landscape
{
    public Ancient()
    {
        this.LandscapeName = "遗迹";
        this.LandscapeType = LandscapeType.Ancient;

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
