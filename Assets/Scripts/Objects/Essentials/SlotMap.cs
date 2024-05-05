using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SlotMap
{
    public Dictionary<int[],Slot> FullSlotDictionary = new();
    public Slot[] FullSlotMap;
    public int[] MapSize;
    public GameObject SlotMapGameObject;
    public Player[] WholePlayers;
}
