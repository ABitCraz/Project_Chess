using System;

[Serializable]
public class Commander : Chess
{
    public Commander()
    {
        this.ChessName = "将军";
        this.ChessType = ChessType.Commander;

        this.Movement = 2;
        this.AttackRange = 1;
        this.Vision = 3;

        SwapSprite(EssenitalDatumLoader.SpriteDictionary[this.ChessType]);
    }
}
