using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootTheMouseRay : MonoBehaviour
{
    Ray MouseRay;
    GameObject hitobject;
    public GameObject slotdropdown;
    public delegate void EndClickActions();
    public static EndClickActions endClickActions;
    GameObject previousgameobject;

    private void Update()
    {
        MouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        bool hitted = GetMouseRayCastHit(ref MouseRay);
        if (Input.GetMouseButtonDown(1) && !hitted)
        {
            EndTheActions();
        }
    }

    private bool GetMouseRayCastHit(ref Ray mouseray)
    {
        if (Physics.Raycast(mouseray, out RaycastHit hit) && (hit.collider.gameObject != null))
        {
            hitobject = hit.collider.gameObject;
            if (previousgameobject != null && previousgameobject != hitobject)
            {
                EndTheActions();
            }
            GameObject[] paramlist = new GameObject[] { hitobject, slotdropdown };
            RaycastUIs.UIHit(ref paramlist);
            return true;
        }
        return false;
    }

    private void EndTheActions()
    {
        if (endClickActions != null)
        {
            endClickActions();
        }
        endClickActions = null;
    }
}
