public class Chess : BasicUnit, IChess
{
    public string ChessName;
    public ChessType ChessType;
    public float HealthPoint = 10f;
    public float AttackPoint = 10f;
    public float DefensePoint = 10f;
    public int Movement;
    public int[] AttackRange;
    public int Vision;
    public int TakeDamagePercent = 100;
    public int Price;
    public bool OnAlert = false;
    public int AlertCounterBackTime = 2;
    public void LoadChessSprite()
    {
        SwapSprite(EssenitalDatumLoader.SpriteDictionary[ChessType]);
    }

}
