using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

public class RaycastUIs
{
    StringBuilder status = new();

    public void SlotOnHover(GameObject hitslot, GameObject statusshow)
    {
        /*hitslot.transform.GetChild(0).gameObject.SetActive(true);
        ShootTheMouseRay.endHoverActions += () =>
        {
            hitslot.transform.GetChild(0).gameObject.SetActive(false);
        };
        if (statusshow.TryGetComponent<TMP_Text>(out _))
        {
            ShowSlotStats(hitslot, statusshow);
        }*/
    }

    public void SlotOnClick(GameObject hitslot, GameObject slotdropdown)
    {
        /*if (Input.GetMouseButtonDown(0))
        {
            hitslot.transform.GetChild(1).gameObject.SetActive(true);
            ShootTheMouseRay.endHoverActions += () =>
            {
                hitslot.transform.GetChild(1).gameObject.SetActive(false);
            };
            if (slotdropdown.CompareTag("DropDown"))
            {
                LoadSlotToDropdown(hitslot, slotdropdown);
                //RefillDropdownEvents(hitslot, slotdropdown);
                ShowSlotDropdown(slotdropdown);
            }
        }*/
    }

    public void LoadSlotToDropdown(GameObject hitslot, GameObject slotdropdown)
    {
        Slot thisslot = hitslot.GetComponent<SlotComponent>().thisSlot;

        TMP_Dropdown landscapedropdown = slotdropdown
            .transform.GetChild(0)
            .GetComponent<TMP_Dropdown>();
        if (thisslot.Landscape != null)
        {
            landscapedropdown.value = (int)thisslot.Landscape.LandscapeType;
        }
        else
        {
            landscapedropdown.value = 0;
        }

        TMP_Dropdown constructiondropdown = slotdropdown
            .transform.GetChild(1)
            .GetComponent<TMP_Dropdown>();
        if (thisslot.Construction != null)
        {
            constructiondropdown.value = (int)thisslot.Construction.ConstructionType;
        }
        else
        {
            constructiondropdown.value = 0;
        }

        TMP_Dropdown chessdropdown = slotdropdown
            .transform.GetChild(2)
            .GetComponent<TMP_Dropdown>();
        if (thisslot.Chess != null)
        {
            chessdropdown.value = (int)thisslot.Chess.ChessType;
        }
        else
        {
            chessdropdown.value = 0;
        }
    }

    public void ShowSlotDropdown(GameObject slotdropdown)
    {
        if (!slotdropdown.activeSelf)
        {
            slotdropdown.SetActive(true);
            slotdropdown.transform.GetChild(0).gameObject.SetActive(true);
            slotdropdown.GetComponent<RectTransform>().position = new Vector3(
                Input.mousePosition.x,
                Input.mousePosition.y - 20f
            );
        }
        else
        {
            slotdropdown.GetComponent<RectTransform>().position = Input.mousePosition;
        }
    }

    public void RefillDropdownEvents(GameObject hitslot, GameObject slotdropdown)
    {
        Slot thisslot = hitslot.GetComponent<SlotComponent>().thisSlot;

        TMP_Dropdown landscapedropdown = slotdropdown
            .transform.GetChild(0)
            .GetComponent<TMP_Dropdown>();
        landscapedropdown.onValueChanged.AddListener(
            (int index) =>
            {
                landscapedropdown.onValueChanged.RemoveAllListeners();
                if (index <= Enum.GetValues(typeof(LandscapeType)).Length)
                {
                    if (
                        (thisslot.Landscape == null || thisslot.Landscape.UnitGameObject == null)
                        && index > 0
                    )
                    {
                        GameObject CreatedObject = new();
                        thisslot.Landscape.UnitGameObject = CreatedObject;
                        Landscape targetlandscape = CreatedObject
                            .GetComponent<LandscapeComponent>()
                            .thisLandscape;
                        targetlandscape = thisslot.Landscape;
                        targetlandscape.PutToSlotPosition(ref hitslot);
                        CreatedObject.transform.SetParent(hitslot.transform);
                        CreatedObject.transform.localPosition = new Vector3(0, 0, 0.2f);
                        //thisslot.Landscape.LoadSprite();
                    }
                }
            }
        );

        TMP_Dropdown constructiondropdown = slotdropdown
            .transform.GetChild(1)
            .GetComponent<TMP_Dropdown>();
        constructiondropdown.onValueChanged.AddListener(
            (int index) =>
            {
                constructiondropdown.onValueChanged.RemoveAllListeners();
                if (index <= Enum.GetValues(typeof(ConstructionType)).Length)
                {
                    if (
                        (
                            thisslot.Construction == null
                            || thisslot.Construction.UnitGameObject == null
                        )
                        && index > 0
                    )
                    {
                        GameObject CreatedObject = new();
                        thisslot.Construction.UnitGameObject = CreatedObject;
                        Construction targetconstruction = CreatedObject
                            .GetComponent<ConstructionComponent>()
                            .thisConstruction;
                        targetconstruction = thisslot.Construction;
                        targetconstruction.PutToSlotPosition(ref hitslot);
                        CreatedObject.transform.SetParent(hitslot.transform);
                        CreatedObject.transform.localScale *= 0.85f;
                        CreatedObject.transform.localPosition = new Vector3(0, 0, 0.4f);
                        //thisslot.Construction.LoadConstructionSprite();
                    }
                }
            }
        );
    }
}
