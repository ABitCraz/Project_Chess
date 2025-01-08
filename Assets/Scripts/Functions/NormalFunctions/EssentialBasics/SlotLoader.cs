using System;
using UnityEditor;
using UnityEngine;

public class SlotLoader
{
    public static void LoadGameObject(ref GameObject slot_object)
    {
        Slot slot = slot_object.GetComponent<SlotComponent>().thisSlot;
        InitializeLandscape(ref slot, ref slot_object);
        InitializeConstruction(ref slot, ref slot_object);
        InitializeChess(ref slot, ref slot_object);
    }

    public static void SwapGameObject(ref GameObject slot_object, Prefab swap_type)
    {
        Slot slot = slot_object.GetComponent<SlotComponent>().thisSlot;
        switch (swap_type)
        {
            case Prefab.Landscape:
                InitializeLandscape(ref slot, ref slot_object);
                break;
            case Prefab.Construction:
                InitializeConstruction(ref slot, ref slot_object);
                break;
            case Prefab.Chess:
                InitializeChess(ref slot, ref slot_object);
                break;
        }
    }

    private static void InitializeLandscape(ref Slot init_slot, ref GameObject slot_game_object)
    {
        if (init_slot.Landscape != null)
        {
            GameObject created_object = MonoBehaviour.Instantiate(
                EssentialDatumLoader.GameObjectDictionary[Prefab.Landscape]
            );
            Landscape target_landscape = created_object
                .GetComponent<LandscapeComponent>()
                .thisLandscape = init_slot.Landscape;
            init_slot.Landscape.UnitSprite = created_object.GetComponent<SpriteRenderer>().sprite;
            init_slot.Landscape.UnitGameObject = created_object;
            target_landscape.PutToSlotPosition(ref slot_game_object);
            target_landscape.LoadSpriteAndAnimation();
            created_object.transform.SetParent(slot_game_object.transform);
            created_object.transform.localPosition = new Vector3(0, 0, 0.2f);
        }
    }

    private static void InitializeConstruction(ref Slot init_slot, ref GameObject slot_object)
    {
        if (init_slot.Construction != null)
        {
            GameObject created_object = MonoBehaviour.Instantiate(
                EssentialDatumLoader.GameObjectDictionary[Prefab.Construction]
            );
            Construction target_construction = created_object
                .GetComponent<ConstructionComponent>()
                .thisConstruction = init_slot.Construction;
            init_slot.Construction.UnitSprite = created_object
                .GetComponent<SpriteRenderer>()
                .sprite;
            init_slot.Construction.UnitGameObject = created_object;
            target_construction.PutToSlotPosition(ref slot_object);
            target_construction.LoadSpriteAndAnimation();
            created_object.transform.SetParent(slot_object.transform);
            created_object.transform.localPosition = new Vector3(0, 0, 0.4f);
        }
    }

    private static void InitializeChess(ref Slot init_slot, ref GameObject slot_object)
    {
        if (init_slot.Chess != null)
        {
            GameObject created_object = MonoBehaviour.Instantiate(
                EssentialDatumLoader.GameObjectDictionary[Prefab.Chess]
            );
            Chess target_chess = created_object.GetComponent<ChessComponent>().thisChess =
                init_slot.Chess;
            init_slot.Chess.UnitSprite = created_object.GetComponent<SpriteRenderer>().sprite;
            init_slot.Chess.UnitGameObject = created_object;
            target_chess.PutToSlotPosition(ref slot_object);
            target_chess.LoadSpriteAndAnimation();
            created_object.transform.SetParent(slot_object.transform);
            created_object.transform.localPosition = new Vector3(0, 0, 0.6f);
        }
    }
}
