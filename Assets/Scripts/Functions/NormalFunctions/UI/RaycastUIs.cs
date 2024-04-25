using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastUIs
{
    static GameObject previousclickslot;
    static GameObject previoushoverslot;

    public static void UIHit(ref GameObject[] hitobject)
    {
        switch (hitobject[0].tag)
        {
            case "Slot":
                SlotOnHover(hitobject[0]);
                SlotOnClick(hitobject[0]);
                OpenSlotDropDown(hitobject[0], hitobject[1]);
                break;
        }
    }

    private static void SlotOnHover(GameObject hitslot)
    {
        if ((hitslot != previoushoverslot) && (previoushoverslot != null))
        {
            previoushoverslot.transform.GetChild(0).gameObject.SetActive(false);
        }
        previoushoverslot = hitslot;
        hitslot.transform.GetChild(0).gameObject.SetActive(true);
    }

    private static void SlotOnClick(GameObject hitslot)
    {
        if (Input.GetMouseButtonDown(0))
        {
            if ((hitslot != previousclickslot) && (previousclickslot != null))
            {
                previousclickslot.transform.GetChild(1).gameObject.SetActive(false);
            }
            previousclickslot = hitslot;
            hitslot.transform.GetChild(1).gameObject.SetActive(true);
        }
    }

    private static void OpenSlotDropDown(GameObject hitslot, GameObject slotdropdown)
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (!slotdropdown.activeSelf)
            {
                slotdropdown.SetActive(true);
                slotdropdown.transform.GetChild(0).gameObject.SetActive(true);
                slotdropdown.GetComponent<RectTransform>().position = Input.mousePosition;
            }
            else
            {
                slotdropdown.GetComponent<RectTransform>().position = Input.mousePosition;
            }
            ShootTheMouseRay.endClickActions += () =>
            {
                slotdropdown.SetActive(false);
            };
        }
    }
}
