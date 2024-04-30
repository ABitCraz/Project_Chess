using System;

[Serializable]
public class Heavy : Chess
{
    public Heavy()
    {
        this.ChessName = "坦克";
        this.ChessType = ChessType.Heavy;

        this.Movement = 3;
        this.AttackRange = 2;
        this.Vision = 2;
        this.Price = 10;
    }
}
