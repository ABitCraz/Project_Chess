public interface IConstruction
{
    public abstract void LoadConstructionSprite();
    public abstract void ChessStepOn(ref Chess stepchess);
    public abstract void ChessStepOff(ref Chess stepchess);
    public abstract void LandscapePlantOn(ref Landscape plantlandscape);
    public abstract void LandscapeDestoryedOn(ref Landscape plantlandscape);
}
