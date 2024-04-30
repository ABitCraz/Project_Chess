using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SlotComponent : MonoBehaviour
{
    public Slot thisSlot = new(LandscapeType.Wildlessness,ConstructionType.City,ChessType.Heavy);
    public GameObject SlotContainer;
}
