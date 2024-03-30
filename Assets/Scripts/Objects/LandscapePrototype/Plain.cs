[System.Serializable]
public class Plain : Landscape
{
    public Plain(){
        this.LandscapeName = "Plain";
        this.LandscapeType = LandscapeType.Plain;
    }
    
    public override void EffectChess(ref Chess stepchess)
    {
        return;
    }
}
