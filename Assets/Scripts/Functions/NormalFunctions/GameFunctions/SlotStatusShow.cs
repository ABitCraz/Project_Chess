using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SlotStatusShow
{
    SlotCalculator slotcalc = new();
    Slot previousattackslot;
    Slot previousvisionslot;

    public void ShowStatus(ref Slot currentslot, ref GameObject showset)
    {
        if (currentslot.Landscape == null)
        {
            showset.transform.GetChild(0).gameObject.SetActive(false);
        }
        else
        {
            showset.transform.GetChild(0).gameObject.SetActive(true);
            showset.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = currentslot
                .Landscape
                .UnitSprite;
            showset.transform.GetChild(0).GetChild(1).GetComponent<TMP_Text>().text = currentslot
                .Landscape
                .LandscapeName;
        }

        if (currentslot.Construction == null)
        {
            showset.transform.GetChild(1).gameObject.SetActive(false);
        }
        else
        {
            showset.transform.GetChild(1).gameObject.SetActive(true);
            showset.transform.GetChild(1).GetChild(0).GetComponent<Image>().sprite = currentslot
                .Construction
                .UnitSprite;
            showset.transform.GetChild(1).GetChild(1).GetComponent<TMP_Text>().text = currentslot
                .Construction
                .ConstructionName;
        }

        if (currentslot.Chess == null)
        {
            showset.transform.GetChild(2).gameObject.SetActive(false);
        }
        else
        {
            showset.transform.GetChild(2).gameObject.SetActive(true);
            showset.transform.GetChild(2).GetChild(0).GetComponent<Image>().sprite = currentslot
                .Chess
                .UnitSprite;
            showset.transform.GetChild(2).GetChild(1).GetComponent<TMP_Text>().text = currentslot
                .Chess
                .ChessName;
        }
    }

    public Slot[] ShowAttackRange(Slot currentslot, SlotMap slotmap)
    {
        if (previousattackslot != null && previousattackslot != currentslot)
        {
            for (int i = 0; i < slotmap.FullSlotMap.Length; i++)
            {
                slotmap.FullSlotMap[i].SlotGameObject
                    .GetComponent<SlotComponent>()
                    .UnfocusingActions();
            }
        }
        previousattackslot = currentslot;
        Slot[] slotsinattackrange = slotcalc.CalculateSlotInAttackRange(
            ref currentslot,
            ref slotmap.FullSlotDictionary,
            ref slotmap.MapSize
        );
        for (int i = 0; i < slotsinattackrange.Length; i++)
        {
            slotsinattackrange[i].SlotGameObject.GetComponent<SlotComponent>().IsAttackFocusing =
                true;
        }
        return slotsinattackrange;
    }

    public Slot[] ShowVisionRange(Slot currentslot, SlotMap slotmap)
    {
        if (previousvisionslot != null && previousvisionslot != currentslot)
        {
            for (int i = 0; i < slotmap.FullSlotMap.Length; i++)
            {
                slotmap.FullSlotMap[i].SlotGameObject
                    .GetComponent<SlotComponent>()
                    .UnfocusingActions();
            }
        }
        previousvisionslot = currentslot;
        Slot[] slotsinattackrange = slotcalc.CalculateSlotInVisionRange(
            ref currentslot,
            ref slotmap.FullSlotDictionary,
            ref slotmap.MapSize
        );
        for (int i = 0; i < slotsinattackrange.Length; i++)
        {
            slotsinattackrange[i].SlotGameObject.GetComponent<SlotComponent>().IsVisionFocusing =
                true;
        }
        return slotsinattackrange;
    }

    public Slot[] ShowMovementRange(Slot currentslot, SlotMap slotmap)
    {
        Slot[] slotsinattackrange = slotcalc.CalculateSlotInMovementRange(
            ref currentslot,
            ref slotmap.FullSlotDictionary,
            ref slotmap.MapSize
        );
        for (int i = 0; i < slotsinattackrange.Length; i++)
        {
            slotsinattackrange[i].SlotGameObject.GetComponent<SlotComponent>().IsVisionFocusing =
                true;
        }
        return slotsinattackrange;
    }
}
