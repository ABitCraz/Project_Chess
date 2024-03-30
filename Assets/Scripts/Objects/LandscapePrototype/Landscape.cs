[System.Serializable]
public abstract class Landscape : ILandscape
{
    public string LandscapeName;
    public LandscapeType LandscapeType;
    public abstract void EffectChess(ref Chess stepchess);
}
