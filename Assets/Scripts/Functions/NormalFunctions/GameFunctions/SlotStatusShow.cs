using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SlotStatusShow
{
    SlotCalculator slotcalc = new();

    public void ShowStatus(Slot currentslot, GameObject showset)
    {
        if (currentslot.Landscape == null)
        {
            showset.transform.GetChild(0).gameObject.SetActive(false);
        }
        else
        {
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
            showset.transform.GetChild(2).GetChild(0).GetComponent<TMP_Text>().text = currentslot
                .Chess
                .ChessName;
            showset.transform.GetChild(2).GetChild(1).GetComponent<Image>().sprite = currentslot
                .Chess
                .UnitSprite;
        }
    }

    public Slot[] ShowAttackRange(Slot currentslot, SlotMap slotmap)
    {
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
