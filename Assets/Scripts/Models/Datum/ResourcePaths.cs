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
            { Prefab.Chess, "Prefabs/Basics/Chess" },
        };
    public static readonly Dictionary<UI, string> UIPrefabs =
        new() { { UI.SlotDropDown, "Prefabs/UIs/SlotDropDown" } };
    public static readonly Dictionary<Enum, string> TargetSprites =
        new()
        {
            //LandscapeTypes
            { LandscapeType.Ancient, "Images/Sprites/Landscape/L_Ancient" },
            { LandscapeType.Canyon, "Images/Sprites/Landscape/L_Canyon" },
            { LandscapeType.Desert, "Images/Sprites/Landscape/L_Desert" },
            { LandscapeType.HighGround, "Images/Sprites/Landscape/L_HighGround" },
            { LandscapeType.Ruin, "Images/Sprites/Landscape/L_Ruin" },
            { LandscapeType.Wilderness, "Images/Sprites/Landscape/L_Wilderness" },
            //ConstructionTypes
            { ConstructionType.City, "Images/Sprites/Construction/L_City" },
            //ChessTypes
            { ChessType.Infantry, "Images/Sprites/Chess/Soldier/S_Soldier" },
            { ChessType.AA_Infantry, "Images/Sprites/Chess/AntiArmorSoldier/S_AntiArmorSoldier" },
            { ChessType.Light, "Images/Sprites/Chess/LightTank/S_LightTank" },
            { ChessType.Heavy, "Images/Sprites/Chess/HeavyTank/S_HeavyTank" },
            { ChessType.Artillery, "Images/Sprites/Chess/Artillery/S_Artillery" },
            { ChessType.Mortar, "Images/Sprites/Chess/Mortar/S_Mortar" },
            { ChessType.Commander, "Images/Sprites/Chess/Commander/S_General" },
            { ActionType.Attack, "Images/UIs/ORDERattack" },
            { ActionType.Move, "Images/UIs/ORDERmove" },
            { ActionType.Alert, "Images/UIs/ORDEROverWatch" },
            { ActionType.Push, "Images/UIs/ORDERpush" },
            { ActionType.Repair, "Images/UIs/ORDERrepair" },
            { ActionType.Hold, "Images/UIs/ORDERrepair" },
            { ActionType.Reinforce, "Images/UIs/ORDERrepair" },
            { ActionType.Drop, "Images/UIs/ORDERrepair" },
        };
    public static readonly Dictionary<Enum, string> TargetAnimators =
        new()
        {
            //LandscapeTypes
            { LandscapeType.Ancient, "Images/Sprites/Landscape/L_Ancient" },
            { LandscapeType.Canyon, "Images/Sprites/Landscape/L_Canyon" },
            { LandscapeType.Desert, "Images/Sprites/Landscape/L_Desert" },
            { LandscapeType.HighGround, "Images/Sprites/Landscape/L_HighGround" },
            { LandscapeType.Ruin, "Images/Sprites/Landscape/L_Ruin" },
            { LandscapeType.Wilderness, "Images/Sprites/Landscape/L_Wilderness" },
            //ConstructionTypes
            { ConstructionType.City, "Images/Sprites/Construction/L_City" },
            //ChessTypes
            { ChessType.Infantry, "Animations/Controllers/AC_Soldier" },
            { ChessType.AA_Infantry, "Animations/Controllers/AC_AntiArmorSoldier" },
            { ChessType.Light, "Animations/Controllers/AC_LightTank" },
            { ChessType.Heavy, "Animations/Controllers/AC_HeavyTank" },
            { ChessType.Artillery, "Animations/Controllers/AC_Artillery" },
            { ChessType.Mortar, "Animations/Controllers/AC_Mortar" },
            { ChessType.Commander, "Animations/Controllers/AC_General" },
        };
}
