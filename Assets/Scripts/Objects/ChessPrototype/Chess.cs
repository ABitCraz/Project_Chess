using UnityEngine;
[System.Serializable]
public abstract class Chess{
    public string ChessName;
    public Sprite ChessSprite;
    public ChessType ChessType;
    public float HealthPoint = 10;
    public int AttackPoint = 10;
    public int DefensePoint = 10;
    public int Movement;
    public int AttackRange;
    public int Vision;
    public int TakeDamagePercent = 100;
}
