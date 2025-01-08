using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static ObjectActions;

public class ShootMouseRay
{
    public GameObjectAction MouseRayComponentUpdateAction;
    Ray MouseRay;
    readonly RaycastHit[] all_raycast_hit = new RaycastHit[128];
    public bool PauseInUI = false;
    public GameObject DropdownGameObject;

    public ShootMouseRay()
    {
        MouseRayComponentUpdateAction += ShootMouseRayActionsOnUpdate;
    }

    private void ShootMouseRayActionsOnUpdate()
    {
        MouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        GetMouseRayCastHit(ref MouseRay);
        if (Input.GetMouseButtonDown((int)MouseButton.Right))
        {
            DoDisableClickActions();
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
            if (all_hit_game_objects.Length > 0 && !PauseInUI)
            {
                SwitchHitToDifferentKinds(ref all_hit_game_objects);
            }
        }
        else
        {
            DoDisableHoverActions();
        }
    }

    private void SwitchHitToDifferentKinds(ref GameObject[] hit_game_objects)
    {
        int hit_game_object_count = hit_game_objects.Length;
        for (int i = 0; i < hit_game_object_count; i++)
        {
            if (hit_game_objects[i] != null)
            {
                switch (hit_game_objects[i].tag)
                {
                    case "Slot":
                        DropdownGameObject
                            .GetComponent<TheSlotDropdownComponent>()
                            .slot_dropdown.GetTargetSlotGameObjectFromRaycasts?.Invoke(
                                ref hit_game_objects[i]
                            );
                        TheSlotStatusComponent.ShowTargetSlotStatus?.Invoke(
                            ref hit_game_objects[i]
                        );
                        SlotSelectionComponent.HideHoverGameObject?.Invoke(ref hit_game_objects[i]);
                        SlotSelectionComponent.ShowHoverGameObject?.Invoke(ref hit_game_objects[i]);
                        SlotSelectionComponent.ToggleSelectionGameObject?.Invoke(
                            ref hit_game_objects[i]
                        );
                        break;
                }
            }
        }
    }

    private void DoDisableClickActions()
    {
        DropdownGameObject
            .GetComponent<TheSlotDropdownComponent>()
            .slot_dropdown.DisableDropdownGameObject?.Invoke();
        TheSlotStatusComponent.DisableStatusGameObject?.Invoke();
        SlotSelectionComponent.DisableHoverAndSelectionGameObject?.Invoke();
        PauseInUI = false;
    }

    private void DoDisableHoverActions() { }
}
