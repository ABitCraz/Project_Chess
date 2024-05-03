using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadTargetMap : MonoBehaviour
{
    SaveAndLoad snl = new();
    public SlotMap LoadedMap;
    public string MapFileName;
    public SavingDatum save;

    public void Awake()
    {
        try
        {
            save = snl.LoadMapJSONFile(MapFileName);
            List<SerializableSlot> s_slots = save.SaveSlots;
            if ((s_slots.Count > 0) && (s_slots != null))
            {
                GameObject createdmap = new("SlotMap");
                createdmap.AddComponent<SlotMapComponent>().thisSlotMap = save.SlotMap;
                save.SlotMap.SlotMapGameObject = createdmap;
                for (int i = 0; i < s_slots.Count; i++)
                {
                    Slot loadedslot = s_slots[i].SwitchToNormalSlot();
                    GameObject loadedslotgameobject = Instantiate(
                        Resources.Load(ResourcePaths.Resources[Prefab.Slot]) as GameObject
                    );
                    loadedslotgameobject.GetComponent<SlotComponent>().thisSlot = loadedslot;
                    loadedslotgameobject.transform.position = loadedslot.FactPosition;
                    loadedslotgameobject.transform.SetParent(createdmap.transform);
                    SlotLoader.LoadGameObjectFromType(ref loadedslotgameobject);
                    save.SlotMap.FullSlotDictionary.Add(s_slots[i].MapPosition, loadedslot);
                }
                save.SlotMap.SlotMapGameObject = createdmap;
            }
        }
        catch (Exception except)
        {
            print("文件不可读:\n" + except);
        }
    }
}
