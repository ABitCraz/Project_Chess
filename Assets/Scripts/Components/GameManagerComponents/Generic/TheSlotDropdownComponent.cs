using System;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using static ObjectActions;
using static TMPro.TMP_Dropdown;

public class TheSlotDropdownComponent : MonoBehaviour
{
    public TheSlotDropdown slot_dropdown;
    public GameObject shoot_ray_game_object;

    private void Awake()
    {
        GameObject current_dropdown_game_object = this.gameObject;
        shoot_ray_game_object
            .GetComponent<ShootMouseRayComponent>()
            .shoot_mouse_ray.DropdownGameObject = current_dropdown_game_object;
        slot_dropdown = new(ref current_dropdown_game_object, ref shoot_ray_game_object);
        slot_dropdown.SlotDropdownComponentAwakeAction?.Invoke(ref current_dropdown_game_object);
    }
}
