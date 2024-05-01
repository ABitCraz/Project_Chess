public class Chess : BasicUnit, IChess
{
    public string ChessName;
    public ChessType ChessType;
    public float HealthPoint = 10;
    public int AttackPoint = 10;
    public int DefensePoint = 10;
    public int Movement;
    public int[] AttackRange;
    public int Vision;
    public int TakeDamagePercent = 100;
    public int Price;
    public void LoadChessSprite()
    {
        SwapSprite(EssenitalDatumLoader.SpriteDictionary[ChessType]);
    }

}
