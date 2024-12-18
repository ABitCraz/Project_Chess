
public class Wilderness : Landscape
{
    public bool IsSandStorming = false;

    public Wilderness()
    {
        this.LandscapeName = "荒地";
        this.LandscapeType = LandscapeType.Wilderness;
        this.MovementPrice = 1;
    }

    public void GetAttacked()
    {
        this.IsSandStorming = true;
        this.VisionRangeEffectInteger = -5;
        this.DefenseEffectPercent = 150;
        this.IsOvershadowed = true;
    }

    public void SandstormOut()
    {
        this.IsSandStorming = false;
        this.VisionRangeEffectInteger = -1;
        this.DefenseEffectPercent = 100;
        this.IsOvershadowed = false;
    }
}
