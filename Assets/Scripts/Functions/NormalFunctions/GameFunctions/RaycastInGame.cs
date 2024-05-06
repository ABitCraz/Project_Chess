using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RaycastInGame : MonoBehaviour
{
    public GameObject StatusSet;
    Ray mouseray;
    SlotStatusShow sss = new();
    SlotCalculator sc = new();
    SavingDatum save;
    Coroutine loadedsave = null;

    private void Awake()
    {
        loadedsave ??= StartCoroutine(loadsave());
    }

    private void Update()
    {
        mouseray = Camera.main.ScreenPointToRay(Input.mousePosition);
        ShootingRaycast(ref mouseray);
    }

    private void ShootingRaycast(ref Ray mouseray)
    {
        RaycastHit[] hits = Physics.RaycastAll(mouseray);
        for (int i = 0; i < hits.Length; i++)
        {
            GameObject hitobject = hits[i].collider.gameObject;
            if (hitobject.CompareTag("Slot"))
            {
                Slot currentslot = hitobject.GetComponent<SlotComponent>().thisSlot;
                ControlCurrentSlot(ref currentslot);
                break;
            }
        }
    }

    private void ControlCurrentSlot(ref Slot slot)
    {
        sss.ShowStatus(ref slot, ref StatusSet);
        if (slot.Chess != null)
        {
            sss.ShowAttackRange(ref slot, ref save.SlotMap);
            sss.ShowVisionRange(ref slot, ref save.SlotMap);
        }
    }

    IEnumerator loadsave()
    {
        while (!this.GetComponent<LoadTargetMap>().IsLoadDone)
        {
            yield return new WaitForEndOfFrame();
        }
        save = this.GetComponent<LoadTargetMap>().save;
        StopCoroutine(loadedsave);
        loadedsave = null;
        print("Load is Done");
    }
}
