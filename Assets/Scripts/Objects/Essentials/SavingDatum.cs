using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SavingDatum
{
    public List<SerializableSlot> SaveSlots = new();
    public SlotMap SlotMap = new();

    public List<SerializableSlot> MapToSerializeSlot()
    {
        for (int i = 0; i < SlotMap.FullSlotMap.Length; i++)
        {
            SaveSlots.Add(SlotMap.FullSlotMap[i].SwapToSerializableSlot());
        }
        return SaveSlots;
    }
}
