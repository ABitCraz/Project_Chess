[System.Serializable]
public class Infantry : Chess
{
    public Infantry()
    {
        this.Movement = 4;
        this.AttackRange = 1;
        this.Vision = 3;
        this.ChessType = ChessType.Infantry;
    }
}