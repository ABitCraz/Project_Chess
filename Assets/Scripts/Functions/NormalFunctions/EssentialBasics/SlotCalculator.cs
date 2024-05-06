using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;

public class SlotCalculator
{
    public int CalculateDistance(ref Slot originslot, ref Slot targetslot)
    {
        int distance =
            Mathf.Abs(originslot.Position.x - targetslot.Position.x)
            + Mathf.Abs(originslot.Position.y - targetslot.Position.y);
        return distance;
    }

    public List<Vector2Int> CalculateWholePositions(Vector2Int originpos, int distance)
    {
        List<Vector2Int> targetpositions = new();
        for (int i = 0; i <= distance; i++)
        {
            for (int j = 0; j <= distance - i; j++)
            {
                if ((j == 0) && (i == 0))
                {
                    targetpositions.Add(new Vector2Int(originpos.x, originpos.y));
                    continue;
                }
                targetpositions.Add(new Vector2Int(originpos.x + i, originpos.y + j));
                targetpositions.Add(new Vector2Int(originpos.x + i, originpos.y - j));
                targetpositions.Add(new Vector2Int(originpos.x - i, originpos.y + j));
                targetpositions.Add(new Vector2Int(originpos.x - i, originpos.y - j));
            }
        }
        List<Vector2Int> distinctpositions = new();
        for (int i = 0; i < targetpositions.Count; i++)
        {
            Vector2Int unique = targetpositions[i];
            for (int j = 0; j < distinctpositions.Count; j++)
            {
                if (unique.Equals(distinctpositions[j]))
                {
                    unique = new Vector2Int(-1, -1);
                }
            }
            if (unique.x != -1)
            {
                distinctpositions.Add(unique);
            }
        }
        return distinctpositions;
    }

    public List<Vector2Int> BoundaryFilter(ref List<Vector2Int> positions, int xmax, int ymax)
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

    private void RemoveEveryPositionFromAnother(
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
    }

    public Vector2Int[] CalculateEveryPositionInDistance(
        Vector2Int originpos,
        int mindistance,
        int maxdistance,
        int xmax,
        int ymax
    )
    {
        List<Vector2Int> maxpositions = CalculateWholePositions(originpos, maxdistance);
        List<Vector2Int> minpositions = CalculateWholePositions(originpos, mindistance);
        RemoveEveryPositionFromAnother(ref maxpositions, ref minpositions);
        List<Vector2Int> result = BoundaryFilter(ref maxpositions, xmax, ymax);
        return result.ToArray();
    }

    public Vector2Int[] CalculateEveryPositionInDistance(
        Vector2Int originpos,
        int distance,
        int xmax,
        int ymax
    )
    {
        List<Vector2Int> maxpositions = CalculateWholePositions(originpos, distance);
        List<Vector2Int> result = BoundaryFilter(ref maxpositions, xmax, ymax);
        return result.ToArray();
    }

    public Slot[] CalculateSlotInAttackRange(
        ref Slot originslot,
        ref Dictionary<Vector2Int, Slot> slotdictionary,
        ref Vector2Int mapsize
    )
    {
        if (originslot.Chess == null)
        {
            return null;
        }
        Chess attackchess = originslot.Chess;
        Landscape steplandscape = originslot.Landscape;
        int minrange = attackchess.AttackRange[0] + steplandscape.EffectRange;
        int maxrange = attackchess.AttackRange[1] + steplandscape.EffectRange;
        minrange = minrange < 0 ? 0 : minrange;
        maxrange = maxrange < 0 ? 0 : maxrange;
        Vector2Int[] inrangepos = CalculateEveryPositionInDistance(
            originslot.Position,
            minrange,
            maxrange,
            mapsize[0],
            mapsize[1]
        );
        List<Slot> getslots = new();
        for (int i = 0; i < inrangepos.Length; i++)
        {
            Slot targetslot = slotdictionary[inrangepos[i]];
            getslots.Add(targetslot);
        }
        return getslots.ToArray();
    }

    public Slot[] CalculateSlotInVisionRange(
        ref Slot originslot,
        ref Dictionary<Vector2Int, Slot> slotdictionary,
        ref Vector2Int mapsize
    )
    {
        Chess attackchess = originslot.Chess;
        Landscape steplandscape = originslot.Landscape;
        int visionrange = attackchess.Vision + steplandscape.EffectVision;
        visionrange = visionrange < 0 ? 0 : visionrange;
        Vector2Int[] inrangepos = CalculateEveryPositionInDistance(
            originslot.Position,
            0,
            visionrange,
            mapsize[0],
            mapsize[1]
        );
        List<Slot> getslots = new();
        for (int i = 0; i < inrangepos.Length; i++)
        {
            Slot targetslot = slotdictionary[inrangepos[i]];
            if (
                targetslot.Landscape != null
                && targetslot.Landscape.LandscapeType == LandscapeType.Wildlessness
            )
            {
                Wildlessness thiswln = targetslot.Landscape as Wildlessness;
                if (thiswln.IsSandstorming)
                {
                    continue;
                }
            }
            getslots.Add(targetslot);
        }
        return getslots.ToArray();
    }

    public Slot[] CalculateSlotInMovementRange(
        ref Slot originslot,
        ref Dictionary<Vector2Int, Slot> slotdictionary,
        ref Vector2Int mapsize
    )
    {
        Vector2Int[] inrangepos = CalculateEveryPositionInDistance(
            originslot.Position,
            0,
            1,
            mapsize[0],
            mapsize[1]
        );
        List<Slot> getslots = new();
        for (int i = 0; i < inrangepos.Length; i++)
        {
            Slot targetslot = slotdictionary[inrangepos[i]];
            if (targetslot.Landscape.LandscapeType == LandscapeType.Canyon)
            {
                continue;
            }
            if (targetslot.Landscape.LandscapeType == LandscapeType.Desert)
            {
                Desert targetdesert = targetslot.Landscape as Desert;
                if (targetdesert.IsQuicksand == true)
                {
                    continue;
                }
            }
            if (
                targetslot.Landscape.IsTroopersOnly == true
                && (
                    originslot.Chess.ChessType != ChessType.AA_Infantry
                    || originslot.Chess.ChessType != ChessType.Infantry
                )
            )
            {
                continue;
            }
            if (originslot.Chess.Movement - targetslot.Landscape.MovementPrice < 0)
            {
                continue;
            }
            if (targetslot.Chess != null)
            {
                continue;
            }
            getslots.Add(targetslot);
        }
        return getslots.ToArray();
    }
}
