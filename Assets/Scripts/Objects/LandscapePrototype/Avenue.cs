
public class Avenue : ILandscape
{
    public void EffectChess(ref Chess stepchess)
    {
        stepchess.Movement += 1;
    }
}
