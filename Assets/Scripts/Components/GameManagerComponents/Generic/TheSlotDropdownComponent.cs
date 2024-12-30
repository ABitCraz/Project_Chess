using System;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using static ObjectActions;
using static TMPro.TMP_Dropdown;

public class TheSlotDropdownComponent : MonoBehaviour
{
    public static GetGameObjectFromRaycasts GetTargetSlotGameObjectFromRaycasts;
    public static GameObjectAction DisableDropdownGameObject;
    TMP_Dropdown landscape_dropdown;
    TMP_Dropdown construction_dropdown;
    TMP_Dropdown chess_dropdown;
    GameObject slot_gameobject;
    Vector3? temp_mouse_position;

    private void Awake()
    {
        landscape_dropdown = transform.GetChild(0).GetComponent<TMP_Dropdown>();
        construction_dropdown = transform.GetChild(1).GetComponent<TMP_Dropdown>();
        chess_dropdown = transform.GetChild(2).GetComponent<TMP_Dropdown>();
        GenerateTheDropDown();
        AddListenersToDropdown();
        GetTargetSlotGameObjectFromRaycasts += GetTargetSlotFromRaycast;
        DisableDropdownGameObject += () =>
        {
            this.gameObject.SetActive(false);
        };
        this.gameObject.SetActive(false);
    }

    private void GenerateTheDropDown()
    {
        GenerateLandscapeDropdown();
        GenerateConstructionDropdown();
        GenerateChessDropdown();
    }

    private void GenerateLandscapeDropdown()
    {
        List<OptionData> landscapes = new();
        int landscape_type_length = Enum.GetNames(typeof(LandscapeType)).Length;
        for (int i = 0; i < landscape_type_length; i++)
        {
            landscapes.Add(
                new OptionData(
                    i switch
                    {
                        1 => "荒地",
                        2 => "高地",
                        3 => "遗迹",
                        4 => "废墟",
                        5 => "沙漠",
                        6 => "峡谷",
                        _ => "空",
                    }
                )
            );
        }
        landscape_dropdown.AddOptions(landscapes);
    }

    private void GenerateConstructionDropdown()
    {
        List<OptionData> constructions = new();
        int construction_type_length = Enum.GetNames(typeof(ConstructionType)).Length;
        for (int i = 0; i < construction_type_length; i++)
        {
            constructions.Add(
                new OptionData(
                    i switch
                    {
                        1 => "城市",
                        _ => "空",
                    }
                )
            );
        }
        construction_dropdown.AddOptions(constructions);
    }

    private void GenerateChessDropdown()
    {
        List<OptionData> chesses = new();
        int chess_type_length = Enum.GetNames(typeof(ChessType)).Length;
        for (int i = 0; i < chess_type_length; i++)
        {
            chesses.Add(
                new OptionData(
                    i switch
                    {
                        1 => "尖兵",
                        2 => "反装甲步兵",
                        3 => "战车",
                        4 => "坦克",
                        5 => "迫击炮",
                        6 => "火炮",
                        7 => "将军",
                        _ => "空",
                    }
                )
            );
        }
        chess_dropdown.AddOptions(chesses);
    }

    private void GetTargetSlotFromRaycast(ref GameObject hit_game_object)
    {
        if (Input.GetMouseButtonUp((int)MouseButton.Left))
        {
            if (slot_gameobject == null || hit_game_object != slot_gameobject)
            {
                slot_gameobject = hit_game_object;
            }
            this.gameObject.SetActive(true);
            temp_mouse_position = Input.mousePosition;
            this.gameObject.transform.position = (Vector3)temp_mouse_position;
            ShowDropdownSlotType();
        }
    }

    private void ShowDropdownSlotType()
    {
        if (slot_gameobject == null || !slot_gameobject.TryGetComponent<SlotComponent>(out _))
            return;

        ShootTheMouseRay.pause_in_UI = true;
        Slot current_slot = slot_gameobject.GetComponent<SlotComponent>().thisSlot;
        landscape_dropdown.SetValueWithoutNotify((int)current_slot.Landscape.LandscapeType);
        construction_dropdown.SetValueWithoutNotify(
            (int)current_slot.Construction.ConstructionType
        );
        chess_dropdown.SetValueWithoutNotify((int)current_slot.Chess.ChessType);
    }

    private void AddListenersToDropdown()
    {
        landscape_dropdown.onValueChanged.AddListener(
            (selected_index) =>
            {
                if (slot_gameobject != null)
                {
                    Slot current_slot = slot_gameobject.GetComponent<SlotComponent>().thisSlot;
                    current_slot.InitializeOrSwapLandscape((LandscapeType)selected_index);
                }
            }
        );
        construction_dropdown.onValueChanged.AddListener(
            (selected_index) =>
            {
                if (slot_gameobject != null)
                {
                    Slot current_slot = slot_gameobject.GetComponent<SlotComponent>().thisSlot;
                    current_slot.InitializeOrSwapConstruction((ConstructionType)selected_index);
                }
            }
        );
        chess_dropdown.onValueChanged.AddListener(
            (selected_index) =>
            {
                if (slot_gameobject != null)
                {
                    Slot current_slot = slot_gameobject.GetComponent<SlotComponent>().thisSlot;
                    current_slot.InitializeOrSwapChess((ChessType)selected_index);
                }
            }
        );
    }
}
