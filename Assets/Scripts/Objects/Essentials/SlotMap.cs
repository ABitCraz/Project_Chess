using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SlotMap
{
    public Dictionary<Vector2Int, Slot> FullSlotDictionary = new();
    public Slot[] FullSlotMap;
    public Vector2Int MapSize;
    public GameObject SlotMapGameObject;
    public Player[] WholePlayers;
}
