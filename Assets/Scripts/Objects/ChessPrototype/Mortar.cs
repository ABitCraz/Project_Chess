using System;

[Serializable]
public class Mortar : Chess
{
    public Mortar()
    {
        this.ChessName = "迫击炮";
        this.ChessType = ChessType.Mortar;

        this.Movement = 3;
        this.AttackRange = 3;
        this.Vision = 3;
        this.Price = 7;
    }
}
