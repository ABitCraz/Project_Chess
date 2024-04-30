using System;
using System.Collections.Generic;

public class ResourcePaths
{
    public static readonly Dictionary<Prefab, string> Resources =
        new()
        {
            { Prefab.Slot, "Prefabs/Basics/Slot" },
            { Prefab.Landscape, "Prefabs/Basics/Landscape" },
            { Prefab.Construction, "Prefabs/Basics/Construction" },
            { Prefab.Chess, "Prefabs/Basics/Chess" }
        };
    public static readonly Dictionary<UI, string> UIPrefabs =
        new() { { UI.SlotDropDown, "Prefabs/UIs/SlotDropDown" } };
    public static readonly Dictionary<Enum, string> TargetSprites =
        new()
        {
            //LandscapeTypes
            { LandscapeType.Ancient, "Images/Sprites/Landscape/Landscape" },
            { LandscapeType.Canyon, "Images/Sprites/Landscape/Landscape" },
            { LandscapeType.Desert, "Images/Sprites/Landscape/Landscape" },
            { LandscapeType.Highground, "Images/Sprites/Landscape/Landscape" },
            { LandscapeType.Ruin, "Images/Sprites/Landscape/Landscape" },
            { LandscapeType.Wildlessness, "Images/Sprites/Landscape/Landscape" },
            //ConstructionTypes
            { ConstructionType.City, "Images/Sprites/Construction/Construction" },
            //ChessTypes
            { ChessType.Infantry, "Images/Sprites/Chess/Chess" },
            { ChessType.AA_Infantry, "Images/Sprites/Chess/Chess" },
            { ChessType.Light, "Images/Sprites/Chess/Sand/Light" },
            { ChessType.Heavy, "Images/Sprites/Chess/Sand/Heavy" },
            { ChessType.Artillery, "Images/Sprites/Chess/Sand/Artillery" },
            { ChessType.Mortar, "Images/Sprites/Chess/Sand/Mortar" },
            { ChessType.Commander, "Images/Sprites/Chess/Chess" },
        };
}
