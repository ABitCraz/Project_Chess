using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static ObjectActions;
using static UnityEngine.UI.Button;

public class MapFileLoader
{
    public GameObjectAction RestockNewMapFileLoader;
    public SavingDatum save = new();
    ButtonClickedEvent SaveButtonEvent;
    ButtonClickedEvent LoadButtonEvent;
    ButtonClickedEvent CoverButtonEvent;
    ButtonClickedEvent ClearButtonEvent;
    GameObject FileDirectoryObject;
    GameObject ActionDoneObject;

    public void InitializeMapFileLoader(ref GameObject map_file_loader)
    {
        SaveButtonEvent = map_file_loader.transform.GetChild(0).GetComponent<Button>().onClick;
        LoadButtonEvent = map_file_loader.transform.GetChild(1).GetComponent<Button>().onClick;
        CoverButtonEvent = map_file_loader.transform.GetChild(2).GetComponent<Button>().onClick;
        ClearButtonEvent = map_file_loader.transform.GetChild(3).GetComponent<Button>().onClick;
        FileDirectoryObject = map_file_loader.transform.GetChild(4).gameObject;
        ActionDoneObject = map_file_loader.transform.GetChild(5).gameObject;
        InitializeMapFileLoaderButtons();
    }

    private void InitializeMapFileLoaderButtons()
    {
        SaveButtonEvent.AddListener(async () =>
        {
            int succeed = await new SaveAndLoad(
                FileDirectoryObject.GetComponent<TMP_InputField>().text
            ).SaveMapJSONFileAsync(save, false);
            if (succeed == 1)
            {
                ActionDoneObject.GetComponent<TMP_Text>().text = "保存成功";
            }
            else
            {
                ActionDoneObject.GetComponent<TMP_Text>().text = "保存失败";
            }
        });
        LoadButtonEvent.AddListener(async () =>
        {
            await LoadFileToSlot();
        });
        CoverButtonEvent.AddListener(async () =>
        {
            await new SaveAndLoad(
                FileDirectoryObject.GetComponent<TMP_InputField>().text
            ).SaveMapJSONFileAsync(save, true);
        });
        ClearButtonEvent.AddListener(() =>
        {
            GameObject created_map = save.SlotMap.SlotMapGameObject;
            MonoBehaviour.Destroy(created_map);
            created_map = null;
            save.SlotMap.FullSlotMap = null;
            FileDirectoryObject.GetComponent<TMP_InputField>().readOnly = false;
        });
    }

    private async Awaitable LoadFileToSlot()
    {
        TMP_InputField filename_TMP_input = FileDirectoryObject.GetComponent<TMP_InputField>();
        TMP_Text result_TMP_text = ActionDoneObject.GetComponent<TMP_Text>();
        string file_name = filename_TMP_input.text;
        if (save.SlotMap.FullSlotMap != null)
        {
            result_TMP_text.text = "地图并不是空的，请先清理地图";
            return;
        }
        save = await new SaveAndLoad(file_name).LoadMapJSONFileAsync();
        if (save == null)
        {
            result_TMP_text.text = $"文件{file_name}读取失败";
            if (filename_TMP_input.readOnly)
            {
                filename_TMP_input.readOnly = false;
            }
            filename_TMP_input.text = "";
            return;
        }
        else
        {
            result_TMP_text.text = $"文件{file_name}读取成功";
            filename_TMP_input.readOnly = true;
        }

        List<SerializableSlot> serialized_slots = save.SaveSlots;
        if (serialized_slots.Count <= 0)
        {
            result_TMP_text.text = "地图是空的";
            return;
        }
        if (serialized_slots == null)
        {
            result_TMP_text.text = "地图不可读";
            return;
        }

        GameObject created_map = new("SlotMap");
        created_map.AddComponent<SlotMapComponent>().thisSlotMap = save.SlotMap;
        save.SlotMap.SlotMapGameObject = created_map;
        save.SlotMap.FullSlotMap = InitializeMapSlots(serialized_slots.ToArray(), ref created_map);
    }

    private Slot[] InitializeMapSlots(
        SerializableSlot[] serialized_slots_array,
        ref GameObject created_map
    )
    {
        List<Slot> slots = new();
        int serialized_slot_count = serialized_slots_array.Length;
        for (int i = 0; i < serialized_slot_count; i++)
        {
            GameObject loaded_slot_gameobject = MonoBehaviour.Instantiate(
                Resources.Load(ResourcePaths.Resources[Prefab.Slot]) as GameObject
            );
            Slot loaded_slot = new(serialized_slots_array[i], ref loaded_slot_gameobject);
            loaded_slot_gameobject.GetComponent<SlotComponent>().thisSlot = loaded_slot;
            loaded_slot_gameobject.GetComponent<SlotComponent>().thisSlot.SlotGameObject =
                loaded_slot_gameobject;
            loaded_slot_gameobject.transform.position = loaded_slot.FactPosition;
            loaded_slot_gameobject.transform.SetParent(created_map.transform);
            SlotLoader.LoadGameObject(ref loaded_slot_gameobject);
            save.SlotMap.FullSlotDictionary.Add(serialized_slots_array[i].MapPosition, loaded_slot);
            slots.Add(loaded_slot);
        }
        return slots.ToArray();
    }
}
