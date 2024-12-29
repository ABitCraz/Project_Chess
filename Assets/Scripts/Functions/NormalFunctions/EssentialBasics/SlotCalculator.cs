using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public static class SlotCalculator
{
    public static int CalculateDistance(ref Slot origin_slot, ref Slot target_slot)
    {
        int distance = (int)
            Mathf.Round(
                Mathf.Abs(origin_slot.Position.x - target_slot.Position.x)
                    + Mathf.Abs(origin_slot.Position.y - target_slot.Position.y)
            );
        return distance;
    }

    public static int CalculateDistance(int[] origin_slot, int[] target_slot)
    {
        int distance = (int)
            Mathf.Round(
                Mathf.Abs(origin_slot[0] - target_slot[0])
                    + Mathf.Abs(origin_slot[1] - target_slot[1])
            );
        return distance;
    }

    public static List<Vector2Int> CalculateAllPositionsInRange(
        Vector2Int origin_position,
        int distance
    )
    {
        List<Vector2Int> target_positions = new();
        for (int i = origin_position.x - distance; i < origin_position.x + distance; i++)
        {
            for (int j = origin_position.y - distance; j < origin_position.y + distance; j++)
            {
                if (
                    CalculateDistance(
                        new[] { origin_position.x, origin_position.y },
                        new[] { i, j }
                    ) <= distance
                )
                {
                    target_positions.Add(new Vector2Int(i, j));
                }
            }
        }
        return target_positions;
    }

    public static List<Vector2Int> BoundaryFilter(
        ref List<Vector2Int> positions,
        int xmax,
        int ymax
    )
    {
        for (int i = 0; i < positions.Count; i++)
        {
            if (positions[i].x < 0 || positions[i].x >= xmax)
            {
                positions.RemoveAt(i);
                i--;
            }
            if (positions[i].y < 0 || positions[i].y >= ymax)
            {
                positions.RemoveAt(i);
                i--;
            }
        }
        return positions;
    }

    public static List<Vector2Int> RemoveEveryPositionFromAnother(
        ref List<Vector2Int> origin,
        ref List<Vector2Int> target
    )
    {
        for (int i = 0; i < origin.Count; i++)
        {
            for (int j = 0; j < target.Count; j++)
            {
                if (origin[i].Equals(target[j]))
                {
                    origin.RemoveAt(i);
                    i--;
                    break;
                }
            }
        }
        return origin;
    }

    public static Vector2Int[] CalculateValidPositionInDistance(
        Vector2Int origin_position,
        int in_range,
        int blind_range,
        int xmax,
        int ymax
    )
    {
        List<Vector2Int> in_range_positions = CalculateAllPositionsInRange(
            origin_position,
            in_range
        );
        List<Vector2Int> blind_range_positions = CalculateAllPositionsInRange(
            origin_position,
            blind_range
        );
        List<Vector2Int> available_positions = RemoveEveryPositionFromAnother(
            ref in_range_positions,
            ref blind_range_positions
        );
        List<Vector2Int> valid_range = BoundaryFilter(ref available_positions, xmax, ymax);
        return valid_range.ToArray();
    }

    public static Slot[] CalculateSlotInVisionRange(
        ref Slot origin_slot,
        ref Dictionary<Vector2Int, Slot> slot_dictionary,
        ref Vector2Int map_size
    )
    {
        Chess current_chess = origin_slot.Chess;
        Landscape current_landscape = origin_slot.Landscape;
        int in_range =
            (current_chess.VisionInRange + current_landscape.VisionRangeEffectInteger)
            * current_landscape.VisionRangeEffectPercentage
            / 100;
        int blind_range =
            (current_chess.VisionBlindRange + current_landscape.VisionRangeEffectInteger)
            * current_landscape.VisionRangeEffectPercentage
            / 100;
        in_range = in_range < 0 ? 0 : in_range;
        blind_range = blind_range < 0 ? 0 : blind_range;
        Vector2Int[] in_range_position = CalculateValidPositionInDistance(
            origin_slot.Position,
            in_range,
            blind_range,
            map_size[0],
            map_size[1]
        );
        List<Slot> get_slots = new();
        for (int i = 0; i < in_range_position.Length; i++)
        {
            Slot target_slot = slot_dictionary[in_range_position[i]];
            if (target_slot.Landscape.IsOvershadowed)
                continue;
            get_slots.Add(target_slot);
        }
        return get_slots.ToArray();
    }

    public static Slot[] CalculateSlotInAttackRange(
        ref Slot origin_slot,
        ref Dictionary<Vector2Int, Slot> slot_dictionary,
        ref Vector2Int map_size
    )
    {
        Chess current_chess = origin_slot.Chess;
        Landscape current_landscape = origin_slot.Landscape;
        int in_range =
            (current_chess.AttackRange[0] + current_landscape.AttackRangeEffectInteger)
            * current_landscape.AttackRangeEffectPercentage
            / 100;
        int blind_range =
            (current_chess.AttackRange[1] + current_landscape.AttackRangeEffectInteger)
            * current_landscape.AttackRangeEffectPercentage
            / 100;
        in_range = in_range < 0 ? 0 : in_range;
        blind_range = blind_range < 0 ? 0 : blind_range;
        Vector2Int[] in_range_position = CalculateValidPositionInDistance(
            origin_slot.Position,
            in_range,
            blind_range,
            map_size[0],
            map_size[1]
        );
        List<Slot> get_slots = new();
        for (int i = 0; i < in_range_position.Length; i++)
        {
            get_slots.Add(slot_dictionary[in_range_position[i]]);
        }
        return get_slots.ToArray();
    }

    public static Slot[] CalculateSlotInMovementRange(
        ref Slot origin_slot,
        ref Dictionary<Vector2Int, Slot> slot_dictionary,
        ref Vector2Int map_size
    )
    {
        Vector2Int[] in_range_pos = CalculateValidPositionInDistance(
            origin_slot.Position,
            1,
            0,
            map_size[0],
            map_size[1]
        );
        List<Slot> get_slots = new();
        for (int i = 0; i < in_range_pos.Length; i++)
        {
            Slot target_slot = slot_dictionary[in_range_pos[i]];
            Chess current_chess = target_slot.Chess;
            Landscape current_landscape = target_slot.Landscape;
            int movement_remain =
                (current_chess.Movement + current_landscape.MovementAddonInteger)
                * current_landscape.MovementAddonPercentage
                / 100;
            if (current_landscape.MovementPrice <= movement_remain)
            {
                get_slots.Add(target_slot);
            }
        }
        return get_slots.ToArray();
    }
}
