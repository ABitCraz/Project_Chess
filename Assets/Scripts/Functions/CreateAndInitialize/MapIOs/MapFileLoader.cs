using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MapFileLoader : MonoBehaviour
{
    public GameObject FileDirectoryObject;
    public GameObject ActionDoneObject;
    public SavingDatum save = new();
    public GameObject SaveButton;
    public GameObject CoverButton;
    public GameObject LoadButton;
    public GameObject ClearMapButton;
    public string MapFileName;

    private void Awake()
    {
        SaveButton
            .GetComponent<Button>()
            .onClick.AddListener(async () =>
            {
                await new SaveAndLoad(MapFileName).SaveMapJSONFileAsync(save, false);
            });
        CoverButton
            .GetComponent<Button>()
            .onClick.AddListener(async () =>
            {
                await new SaveAndLoad(MapFileName).SaveMapJSONFileAsync(save, true);
            });
        LoadButton
            .GetComponent<Button>()
            .onClick.AddListener(async () =>
            {
                await LoadFileToSlot();
            });
        ClearMapButton
            .GetComponent<Button>()
            .onClick.AddListener(() =>
            {
                GameObject created_map = save.SlotMap.SlotMapGameObject;
                Destroy(created_map);
                created_map = null;
                FileDirectoryObject.GetComponent<TMP_InputField>().readOnly = false;
            });
    }

    private async Task LoadFileToSlot()
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
        created_map.AddComponent<MapControl>();
        created_map.AddComponent<SlotMapComponent>().thisSlotMap = save.SlotMap;
        save.SlotMap.SlotMapGameObject = created_map;
        save.SlotMap.FullSlotMap = InitializeMapSlots(serialized_slots.ToArray(), ref created_map);
        save.SlotMap.SlotMapGameObject = created_map;
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
            Slot loaded_slot = serialized_slots_array[i].SerializableSlotSwitchToNormalSlot();
            GameObject loaded_slot_gameobject = Instantiate(
                Resources.Load(ResourcePaths.Resources[Prefab.Slot]) as GameObject
            );
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
