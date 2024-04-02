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
        if (Physics.Raycast(mouseray, out RaycastHit hit)&&hit.collider.gameObject.CompareTag(Slot.EmptySlotTagName))
        {
            if(hit.collider.gameObject!=hoverslot)
            {
                if(hoverslot!=null&&hoverslot.CompareTag(Slot.EmptySlotTagName))
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
        if(Physics.Raycast(mouseray, out RaycastHit hit)&&hit.collider.gameObject.CompareTag(Slot.EmptySlotTagName)&&Input.GetMouseButton(0))
        {
            if(hit.collider.gameObject!=clickslot)
            {
                if(clickslot!=null&&clickslot.CompareTag(Slot.EmptySlotTagName))
                {
                    clickslot.transform.GetChild(1).gameObject.SetActive(false);
                }
                clickslot = hit.collider.gameObject;
                hit.collider.gameObject.transform.GetChild(1).gameObject.SetActive(true);
            }
        }
    }
}
