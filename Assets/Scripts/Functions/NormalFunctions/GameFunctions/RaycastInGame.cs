using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastInGame : MonoBehaviour
{
    public GameObject StatusSet;
    Ray mouseray;
    SlotStatusShow sss = new();
    SlotCalculator sc = new();
    SavingDatum save;

    private void Awake()
    {
        mouseray = Camera.main.ScreenPointToRay(Input.mousePosition);
    }

    private void Start()
    {
        save = this.GetComponent<LoadTargetMap>().save;
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
        if(slot.Chess!=null)
        {
            sss.ShowAttackRange(slot,save.SlotMap);
            sss.ShowVisionRange(slot,save.SlotMap);
        }
    }
}
