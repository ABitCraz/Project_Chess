public interface ILandscape
{
    public abstract void LoadLandscapeSprite();
    public abstract void ChessStepOn(ref Chess stepchess);
    public abstract void ChessStepOff(ref Chess stepchess);
    public abstract void ConstructionPlantOn(ref Construction plantconstruction);
    public abstract void ConstructionDestoryed(ref Construction plantconstruction);
}
