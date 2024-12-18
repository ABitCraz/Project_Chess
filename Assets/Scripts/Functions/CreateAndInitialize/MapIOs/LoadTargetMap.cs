using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadTargetMap : MonoBehaviour
{
    public SlotMap LoadedMap;
    public string MapFileName;
    public SavingDatum save;
    public bool IsLoadDone = false;

    public void Start()
    {
        LoadMap();
        ChangeCamera();
    }

    private void ChangeCamera()
    {
        GameObject c = Camera.main.gameObject;
        c.transform.Rotate(Vector3.left, 45f, Space.World);
        c.transform.Rotate(Vector3.forward, 45f, Space.World);
        c.transform.position = new Vector3(8, -8f, c.transform.position.z);
    }

    private async void LoadMap()
    {
        try
        {
            save = await new SaveAndLoad(MapFileName).LoadMapJSONFileAsync();
            List<SerializableSlot> s_slots = save.SaveSlots;
            if ((s_slots.Count > 0) && (s_slots != null))
            {
                GameObject created_map = new("SlotMap");
                created_map.AddComponent<MapControl>();
                created_map.AddComponent<SlotMapComponent>().thisSlotMap = save.SlotMap;
                save.SlotMap.SlotMapGameObject = created_map;
                List<Slot> whole_slot = new();
                for (int i = 0; i < s_slots.Count; i++)
                {
                    Slot loaded_slot = s_slots[i].SerializableSlotSwitchToNormalSlot();
                    GameObject loaded_slot_gameobject = Instantiate(
                        Resources.Load(ResourcePaths.Resources[Prefab.Slot]) as GameObject
                    );
                    loaded_slot_gameobject.GetComponent<SlotComponent>().thisSlot = loaded_slot;
                    loaded_slot_gameobject.GetComponent<SlotComponent>().thisSlot.SlotGameObject =
                        loaded_slot_gameobject;
                    loaded_slot_gameobject.transform.position = loaded_slot.FactPosition;
                    loaded_slot_gameobject.transform.SetParent(created_map.transform);
                    SlotLoader.LoadGameObject(ref loaded_slot_gameobject);
                    save.SlotMap.FullSlotDictionary.Add(s_slots[i].MapPosition, loaded_slot);
                    whole_slot.Add(loaded_slot);
                }
                save.SlotMap.FullSlotMap = whole_slot.ToArray();
                save.SlotMap.SlotMapGameObject = created_map;
            }
        }
        catch (Exception except)
        {
            print("文件不可读:\n" + except);
        }
        IsLoadDone = true;
    }
}
