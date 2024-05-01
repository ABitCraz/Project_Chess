public class Infantry : Chess
{
    public Infantry()
    {
        this.ChessName = "尖兵";
        this.ChessType = ChessType.Infantry;

        this.Movement = 4;
        this.AttackRange = new int[] {0,1};
        this.Vision = 3;
        this.Price = 3;
    }
}
