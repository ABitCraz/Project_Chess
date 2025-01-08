using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using Unity.Burst;
using Unity.Jobs;
using UnityEngine;
using UnityEngine.Jobs;
using UnityEngine.UI;

public class CreateSlotMap
{
    GameObject EssentialDatumGameObject;

    public void CreateSlotMapComponentActionsOnStart(
        ref GameObject slot_map_game_object,
        GameObject map_loader_game_object,
        GameObject essential_datum_game_object
    )
    {
        GameObject GenerateMapButton = slot_map_game_object.transform.GetChild(0).gameObject;
        GameObject XSlotInputGameObject = slot_map_game_object.transform.GetChild(1).gameObject;
        GameObject YSlotInputGameObject = slot_map_game_object.transform.GetChild(2).gameObject;
        EssentialDatumGameObject = essential_datum_game_object;
        GenerateMapButton
            .GetComponent<Button>()
            .onClick.AddListener(() =>
            {
                InitializeMapGameObject(
                    ref map_loader_game_object,
                    XSlotInputGameObject.GetComponent<TMP_InputField>().text,
                    YSlotInputGameObject.GetComponent<TMP_InputField>().text
                );
            });
    }

    private void InitializeMapGameObject(
        ref GameObject slot_map_game_object,
        string x_count_string,
        string y_count_string
    )
    {
        if (
            slot_map_game_object
                .GetComponent<MapFileLoaderComponent>()
                .map_file_loader.save.SlotMap.SlotMapGameObject != null
        )
        {
            MonoBehaviour.Destroy(
                slot_map_game_object
                    .GetComponent<MapFileLoaderComponent>()
                    .map_file_loader.save.SlotMap.SlotMapGameObject
            );
            slot_map_game_object
                .GetComponent<MapFileLoaderComponent>()
                .map_file_loader.save.SlotMap = new();
        }
        int x_count = Convert.ToInt32(x_count_string);
        int y_count = Convert.ToInt32(y_count_string);
        SlotMap slot_map = new() { MapSize = new(x_count, y_count) };
        GameObject slot_map_go = new("SlotMap");
        slot_map.SlotMapGameObject = slot_map_go;
        slot_map_go.AddComponent<SlotMapComponent>().thisSlotMap = slot_map;
        slot_map_game_object.GetComponent<MapFileLoaderComponent>().map_file_loader.save.SlotMap =
            slot_map;
        CreateMapAndSlotGameObjects(slot_map);
    }

    private async void CreateMapAndSlotGameObjects(SlotMap slot_map)
    {
        GameObject slot_go = Resources.Load(ResourcePaths.Resources[Prefab.Slot]) as GameObject;
        slot_go.GetComponent<SlotComponent>().thisSlot = new() { SlotGameObject = slot_go };
        Vector3Int spawn_place = Vector3Int.zero;
        int game_map_size_x = slot_map.MapSize[0];
        int game_map_size_y = slot_map.MapSize[1];
        List<Slot> whole_slot = new();
        for (int i = 0; i < game_map_size_x; i++)
        {
            spawn_place.x = i;
            for (int j = 0; j < game_map_size_y; j++)
            {
                spawn_place.y = j;

                await SpawnSlotGameObject(slot_go, spawn_place, slot_map, whole_slot);
            }
        }
        slot_map.FullSlotMap = whole_slot.ToArray();
    }

    private async Awaitable SpawnSlotGameObject(
        GameObject slot_go,
        Vector3Int spawn_place,
        SlotMap slot_map,
        List<Slot> whole_slot
    )
    {
        GameObject spawned_slot = MonoBehaviour.Instantiate(slot_go);
        spawned_slot.transform.position = spawn_place;
        spawned_slot.transform.parent = slot_map.SlotMapGameObject.transform;
        Slot this_slot = spawned_slot.GetComponent<SlotComponent>().thisSlot = new()
        {
            SlotGameObject = spawned_slot,
            Position = new Vector2Int(spawn_place.x, spawn_place.y),
            FactPosition = spawn_place,
        };
        SlotLoader.LoadGameObject(ref spawned_slot);
        await Task.Run(() =>
        {
            slot_map.FullSlotDictionary.Add(
                new Vector2Int(spawn_place.x, spawn_place.y),
                this_slot
            );
            whole_slot.Add(this_slot);
        });
    }
}
