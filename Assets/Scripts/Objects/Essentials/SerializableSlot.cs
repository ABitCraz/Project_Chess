using System;
using UnityEngine;

[Serializable]
public class SerializableSlot
{
    public LandscapeType S_LandscapeType;
    public ConstructionType S_ConstructionType;
    public ChessType S_ChessType;
    public float Chess_HealthPoint;
    public Vector2Int MapPosition;
    public Vector3 FactPosition;
    public Player Chess_Owner;

    public SerializableSlot(Slot swap_slot)
    {
        if (swap_slot.Landscape != null)
        {
            this.S_LandscapeType = swap_slot.Landscape.LandscapeType;
        }
        if (swap_slot.Construction != null)
        {
            this.S_ConstructionType = swap_slot.Construction.ConstructionType;
        }
        if (swap_slot.Chess != null)
        {
            this.S_ChessType = swap_slot.Chess.ChessType;
            this.Chess_HealthPoint = swap_slot.Chess.HealthPoint;
            this.Chess_Owner = swap_slot.Chess.Owner;
        }
        this.MapPosition = swap_slot.Position;
        this.FactPosition = swap_slot.FactPosition;
    }

    public Slot SerializableSlotSwitchToNormalSlot()
    {
        Slot swap_slot = new(S_LandscapeType, S_ConstructionType, S_ChessType)
        {
            Position = MapPosition,
            FactPosition = FactPosition
        };
        if (swap_slot.Chess != null)
        {
            swap_slot.Chess.HealthPoint = Chess_HealthPoint;
            swap_slot.Chess.Owner = Chess_Owner;
        }
        return swap_slot;
    }
}
