public class Heavy : Chess
{
    public Heavy()
    {
        this.ChessName = "坦克";
        this.ChessType = ChessType.Heavy;

        this.Movement = 3;
        this.AttackRange = new int[]{1,2};
        this.VisionInRange = 2;
    }
}
