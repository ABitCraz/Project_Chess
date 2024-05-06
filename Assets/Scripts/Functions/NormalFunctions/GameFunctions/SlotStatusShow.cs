using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SlotStatusShow
{
    SlotCalculator slotcalc = new();
    Slot lastslot;

    public void ShowStatus(ref Slot currentslot, ref GameObject showset)
    {
        if (currentslot.Landscape == null)
        {
            showset.transform.GetChild(0).gameObject.SetActive(false);
        }
        else
        {
            showset.transform.GetChild(0).gameObject.SetActive(true);
            showset.transform.GetChild(0).GetChild(0).GetComponent<TMP_Text>().text = currentslot
                .Landscape
                .LandscapeName;
            showset.transform.GetChild(0).GetChild(1).GetComponent<Image>().sprite = currentslot
                .Landscape
                .UnitSprite;
        }

        if (currentslot.Construction == null)
        {
            showset.transform.GetChild(1).gameObject.SetActive(false);
        }
        else
        {
            showset.transform.GetChild(1).gameObject.SetActive(true);
            showset.transform.GetChild(1).GetChild(0).GetComponent<TMP_Text>().text = currentslot
                .Construction
                .ConstructionName;
            showset.transform.GetChild(1).GetChild(1).GetComponent<Image>().sprite = currentslot
                .Construction
                .UnitSprite;
        }

        if (currentslot.Chess == null)
        {
            showset.transform.GetChild(2).gameObject.SetActive(false);
        }
        else
        {
            showset.transform.GetChild(2).gameObject.SetActive(true);
            showset.transform.GetChild(2).GetChild(0).GetComponent<TMP_Text>().text = currentslot
                .Chess
                .ChessName;
            showset.transform.GetChild(2).GetChild(1).GetComponent<Image>().sprite = currentslot
                .Chess
                .UnitSprite;
        }
    }

    public Slot[] ShowAttackRange(ref Slot currentslot, ref SlotMap slotmap)
    {
        if (lastslot != null && lastslot != currentslot)
        {
            Slot[] lastslotsinattackrange = slotcalc.CalculateSlotInAttackRange(
                ref lastslot,
                ref slotmap.FullSlotDictionary,
                ref slotmap.MapSize
            );
            for (int i = 0; i < lastslotsinattackrange.Length; i++)
            {
                lastslotsinattackrange[i].SlotGameObject
                    .GetComponent<SlotComponent>()
                    .UnfocusingActions();
            }
        }
        lastslot = currentslot;

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

    public Slot[] ShowVisionRange(ref Slot currentslot, ref SlotMap slotmap)
    {
        if ((lastslot != null) && (lastslot != currentslot))
        {
            Slot[] lastslotsinattackrange = slotcalc.CalculateSlotInAttackRange(
                ref lastslot,
                ref slotmap.FullSlotDictionary,
                ref slotmap.MapSize
            );
            for (int i = 0; i < lastslotsinattackrange.Length; i++)
            {
                lastslotsinattackrange[i].SlotGameObject
                    .GetComponent<SlotComponent>()
                    .UnfocusingActions();
            }
        }
        lastslot = currentslot;

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
}
