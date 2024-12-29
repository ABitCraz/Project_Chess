using System.Text;
using TMPro;
using UnityEngine;

public static class ShowStatus
{
    static readonly string landscape_head_string = "地形:";
    static readonly string construction_head_string = "建筑:";
    static readonly string chess_head_string = "单位:";
    static StringBuilder status_text_string_builder = new();

    public static void ShowSlotStatus(
        ref GameObject hit_slot_game_object,
        ref GameObject show_status_game_object
    )
    {
        TMP_Text status_TMP_text = show_status_game_object.GetComponent<TMP_Text>();
        if (!show_status_game_object.activeSelf)
        {
            show_status_game_object.SetActive(true);
        }
        Slot this_slot = hit_slot_game_object.GetComponent<SlotComponent>().thisSlot;
        status_text_string_builder.Clear();
        if (this_slot.Landscape != null)
        {
            status_text_string_builder
                .Append(landscape_head_string)
                .Append(this_slot.Landscape.LandscapeType.ToString())
                .Append(" ")
                .Append(this_slot.Landscape.LandscapeName)
                .Append("\n");
        }
        if (this_slot.Construction != null)
        {
            status_text_string_builder
                .Append(construction_head_string)
                .Append(this_slot.Construction.ConstructionType.ToString())
                .Append(" ")
                .Append(this_slot.Construction.ConstructionName)
                .Append("\n");
        }
        if (this_slot.Chess != null)
        {
            status_text_string_builder
                .Append(chess_head_string)
                .Append(this_slot.Chess.ChessType.ToString())
                .Append(" ")
                .Append(this_slot.Chess.ChessName)
                .Append("\n");
        }
        status_TMP_text.text = status_text_string_builder.ToString();
    }
}
