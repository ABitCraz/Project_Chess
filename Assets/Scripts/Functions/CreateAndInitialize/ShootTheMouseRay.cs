using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ShootTheMouseRay : MonoBehaviour
{
    Vector3 mouse_position;
    Ray mouse_ray;
    readonly RaycastHit[] all_raycast_hit = new RaycastHit[128];

    private void Update()
    {
        mouse_ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        mouse_position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        GetMouseRayCastHit(ref mouse_ray);
        if (Input.GetMouseButtonDown((int)MouseButton.Right))
        {
            DoDisableActions();
        }
    }

    private void GetMouseRayCastHit(ref Ray mouse_ray)
    {
        int raycast_hit_count = Physics.RaycastNonAlloc(mouse_ray, all_raycast_hit);
        GameObject[] all_hit_game_objects = new GameObject[128];
        if (raycast_hit_count > 0)
        {
            for (int i = 0; i < raycast_hit_count; i++)
            {
                if (all_hit_game_objects != null)
                    all_hit_game_objects[i] = all_raycast_hit[i].collider.gameObject;
            }
            if (all_hit_game_objects.Length > 0)
            {
                SwitchHitToDifferentKinds(ref all_hit_game_objects);
            }
        }
    }

    private void SwitchHitToDifferentKinds(ref GameObject[] hit_game_objects)
    {
        int hit_game_object_count = hit_game_objects.Length;
        for (int i = 0; i < hit_game_object_count; i++)
            if (hit_game_objects[i] != null)
            {
                switch (hit_game_objects[i].tag)
                {
                    case "Slot":
                        TheSlotDropdown.GetTargetSlotGameObjectFromRaycasts.Invoke(
                            ref hit_game_objects[i]
                        );
                        TheSlotStatus.ShowTargetSlotStatus.Invoke(ref hit_game_objects[i]);
                        break;
                }
            }
    }

    private void DoDisableActions()
    {
        TheSlotDropdown.DisableDropdownGameObject.Invoke();
        TheSlotStatus.DisableStatusGameObject.Invoke();
    }
}
