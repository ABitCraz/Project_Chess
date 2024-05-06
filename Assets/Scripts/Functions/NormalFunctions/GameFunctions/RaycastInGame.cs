using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastInGame : MonoBehaviour
{
    public GameObject StatusSet;
    Ray mouseray;
    SlotStatusShow sss = new();

    private void Awake()
    {
        mouseray = Camera.main.ScreenPointToRay(Input.mousePosition);
    }

    private void Update()
    {
        ShootingRaycast();
    }

    private void ShootingRaycast()
    {
        if (Physics.Raycast(mouseray, out RaycastHit hit) && hit.collider.gameObject)
        {
            GameObject hitobject = hit.collider.gameObject;
            if (hitobject.CompareTag("Slot"))
            {
                Slot currentslot = hitobject.GetComponent<SlotComponent>().thisSlot;
                ControlCurrentSlot(ref currentslot);
            }
        }
    }

    private void ControlCurrentSlot(ref Slot slot)
    {
        sss.ShowStatus(slot, StatusSet);
    }
}
