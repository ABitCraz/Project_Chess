[System.Serializable]
public class Tank : Chess
{
    public Tank()
    {
        this.Movement = 3;
        this.AttackRange = 2;
        this.Vision = 2;
        this.ChessType = ChessType.Tank;
    }
}
