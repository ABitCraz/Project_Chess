public class Commander : Chess
{
    public Commander()
    {
        this.ChessName = "将军";
        this.ChessType = ChessType.Commander;

        this.Movement = 2;
        this.AttackRange = new int[] { 0, 1 };
        this.Vision = 3;
    }
}
