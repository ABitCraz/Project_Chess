using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;
using UnityEditor.SceneManagement;

public class CreateSlotMap : MonoBehaviour
{
    public GameObject GenerateMapButton;
    public GameObject X_Slot_Input;
    public GameObject Y_Slot_Input;

    private void Awake()
    {
        GenerateMapButton.GetComponent<Button>().onClick.AddListener(MapGenerateClicked);
    }

    private void MapGenerateClicked()
    {
        if (this.GetComponent<MapFileControls>().save.SlotMap.SlotMapGameObject != null)
        {
            Destroy(this.GetComponent<MapFileControls>().save.SlotMap.SlotMapGameObject);
            this.GetComponent<MapFileControls>().save.SlotMap = new();
        }
        int xcount = Convert.ToInt32(X_Slot_Input.GetComponent<TMP_InputField>().text);
        int ycount = Convert.ToInt32(Y_Slot_Input.GetComponent<TMP_InputField>().text);
        SlotMap slotmap = new() { MapSize = new[] { xcount, ycount } };
        GameObject emptyslotgo = Resources.Load(ResourcePaths.Resources[Prefab.Slot]) as GameObject;
        emptyslotgo.GetComponent<SlotComponent>().thisSlot.SlotGameObject = emptyslotgo;
        GameObject slotmapgo = new("SlotMap");
        slotmapgo.AddComponent<MapControl>();
        slotmap.SlotMapGameObject = slotmapgo;
        slotmapgo.AddComponent<SlotMapComponent>().thisSlotMap = slotmap;
        this.GetComponent<MapFileControls>().save.SlotMap = slotmap;
        CreateSlots(emptyslotgo, ref slotmap);
    }

    private void CreateSlots(GameObject slotgo, ref SlotMap slotmap)
    {
        Vector3Int spawnplace = Vector3Int.zero;
        int gamemapsizex = slotmap.MapSize[0];
        int gamemapsizey = slotmap.MapSize[1];
        List<Slot> wholeslot = new();
        for (int i = 0; i < gamemapsizex; i++)
        {
            spawnplace.x = i;
            for (int j = 0; j < gamemapsizey; j++)
            {
                spawnplace.y = j;
                GameObject spawnedslot = Instantiate(slotgo);
                spawnedslot.transform.position = spawnplace;
                spawnedslot.transform.parent = slotmap.SlotMapGameObject.transform;
                Slot thisslot = spawnedslot.GetComponent<SlotComponent>().thisSlot;
                thisslot.Position = new int[] { i, j };
                thisslot.FactPosition = spawnplace;
                SlotLoader.LoadGameObjectFromType(ref spawnedslot);
                slotmap.FullSlotDictionary.Add(new int[] { i, j }, thisslot);
                wholeslot.Add(thisslot);
            }
        }
        slotmap.FullSlotMap = wholeslot.ToArray();
    }
}
