using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepSpriteErect
{
    public static void KeepErect(ref SlotMap current_map, GameObject camera_object)
    {
        List<GameObject> whole_sprite_gos = new();
        Slot[] cm = current_map.FullSlotMap;
        for (int i = 0; i < cm.Length; i++)
        {
            if (cm[i].Landscape != null)
            {
                whole_sprite_gos.Add(cm[i].Landscape.UnitGameObject);
            }
            if (cm[i].Construction != null)
            {
                whole_sprite_gos.Add(cm[i].Construction.UnitGameObject);
            }
            if (cm[i].Chess != null)
            {
                whole_sprite_gos.Add(cm[i].Chess.UnitGameObject);
            }
        }

        for (int i = 0; i < whole_sprite_gos.Count; i++)
        {
            whole_sprite_gos[i].transform.rotation = camera_object.transform.rotation;
        }
    }
}
