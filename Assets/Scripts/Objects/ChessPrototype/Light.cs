public class Light : Chess
{
    public Light()
    {
        this.ChessName = "战车";
        this.ChessType = ChessType.Light;

        this.Movement = 4;
        this.AttackRange = new int[]{0,1};
        this.Vision = 2;
    }
}
