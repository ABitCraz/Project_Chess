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
    KeepSpriteErect kse = new();
    public bool IsLoadDone = false;

    public void Start()
    {
        LoadMap();
        ChangeCamera();
    }

    public void Update()
    {
        if (save.SlotMap != null)
        {
            kse.KeepErect(ref save.SlotMap, Camera.main.gameObject);
        }
    }

    private void ChangeCamera()
    {
        GameObject c = Camera.main.gameObject;
        c.transform.Rotate(Vector3.left, 45f, Space.World);
        c.transform.Rotate(Vector3.forward, 45f, Space.World);
        c.transform.position = new Vector3(8, -8f, c.transform.position.z);
    }

    private void LoadMap()
    {
        try
        {
            save = snl.LoadMapJSONFile(MapFileName);
            List<SerializableSlot> s_slots = save.SaveSlots;
            if ((s_slots.Count > 0) && (s_slots != null))
            {
                GameObject createdmap = new("SlotMap");
                createdmap.AddComponent<MapControl>();
                createdmap.AddComponent<SlotMapComponent>().thisSlotMap = save.SlotMap;
                save.SlotMap.SlotMapGameObject = createdmap;
                List<Slot> wholeslot = new();
                for (int i = 0; i < s_slots.Count; i++)
                {
                    Slot loadedslot = s_slots[i].SwitchToNormalSlot();
                    GameObject loadedslotgameobject = Instantiate(
                        Resources.Load(ResourcePaths.Resources[Prefab.Slot]) as GameObject
                    );
                    loadedslotgameobject.GetComponent<SlotComponent>().thisSlot = loadedslot;
                    loadedslotgameobject.GetComponent<SlotComponent>().thisSlot.SlotGameObject = loadedslotgameobject;
                    loadedslotgameobject.transform.position = loadedslot.FactPosition;
                    loadedslotgameobject.transform.SetParent(createdmap.transform);
                    SlotLoader.LoadGameObjectFromType(ref loadedslotgameobject);
                    save.SlotMap.FullSlotDictionary.Add(s_slots[i].MapPosition, loadedslot);
                    wholeslot.Add(loadedslot);
                }
                save.SlotMap.FullSlotMap = wholeslot.ToArray();
                save.SlotMap.SlotMapGameObject = createdmap;
            }
        }
        catch (Exception except)
        {
            print("文件不可读:\n" + except);
        }
        IsLoadDone = true;
    }
}
