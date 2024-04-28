using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotComponent : MonoBehaviour
{
    public Slot thisSlot = new(LandscapeType.Wildlessness,ChessType.AA_Infantry);
    
    public GameObject SlotContainer;
}
