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
    public GameObject CreatedMap;

    private void Awake()
    {
        GenerateMapButton.GetComponent<Button>().onClick.AddListener(MapGenerateClicked);
    }

    private void MapGenerateClicked()
    {
        if (CreatedMap != null)
        {
            Destroy(CreatedMap);
        }
        int xcount = Convert.ToInt32(X_Slot_Input.GetComponent<TMP_InputField>().text);
        int ycount = Convert.ToInt32(Y_Slot_Input.GetComponent<TMP_InputField>().text);
        GameMap gamemap = new(xcount, ycount);
        GameObject emptyslotgo = Resources.Load(ResourcePaths.Resources[Prefab.Slot]) as GameObject;
        GameObject slotmap = new("SlotMap");
        slotmap.AddComponent<MapControl>();
        CreatedMap = slotmap;
        CreateSlots(emptyslotgo, gamemap, ref slotmap);
    }

    private void CreateSlots(GameObject slotgo, GameMap gamemap, ref GameObject slotmap)
    {
        Vector3Int spawnplace = Vector3Int.zero;
        int gamemapsizex = gamemap.Size.x;
        int gamemapsizey = gamemap.Size.y;
        for (int i = 0; i < gamemapsizex; i++)
        {
            spawnplace.x = i;
            for (int j = 0; j < gamemapsizey; j++)
            {
                spawnplace.y = j;
                GameObject spawnedslot = Instantiate(slotgo);
                spawnedslot.transform.position = spawnplace;
                spawnedslot.transform.parent = slotmap.transform;
                Slot thisslot = spawnedslot.GetComponent<SlotComponent>().thisSlot = new(LandscapeType.Wildlessness);
                thisslot.Position = new Vector2(i, j);
                thisslot.FactPosition = spawnplace;
                SlotLoader.LoadGameObjectFromType(ref spawnedslot);
                MapFileControls.SaveSlots();
            }
        }
    }
}
