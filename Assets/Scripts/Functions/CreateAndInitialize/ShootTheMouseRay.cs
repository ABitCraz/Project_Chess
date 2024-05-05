using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootTheMouseRay : MonoBehaviour
{
    Ray MouseRay;
    GameObject hitobject;
    public GameObject slotdropdown;
    public GameObject statusshow;
    public delegate void EndActions();
    public static event EndActions endClickActions;
    public static event EndActions endHoverActions;
    GameObject previoushover;
    GameObject[] paramlist;
    public bool Isdropdownoff;
    bool israyshootnothing = true;

    private void Update()
    {
        MouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        israyshootnothing = GetMouseRayCastHit(ref MouseRay);
        if (israyshootnothing)
        {
            EndTheHoverActions();
        }
        if (Input.GetMouseButtonDown(1))
        {
            EndTheClickActions();
        }
    }

    private bool GetMouseRayCastHit(ref Ray mouseray)
    {
        if (Physics.Raycast(mouseray, out RaycastHit hit) && (hit.collider.gameObject != null))
        {
            GameObject hitobject = hit.collider.gameObject;
            if (previoushover != null && hitobject != previoushover)
            {
                EndTheHoverActions();
            }
            previoushover = hitobject;
            if (Input.GetMouseButtonDown(0))
            {
                if (!Isdropdownoff)
                {
                    paramlist = new GameObject[] { hitobject, slotdropdown };
                    Isdropdownoff = true;
                    endClickActions += () =>
                    {
                        Isdropdownoff = false;
                    };
                }
            }
            else
            {
                paramlist = new GameObject[] { hitobject, statusshow };
            }
            UIHit(ref paramlist);
            return false;
        }
        return true;
    }

    public void UIHit(ref GameObject[] hitobject)
    {
        RaycastUIs rui = new();
        switch (hitobject[0].tag)
        {
            case "Slot":
                rui.SlotOnHover(hitobject[0], hitobject[1]);
                rui.SlotOnClick(hitobject[0], hitobject[1]);
                break;
        }
    }

    private void EndTheClickActions()
    {
        endClickActions?.Invoke();
        endClickActions = null;
    }

    private void EndTheHoverActions()
    {
        endHoverActions?.Invoke();
        endHoverActions = null;
    }
}
