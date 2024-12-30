using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SlotStatusShow
{
    Slot previous_attack_slot;
    Slot previous_vision_slot;

    public void ShowStatus(ref Slot current_slot, ref GameObject showset)
    {
        if (current_slot.Landscape == null)
        {
            showset.transform.GetChild(0).gameObject.SetActive(false);
        }
        else
        {
            showset.transform.GetChild(0).gameObject.SetActive(true);
            showset.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = current_slot
                .Landscape
                .UnitSprite;
            showset.transform.GetChild(0).GetChild(1).GetComponent<TMP_Text>().text = current_slot
                .Landscape
                .LandscapeName;
        }

        if (current_slot.Construction == null)
        {
            showset.transform.GetChild(1).gameObject.SetActive(false);
        }
        else
        {
            showset.transform.GetChild(1).gameObject.SetActive(true);
            showset.transform.GetChild(1).GetChild(0).GetComponent<Image>().sprite = current_slot
                .Construction
                .UnitSprite;
            showset.transform.GetChild(1).GetChild(1).GetComponent<TMP_Text>().text = current_slot
                .Construction
                .ConstructionName;
        }

        if (current_slot.Chess == null)
        {
            showset.transform.GetChild(2).gameObject.SetActive(false);
        }
        else
        {
            showset.transform.GetChild(2).gameObject.SetActive(true);
            showset.transform.GetChild(2).GetChild(0).GetComponent<Image>().sprite = current_slot
                .Chess
                .UnitSprite;
            showset.transform.GetChild(2).GetChild(1).GetComponent<TMP_Text>().text = current_slot
                .Chess
                .ChessName;
        }
    }

    public Slot[] ShowAttackRange(Slot currentslot, SlotMap slotmap)
    {
        if (previous_attack_slot != null && previous_attack_slot != currentslot)
        {
            for (int i = 0; i < slotmap.FullSlotMap.Length; i++)
            {
                //slotmap.FullSlotMap[i].SlotGameObject.GetComponent<SlotComponent>().UnfocusingActions();
            }
        }
        previous_attack_slot = currentslot;
        Slot[] slotsinattackrange = SlotCalculator.CalculateSlotInAttackRange(
            ref currentslot,
            ref slotmap.FullSlotDictionary,
            ref slotmap.MapSize
        );
        return slotsinattackrange;
    }

    public Slot[] ShowVisionRange(Slot currentslot, SlotMap slotmap)
    {
        if (previous_vision_slot != null && previous_vision_slot != currentslot)
        {
            for (int i = 0; i < slotmap.FullSlotMap.Length; i++)
            {
                //slotmap.FullSlotMap[i].SlotGameObject.GetComponent<SlotComponent>().UnfocusingActions();
            }
        }
        previous_vision_slot = currentslot;
        Slot[] slotsinattackrange = SlotCalculator.CalculateSlotInVisionRange(
            ref currentslot,
            ref slotmap.FullSlotDictionary,
            ref slotmap.MapSize
        );
        return slotsinattackrange;
    }

    public Slot[] ShowMovementRange(Slot currentslot, SlotMap slotmap)
    {
        Slot[] slotsinattackrange = SlotCalculator.CalculateSlotInMovementRange(
            ref currentslot,
            ref slotmap.FullSlotDictionary,
            ref slotmap.MapSize
        );
        return slotsinattackrange;
    }
}
