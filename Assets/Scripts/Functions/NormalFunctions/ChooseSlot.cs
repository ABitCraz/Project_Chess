using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ChooseSlot : MonoBehaviour
{
    GameObject hoverslot;
    GameObject clickslot;
    bool rolleropened;
    public bool DrawingLandscape;
    public bool DrawingTrooper;
    private void Update()
    {
        Ray mouseray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (rolleropened)
        {
            ChooseLandscapeForSlot(ref mouseray);
        }
        else
        {
            GetSlotWhenHover(ref mouseray);
            GetSlotWhenClick(ref mouseray);
        }
    }

    private void GetSlotWhenHover(ref Ray mouseray)
    {
        if (Physics.Raycast(mouseray, out RaycastHit hit) && hit.collider.gameObject.CompareTag(Slot.EmptySlotTagName))
        {
            if (hit.collider.gameObject != hoverslot)
            {
                if (hoverslot != null && hoverslot.CompareTag(Slot.EmptySlotTagName))
                {
                    hoverslot.transform.GetChild(0).gameObject.SetActive(false);
                }
                hoverslot = hit.collider.gameObject;
                hit.collider.gameObject.transform.GetChild(0).gameObject.SetActive(true);
            }
        }
    }

    private void GetSlotWhenClick(ref Ray mouseray)
    {
        if (Physics.Raycast(mouseray, out RaycastHit hit) && hit.collider.gameObject.CompareTag(Slot.EmptySlotTagName) && Input.GetMouseButtonDown(0))
        {
            GameObject hitted = hit.collider.gameObject;
            if (hitted != clickslot)
            {
                if (clickslot != null && clickslot.CompareTag(Slot.EmptySlotTagName))
                {
                    clickslot.transform.GetChild(1).gameObject.SetActive(false);
                    clickslot.transform.GetChild(2).gameObject.SetActive(false);
                }
                clickslot = hitted;
                hitted.transform.GetChild(1).gameObject.SetActive(true);
                ShowLandscapeChoicesIfSlotEmpty(ref clickslot);
                rolleropened = true;
            }
            else
            {
                if (clickslot.GetComponent<EmptySlotComponent>().thisSlot.Landscape == null)
                {
                    hitted.transform.GetChild(2).gameObject.SetActive(true);
                    rolleropened = true;
                }
            }
        }
    }

    private void ShowLandscapeChoicesIfSlotEmpty(ref GameObject chosenslotobject)
    {
        if (chosenslotobject.GetComponent<EmptySlotComponent>().SlotContainer == null)
        {
            GameObject choicecontainer = chosenslotobject.transform.GetChild(2).gameObject;
            choicecontainer.tag = Slot.LandscapeContainerTagName;
            choicecontainer.AddComponent<BoxCollider>();
            choicecontainer.SetActive(true);
            int lstypecount = Enum.GetValues(typeof(LandscapeType)).Length;

            GameObject positioncontainer = new("Position_Container");
            positioncontainer.transform.parent = choicecontainer.transform;
            positioncontainer.transform.localPosition = new Vector3(0, 0, -0.1f);
            GameObject positionleader = new("Position_Leader");
            positionleader.transform.parent = positioncontainer.transform;
            positionleader.transform.localPosition = new Vector2(0, 1);

            GameObject landscapeobject = Resources.Load("Prefabs/Landscapes/Landscape") as GameObject;
            for (int i = 0; i < lstypecount; i++)
            {
                GameObject thislandscape = Instantiate(landscapeobject);
                thislandscape.AddComponent<LandscapeChooseComponent>();
                thislandscape.transform.parent = choicecontainer.transform;
                thislandscape.transform.position = positionleader.transform.position;
                thislandscape.transform.tag = Slot.LandscapeTypeTagName;
                GiveTypeForChoices(ref thislandscape, i);
                thislandscape.AddComponent<BoxCollider>();
                positioncontainer.transform.Rotate(Vector3.forward, 360f / lstypecount);
            }
            chosenslotobject.GetComponent<EmptySlotComponent>().SlotContainer = choicecontainer;
            Destroy(positioncontainer);
        }
        else
        {
            chosenslotobject.GetComponent<EmptySlotComponent>().SlotContainer.SetActive(true);
        }
    }

    private void GiveTypeForChoices(ref GameObject chosenslotobject, int index)
    {
        switch (index)
        {
            case 0:
                chosenslotobject.GetComponent<LandscapeChooseComponent>().thisLandscape = new Plain();
                break;
            case 1:
                chosenslotobject.GetComponent<LandscapeChooseComponent>().thisLandscape = new Avenue();
                break;
            case 2:
                chosenslotobject.GetComponent<LandscapeChooseComponent>().thisLandscape = new Highland();
                break;
            case 3:
                chosenslotobject.GetComponent<LandscapeChooseComponent>().thisLandscape = new Forest();
                break;
            case 4:
                chosenslotobject.GetComponent<LandscapeChooseComponent>().thisLandscape = new River();
                break;
        }
    }

    private void ChooseLandscapeForSlot(ref Ray mouseray)
    {
        if (Physics.Raycast(mouseray, out RaycastHit hit) && Input.GetMouseButtonDown(0))
        {
            GameObject hitted = hit.collider.gameObject;
            if (hitted.CompareTag(Slot.LandscapeTypeTagName))
            {
                Landscape chosenlandscape = hitted.GetComponent<LandscapeChooseComponent>().thisLandscape;
                clickslot.GetComponent<EmptySlotComponent>().thisSlot.Landscape = chosenlandscape;
                hitted.transform.parent.gameObject.SetActive(false);
                rolleropened = false;
            }
            else if(hitted.CompareTag(Slot.LandscapeContainerTagName))
            {
                hitted.SetActive(false);
                rolleropened = false;
            }
        }
    }
}
