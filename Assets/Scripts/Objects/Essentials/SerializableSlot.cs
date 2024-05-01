using System;
using UnityEngine;

[Serializable]
public class SerializableSlot
{
    public LandscapeType S_LandscapeType;
    public ConstructionType S_ConstructionType;
    public ChessType S_ChessType;
    public float Chess_HealthPoint;
    public Vector2 MapPosition;
    public Vector3 FactPosition;

    public SerializableSlot(Slot swapslot)
    {
        if (swapslot.Landscape != null)
        {
            this.S_LandscapeType = swapslot.Landscape.LandscapeType;
        }
        if (swapslot.Construction != null)
        {
            this.S_ConstructionType = swapslot.Construction.ConstructionType;
        }
        if (swapslot.Chess != null)
        {
            this.S_ChessType = swapslot.Chess.ChessType;
            this.Chess_HealthPoint = swapslot.Chess.HealthPoint;
        }
        this.MapPosition = swapslot.Position;
        this.FactPosition = swapslot.FactPosition;
    }

    public Slot SwitchToNormalSlot()
    {
        Slot swapslot = new(S_LandscapeType, S_ConstructionType, S_ChessType)
        {
            Position = MapPosition,
            FactPosition = FactPosition
        };
        if (swapslot.Chess != null)
        {
            swapslot.Chess.HealthPoint = Chess_HealthPoint;
        }
        return swapslot;
    }
}
