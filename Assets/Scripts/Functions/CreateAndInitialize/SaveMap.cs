using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class SaveMap : MonoBehaviour
{
    SaveAndLoad snl = new();
    public GameObject SaveFileDirectoryObject;
    public GameObject SaveFileDoneObject;
    public static DataManeuvers.SaveSlotDatum SaveSlots;
    bool isempty = true;
    public List<Slot> slots;

    private void Awake()
    {
        SaveSlots += FillTheSaveSlots;
        this.GetComponent<Button>()
            .onClick.AddListener(() =>
            {
                snl.SaveMapFile(slots, SaveFileDirectoryObject, SaveFileDoneObject);
                isempty = true;
            });
    }

    private void Update()
    {
        if (isempty)
        {
            if (slots.Count <= 0)
            {
                this.GetComponent<Button>().interactable = false;
                SaveFileDoneObject.GetComponent<TMP_Text>().text = "地图空的，把作者存进去?";
            }
            else if (slots.Count > 0)
            {
                this.GetComponent<Button>().interactable = true;
                SaveFileDoneObject.GetComponent<TMP_Text>().text = "地图可以保存了";
                isempty = false;
            }
        }
    }

    private void FillTheSaveSlots(ref Slot saveslot)
    {
        slots.Add(saveslot);
    }
}
