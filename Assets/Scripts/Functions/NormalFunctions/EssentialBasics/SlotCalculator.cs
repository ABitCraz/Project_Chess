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
            Mathf.Abs(originslot.Position[0] - targetslot.Position[1])
            + Mathf.Abs(originslot.Position[0] - targetslot.Position[1]);
        return distance;
    }

    public List<int[]> CalculateWholePositions(int[] originpos, int distance)
    {
        List<int[]> targetpositions = new();
        for (int i = 0; i <= distance; i++)
        {
            for (int j = 0; j <= distance - i; j++)
            {
                if ((j == 0) && (i == 0))
                {
                    targetpositions.Add(new int[] { originpos[0], originpos[1] });
                    continue;
                }
                targetpositions.Add(new int[] { originpos[0] + i, originpos[1] + j });
                targetpositions.Add(new int[] { originpos[0] + i, originpos[1] - j });
                targetpositions.Add(new int[] { originpos[0] - i, originpos[1] + j });
                targetpositions.Add(new int[] { originpos[0] - i, originpos[1] - j });
            }
        }
        List<int[]> distinctpositions = new();
        for (int i = 0; i < targetpositions.Count; i++)
        {
            int[] unique = targetpositions[i];
            for (int j = 0; j < distinctpositions.Count; j++)
            {
                if (unique.SequenceEqual(distinctpositions[j]))
                {
                    unique = new int[] { -1, -1 };
                }
            }
            if (unique[0] != -1)
            {
                distinctpositions.Add(unique);
            }
        }
        return distinctpositions;
    }

    public List<int[]> BoundaryFilter(ref List<int[]> positions, int xmax, int ymax)
    {
        for (int i = 0; i < positions.Count; i++)
        {
            if (positions[i][0] < 0 || positions[i][0] > xmax)
            {
                positions.RemoveAt(i);
                i--;
            }
            if (positions[i][1] < 0 || positions[i][1] > ymax)
            {
                positions.RemoveAt(i);
                i--;
            }
        }
        return positions;
    }

    private void RemoveEveryPositionFromAnother(ref List<int[]> origin, ref List<int[]> target)
    {
        for (int i = 0; i < origin.Count; i++)
        {
            for (int j = 0; j < target.Count; j++)
            {
                if (origin[i].SequenceEqual(target[j]))
                {
                    origin.RemoveAt(i);
                    i--;
                    break;
                }
            }
        }
    }

    public int[][] CalculateEveryPositionInDistance(
        int[] originpos,
        int mindistance,
        int maxdistance,
        int xmax,
        int ymax
    )
    {
        List<int[]> maxpositions = CalculateWholePositions(originpos, maxdistance);
        List<int[]> minpositions = CalculateWholePositions(originpos, mindistance);
        RemoveEveryPositionFromAnother(ref maxpositions, ref minpositions);
        List<int[]> result = BoundaryFilter(ref maxpositions, xmax, ymax);
        return result.ToArray();
    }

    public int[][] CalculateEveryPositionInDistance(
        int[] originpos,
        int distance,
        int xmax,
        int ymax
    )
    {
        List<int[]> maxpositions = CalculateWholePositions(originpos, distance);
        List<int[]> result = BoundaryFilter(ref maxpositions, xmax, ymax);
        return result.ToArray();
    }

    public Slot[] CalculateSlotInAttackRange(
        ref Slot originslot,
        ref Dictionary<int[], Slot> slotdictionary,
        ref int[] mapsize
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

        int[][] inrangepos = CalculateEveryPositionInDistance(
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
        ref Dictionary<int[], Slot> slotdictionary,
        ref int[] mapsize
    )
    {
        Chess attackchess = originslot.Chess;
        Landscape steplandscape = originslot.Landscape;
        int visionrange = attackchess.Vision + steplandscape.EffectVision;
        visionrange = visionrange < 0 ? 0 : visionrange;
        int[][] inrangepos = CalculateEveryPositionInDistance(
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
            if (targetslot.Landscape.LandscapeType == LandscapeType.Wildlessness)
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
        ref Dictionary<int[], Slot> slotdictionary,
        ref int[] mapsize
    )
    {
        Chess attackchess = originslot.Chess;
        Landscape steplandscape = originslot.Landscape;
        int visionrange = attackchess.Vision + steplandscape.EffectVision;
        visionrange = visionrange < 0 ? 0 : visionrange;
        int[][] inrangepos = CalculateEveryPositionInDistance(
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
            getslots.Add(targetslot);
        }
        return getslots.ToArray();
    }
}
