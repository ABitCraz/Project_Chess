public class Ancient : Landscape
{
    public Ancient()
    {
        this.LandscapeName = "遗迹";
        this.LandscapeType = LandscapeType.Ancient;

        this.MovementPrice = 2;
        this.DefenseEffectPercent = 150;
        this.VisionRangeEffectInteger = -1;
        this.IsTroopersOnly = true;
    }

    public void GetAttacked(ref Slot attack_slot)
    {
        attack_slot.InitializeOrSwapLandscape(LandscapeType.Wilderness);
    }
}
