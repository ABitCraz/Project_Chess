
public class Wildlessness : Landscape
{
    public bool IsSandstorming = false;
    public Wildlessness()
    {
        this.LandscapeName = "荒地";
        this.LandscapeType = LandscapeType.Wildlessness;

        this.MovementPrice = 1;
    }

    public void GetAttacked()
    {
        this.IsSandstorming = true;
        this.DefenceEffectPercent = 150;
    }

    public void SandstormOut()
    {
        this.IsSandstorming = false;
        this.DefenceEffectPercent = 100;
    }
}
