public class AA_Infantry : Chess
{
    public AA_Infantry()
    {
        this.ChessName = "反装甲步兵";
        this.ChessType = ChessType.AA_Infantry;

        this.Movement = 3;
        this.AttackRange = new int[] { 0, 1 };
        this.Vision = 3;
        this.Price = 4;
    }
}
