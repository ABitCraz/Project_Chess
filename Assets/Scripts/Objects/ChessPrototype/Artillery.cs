
[System.Serializable]
public class Artillery : Chess
{
    public Artillery()
    {
        this.Movement = 2;
        this.AttackRange = 2;
        this.Vision = 3;
        this.ChessType = ChessType.Artillery;
    }
}
