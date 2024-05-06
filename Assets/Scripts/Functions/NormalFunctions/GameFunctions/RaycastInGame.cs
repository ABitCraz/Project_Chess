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
    GameObject previousslot;

    private void Awake()
    {
        loadedsave ??= StartCoroutine(loadsave());
    }

    private void Update()
    {
        mouseray = Camera.main.ScreenPointToRay(Input.mousePosition);
        bool shotsth = ShootingRaycast(ref mouseray);
    }

    private bool ShootingRaycast(ref Ray mouseray)
    {
        RaycastHit[] hits = Physics.RaycastAll(mouseray);
        for (int i = 0; i < hits.Length; i++)
        {
            GameObject hitobject = hits[i].collider.gameObject;
            if (hitobject.CompareTag("Slot"))
            {
                previousslot = hitobject;
                Slot currentslot = hitobject.GetComponent<SlotComponent>().thisSlot;
                ControlCurrentSlot(ref currentslot);
                return true;
            }
        }
        return false;
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
