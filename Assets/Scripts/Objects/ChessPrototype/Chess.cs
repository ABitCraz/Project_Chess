using UnityEngine;

public abstract class Chess{
    public string ChessName;
    public Sprite ChessSprite;
    public Vector2 Position;
    public ChessType ChessType;
    public float HealthPoint;
    public int AttackPoint;
    public int DefensePoint;
    public int Movement;
    public int AttackRange;
    public int Vision;
}
