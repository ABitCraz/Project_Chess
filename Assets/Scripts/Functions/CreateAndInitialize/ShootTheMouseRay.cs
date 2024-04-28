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

    private void Update()
    {
        MouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        bool hitted = GetMouseRayCastHit(ref MouseRay);
        if (Input.GetMouseButtonDown(1) && !hitted)
        {
            EndTheClickActions();
        }
        if (!hitted)
        {
            EndTheHoverActions();
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
            RaycastUIs.UIHit(ref paramlist);
            return true;
        }
        return false;
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
