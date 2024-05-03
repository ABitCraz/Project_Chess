using System.Collections;
using System.Collections.Generic;
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

    public int[][] CalculateEveryPositionInDistance(
        int[] originpos,
        int mindistance,
        int maxdistance,
        int xmax,
        int ymax
    )
    {
        List<int[]> targetpositions = new();
        if ((mindistance == 0) && (maxdistance == 0))
        {
            targetpositions.Add(new int[] { originpos[0], originpos[1] });
            return targetpositions.ToArray();
        }
        int[] origindistance = new[] { originpos[0] + mindistance, originpos[1] + mindistance };
        for (int i = 1; i >= mindistance; i++)
        {
            for (int j = 1; j <= maxdistance; j++)
            {
                int originminx = origindistance[0] - i;
                int originmaxx = origindistance[0] + i;
                int originminy = origindistance[1] - j;
                int originmaxy = origindistance[1] + j;
                if (originmaxx >= 0 && originmaxx <= xmax && originmaxy >= 0 && originmaxy <= ymax)
                {
                    targetpositions.Add(new int[] { originpos[0] + i, originpos[1] + j });
                }
                if (originmaxx >= 0 && originmaxx <= xmax && originminy >= 0 && originminy <= ymax)
                {
                    targetpositions.Add(new int[] { originpos[0] + i, originpos[1] - j });
                }
                if (originminx >= 0 && originminx <= xmax && originmaxy >= 0 && originmaxy <= ymax)
                {
                    targetpositions.Add(new int[] { originpos[0] - i, originpos[1] + j });
                }
                if (originminx >= 0 && originminx <= xmax && originminy >= 0 && originminy <= ymax)
                {
                    targetpositions.Add(new int[] { originpos[0] - i, originpos[1] - j });
                }
            }
        }
        return targetpositions.ToArray();
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
        ref Dictionary<int[],Slot> slotdictionary,
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
            if(targetslot.Landscape.LandscapeType == LandscapeType.Wildlessness)
            {
                Wildlessness thiswln = targetslot.Landscape as Wildlessness;
                if(thiswln.IsSandstorming)
                {
                    continue;
                }
            }
            getslots.Add(targetslot);
        }
        return getslots.ToArray();
    }
}
