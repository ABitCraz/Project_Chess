using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MapFileControls : MonoBehaviour
{
    SaveAndLoad snl = new();
    public GameObject FileDirectoryObject;
    public GameObject ActionDoneObject;
    public static DataManeuvers.SaveSlotDatum SaveSlots;
    bool isempty = true;
    public SavingDatum save = new();
    public bool iscover;
    public GameObject SaveButton;
    public GameObject CoverButton;
    public GameObject LoadButton;
    public GameObject ClearMapButton;

    private void Awake()
    {
        SaveSlots += FillTheSaveSlots;
        SaveButton
            .GetComponent<Button>()
            .onClick.AddListener(() =>
            {
                snl.SaveMapJSONFile(save, FileDirectoryObject, ActionDoneObject, false);
                isempty = true;
            });
        CoverButton
            .GetComponent<Button>()
            .onClick.AddListener(() =>
            {
                snl.SaveMapJSONFile(save, FileDirectoryObject, ActionDoneObject, true);
                isempty = true;
            });
        LoadButton
            .GetComponent<Button>()
            .onClick.AddListener(() =>
            {
                LoadFileToSlot();
                isempty = true;
            });
        ClearMapButton
            .GetComponent<Button>()
            .onClick.AddListener(() =>
            {
                GameObject createdmap = this.GetComponent<CreateSlotMap>().CreatedMap;
                Destroy(createdmap);
                createdmap = null;
                isempty = true;
            });
    }

    private void Update()
    {
        if (isempty)
        {
            if (save.saveslots.Count <= 0)
            {
                SaveButton.GetComponent<Button>().interactable = false;
                ActionDoneObject.GetComponent<TMP_Text>().text = "地图空的，把作者存进去?";
            }
            else if (save.saveslots.Count > 0)
            {
                SaveButton.GetComponent<Button>().interactable = true;
                ActionDoneObject.GetComponent<TMP_Text>().text = "地图可以保存了";
                isempty = false;
            }
        }
    }

    private void LoadFileToSlot()
    {
        try
        {
            SavingDatum saved = snl.LoadMapJSONFile(ref FileDirectoryObject, ref ActionDoneObject);
            List<SerializableSlot> s_slots = saved.saveslots;
            if ((s_slots.Count > 0) && (s_slots != null))
            {
                if (this.GetComponent<CreateSlotMap>().CreatedMap != null)
                {
                    ActionDoneObject.GetComponent<TMP_Text>().text = "地图并不是空的，请先清理地图";
                    return;
                }
                GameObject createdmap = new("SlotMap");
                for (int i = 0; i < s_slots.Count; i++)
                {
                    GameObject loadslot = Instantiate(
                        Resources.Load(ResourcePaths.Resources[Prefab.Slot]) as GameObject
                    );
                    loadslot.GetComponent<SlotComponent>().thisSlot = s_slots[
                        i
                    ].SwitchToNormalSlot();
                    loadslot.transform.position = loadslot
                        .GetComponent<SlotComponent>()
                        .thisSlot.FactPosition;
                    loadslot.transform.SetParent(createdmap.transform);
                    SlotLoader.LoadGameObjectFromType(ref loadslot);
                }
                this.GetComponent<CreateSlotMap>().CreatedMap = createdmap;
            }
        }
        catch (Exception except)
        {
            print("文件不可读:\n" + except);
        }
    }

    private void FillTheSaveSlots()
    {
        save.saveslots = new();
        GameObject slotmap = this.GetComponent<CreateSlotMap>().CreatedMap;
        int slotcount = slotmap.transform.childCount;
        for (int i = 0; i < slotcount; i++)
        {
            save.saveslots.Add(
                slotmap.transform
                    .GetChild(i)
                    .GetComponent<SlotComponent>()
                    .thisSlot.SwapToSerializableSlot()
            );
        }
    }
}
