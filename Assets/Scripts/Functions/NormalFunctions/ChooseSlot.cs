using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ChooseSlot : MonoBehaviour
{
    GameObject hoverslot;
    GameObject clickslot;
    private void Update()
    {
        Ray mouseray = Camera.main.ScreenPointToRay(Input.mousePosition);
        GetSlotWhenHover(ref mouseray);
        GetSlotWhenClick(ref mouseray);
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
        if (Physics.Raycast(mouseray, out RaycastHit hit) && hit.collider.gameObject.CompareTag(Slot.EmptySlotTagName) && Input.GetMouseButton(0))
        {
            if (hit.collider.gameObject != clickslot)
            {
                if (clickslot != null && clickslot.CompareTag(Slot.EmptySlotTagName))
                {
                    clickslot.transform.GetChild(1).gameObject.SetActive(false);
                }
                clickslot = hit.collider.gameObject;
                hit.collider.gameObject.transform.GetChild(1).gameObject.SetActive(true);
                ShowLandscapeChoicesIfSlotEmpty(ref clickslot);
            }
        }
    }

    private void ShowLandscapeChoicesIfSlotEmpty(ref GameObject chosenslotobject)
    {
        if(chosenslotobject.GetComponent<EmptySlotComponent>().SlotContainer == null)
        {
            Transform choicecontainer = chosenslotobject.transform.GetChild(2);
            choicecontainer.gameObject.SetActive(true);
            int lstypecount = Enum.GetValues(typeof(LandscapeType)).Length;

            GameObject positioncontainer = new("Position_Container");
            positioncontainer.transform.parent = choicecontainer;
            positioncontainer.transform.localPosition = Vector3.zero;
            GameObject positionleader = new("Position_Leader");
            positionleader.transform.parent = positioncontainer.transform;
            positionleader.transform.localPosition = new Vector2(0, 1);
            
            GameObject landscapeobject = Resources.Load("Prefabs/Landscapes/Landscape") as GameObject;
            for (int i = 0; i < lstypecount; i++)
            {
                GameObject thislandscape = Instantiate(landscapeobject);
                thislandscape.transform.parent = choicecontainer;
                thislandscape.transform.position = positionleader.transform.position;
                positioncontainer.transform.Rotate(Vector3.forward, 360f/lstypecount);
            }
            chosenslotobject.GetComponent<EmptySlotComponent>().SlotContainer = positioncontainer;
            Destroy(positioncontainer);
        }
        else
        {
            chosenslotobject.GetComponent<EmptySlotComponent>().SlotContainer.SetActive(true);
        }
    }

    private void ChooseLandscapeForSlot(ref Ray mouseray)
    {
        if (Physics.Raycast(mouseray, out RaycastHit hit) && hit.collider.gameObject.CompareTag(Slot.LandscapeContainerTagName) && Input.GetMouseButton(0))
        {

        }
    }
}
