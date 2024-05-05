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
    bool isempty = true;
    public SavingDatum save = new();
    public GameObject SaveButton;
    public GameObject CoverButton;
    public GameObject LoadButton;
    public GameObject ClearMapButton;

    private void Awake()
    {
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
                GameObject createdmap = save.SlotMap.SlotMapGameObject;
                Destroy(createdmap);
                createdmap = null;
                isempty = true;
            });
    }

    private void Update()
    {
        if (isempty)
        {
            if (save.SlotMap.FullSlotMap == null)
            {
                return;
            }
            if (save.SlotMap.FullSlotMap.Length <= 0)
            {
                SaveButton.GetComponent<Button>().interactable = false;
                ActionDoneObject.GetComponent<TMP_Text>().text = "地图空的，把作者存进去?";
            }
            else if (save.SlotMap.FullSlotMap.Length > 0)
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
            save = snl.LoadMapJSONFile(ref FileDirectoryObject, ref ActionDoneObject);
            List<SerializableSlot> s_slots = save.SaveSlots;
            if ((s_slots.Count > 0) && (s_slots != null))
            {
                if (save.SlotMap.FullSlotMap != null)
                {
                    ActionDoneObject.GetComponent<TMP_Text>().text = "地图并不是空的，请先清理地图";
                    return;
                }
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
                    loadedslotgameobject.GetComponent<SlotComponent>().thisSlot.SlotGameObject = loadedslotgameobject;
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
