[System.Serializable]
public class Artillery : Chess
{
    public Artillery()
    {
        this.ChessName = "火炮";
        this.ChessType = ChessType.Artillery;

        this.Movement = 2;
        this.AttackRange = 2;
        this.Vision = 3;
        this.Price = 11;
    }
}
