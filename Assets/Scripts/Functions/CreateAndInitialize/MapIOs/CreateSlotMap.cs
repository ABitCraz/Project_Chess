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

public class CreateSlotMap : MonoBehaviour
{
    public GameObject GenerateMapButton;
    public GameObject X_Slot_Input;
    public GameObject Y_Slot_Input;

    private void Awake()
    {
        GenerateMapButton.GetComponent<Button>().onClick.AddListener(InitializeMapGameObject);
    }

    private void InitializeMapGameObject()
    {
        if (this.GetComponent<MapFileLoader>().save.SlotMap.SlotMapGameObject != null)
        {
            Destroy(this.GetComponent<MapFileLoader>().save.SlotMap.SlotMapGameObject);
            this.GetComponent<MapFileLoader>().save.SlotMap = new();
        }
        int x_count = Convert.ToInt32(X_Slot_Input.GetComponent<TMP_InputField>().text);
        int y_count = Convert.ToInt32(Y_Slot_Input.GetComponent<TMP_InputField>().text);
        SlotMap slot_map = new() { MapSize = new(x_count, y_count) };
        GameObject slot_map_go = new("SlotMap");
        slot_map_go.AddComponent<MapControl>();
        slot_map.SlotMapGameObject = slot_map_go;
        slot_map_go.AddComponent<SlotMapComponent>().thisSlotMap = slot_map;
        this.GetComponent<MapFileLoader>().save.SlotMap = slot_map;
        CreateMapAndSlotGameObjects(slot_map);
    }

    private async void CreateMapAndSlotGameObjects(SlotMap slot_map)
    {
        GameObject slot_go = Resources.Load(ResourcePaths.Resources[Prefab.Slot]) as GameObject;
        slot_go.GetComponent<SlotComponent>().thisSlot.SlotGameObject = slot_go;
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
        GameObject spawned_slot = Instantiate(slot_go);
        spawned_slot.transform.position = spawn_place;
        spawned_slot.transform.parent = slot_map.SlotMapGameObject.transform;
        Slot this_slot = spawned_slot.GetComponent<SlotComponent>().thisSlot;
        this_slot.Position = new Vector2Int(spawn_place.x, spawn_place.y);
        this_slot.FactPosition = spawn_place;
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
