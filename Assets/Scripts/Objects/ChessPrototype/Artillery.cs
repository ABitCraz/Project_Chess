public class Artillery : Chess
{
    public Artillery()
    {
        this.ChessName = "火炮";
        this.ChessType = ChessType.Artillery;

        this.Movement = 2;
        this.AttackRange = new int[] { 3, 4 };
        this.Vision = 3;
        this.Price = 11;
    }
}
