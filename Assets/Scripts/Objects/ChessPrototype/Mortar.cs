public class Mortar : Chess
{
    public Mortar()
    {
        this.ChessName = "迫击炮";
        this.ChessType = ChessType.Mortar;

        this.Movement = 3;
        this.AttackRange = new int[]{2,3};
        this.VisionInRange = 3;
    }
}
