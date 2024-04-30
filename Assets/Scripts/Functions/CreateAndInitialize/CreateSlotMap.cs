using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;
using UnityEditor.SceneManagement;

public class CreateSlotMap : MonoBehaviour
{
    SaveAndLoad snl = new();
    public GameObject LoadFileDirectoryObject;
    public GameObject LoadFileDoneObject;
    public GameObject X_Slot_Input;
    public GameObject Y_Slot_Input;
    public GameObject createdmap;

    private void Awake()
    {
        this.GetComponent<Button>().onClick.AddListener(MapGenerateClicked);
        LoadMap.LoadSlots += () =>
        {
            List<Slot> slots = snl.LoadMapFile(
                ref LoadFileDirectoryObject,
                ref LoadFileDoneObject
            );
            if (slots.Count > 0)
            {
                createdmap = new("SlotMap");
                for (int i = 0; i < slots.Count; i++)
                {
                    GameObject loadslot = Instantiate(Resources.Load(ResourcePaths.Resources[Prefab.Slot]) as GameObject);
                    loadslot.AddComponent<SlotComponent>().thisSlot = slots[i];
                    loadslot.transform.position = loadslot
                        .GetComponent<SlotComponent>()
                        .thisSlot.FactPosition;
                    loadslot.transform.SetParent(createdmap.transform);
                }
            }
        };
    }

    private void MapGenerateClicked()
    {
        if (createdmap != null)
        {
            Destroy(createdmap);
        }
        int xcount = Convert.ToInt32(X_Slot_Input.GetComponent<TMP_InputField>().text);
        int ycount = Convert.ToInt32(Y_Slot_Input.GetComponent<TMP_InputField>().text);
        GameMap gamemap = new(xcount, ycount);
        GameObject emptyslotgo = Resources.Load(ResourcePaths.Resources[Prefab.Slot]) as GameObject;
        GameObject slotmap = new("SlotMap");
        slotmap.AddComponent<MapControl>();
        createdmap = slotmap;
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
                Slot thisslot = spawnedslot.GetComponent<SlotComponent>().thisSlot;
                thisslot.Position = new Vector2(i, j);
                thisslot.FactPosition = spawnplace;
                LoadGameObjectFromType(ref thisslot, ref spawnedslot);
            }
        }
    }

    private void LoadGameObjectFromType(ref Slot slot, ref GameObject slotobject)
    {
        if (
            slot.Landscape != null
            && slot.Landscape.LandscapeType.GetType() == typeof(LandscapeType)
        )
        {
            GameObject CreatedObject = Instantiate(
                EssenitalDatumLoader.GameObjectDictionary[Prefab.Landscape]
            );
            slot.Landscape.UnitSprite = CreatedObject.GetComponent<SpriteRenderer>().sprite;
            slot.Landscape.UnitGameObject = CreatedObject;
            Landscape targetlandscape = CreatedObject
                .GetComponent<LandscapeComponent>()
                .thislandscape;
            targetlandscape = slot.Landscape;
            targetlandscape.PutToSlotPosition(ref slotobject);
            targetlandscape.LoadLandscapeSprite();
            CreatedObject.transform.SetParent(slotobject.transform);
            CreatedObject.transform.localPosition = new Vector3(0, 0, 0.2f);
        }
        if (
            slot.Construction != null
            && slot.Construction.ConstructionType.GetType() == typeof(ConstructionType)
        )
        {
            GameObject CreatedObject = Instantiate(
                EssenitalDatumLoader.GameObjectDictionary[Prefab.Construction]
            );
            slot.Construction.UnitSprite = CreatedObject.GetComponent<SpriteRenderer>().sprite;
            slot.Construction.UnitGameObject = CreatedObject;
            Construction targetconstruction = CreatedObject
                .GetComponent<ConstructionComponent>()
                .thisconstruction;
            targetconstruction = slot.Construction;
            targetconstruction.PutToSlotPosition(ref slotobject);
            targetconstruction.LoadConstructionSprite();
            CreatedObject.transform.localScale *= 0.85f;
            CreatedObject.transform.SetParent(slotobject.transform);
            CreatedObject.transform.localPosition = new Vector3(0, 0, 0.4f);
        }
        if (slot.Chess != null && slot.Chess.ChessType.GetType() == typeof(ChessType))
        {
            GameObject CreatedObject = Instantiate(
                EssenitalDatumLoader.GameObjectDictionary[Prefab.Chess]
            );
            slot.Chess.UnitSprite = CreatedObject.GetComponent<SpriteRenderer>().sprite;
            slot.Chess.UnitGameObject = CreatedObject;
            Chess targetchess = CreatedObject.GetComponent<ChessComponent>().thischess;
            targetchess = slot.Chess;
            targetchess.PutToSlotPosition(ref slotobject);
            targetchess.LoadChessSprite();
            CreatedObject.transform.localScale *= 0.7f;
            CreatedObject.transform.SetParent(slotobject.transform);
            CreatedObject.transform.localPosition = new Vector3(0, 0, 0.6f);
        }
        SaveMap.SaveSlots.Invoke(ref slot);
    }
}
