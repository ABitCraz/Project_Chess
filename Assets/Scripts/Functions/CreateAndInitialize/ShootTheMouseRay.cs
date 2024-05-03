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
    GameObject previousgameobject;
    GameObject[] paramlist;
    public bool Isdropdownoff = true;
    bool israyshootnothing = true;

    private void Update()
    {
        MouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        israyshootnothing = GetMouseRayCastHit(ref MouseRay);
        if (israyshootnothing)
        {
            EndTheHoverActions();
        }
        if (Input.GetMouseButtonDown(1) && israyshootnothing)
        {
            EndTheClickActions();
        }
    }

    private bool GetMouseRayCastHit(ref Ray mouseray)
    {
        if (Physics.Raycast(mouseray, out RaycastHit hit) && (hit.collider.gameObject != null))
        {
            hitobject = hit.collider.gameObject;
            if (previousgameobject != null && previousgameobject != hitobject)
            {
                EndTheClickActions();
            }
            if (Input.GetMouseButtonDown(0))
            {
                paramlist = new GameObject[] { hitobject, slotdropdown };
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
