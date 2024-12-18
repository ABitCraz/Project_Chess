using System;
using UnityEngine;

public class Slot
{
    public const string SlotTagName = "Slot";
    public const string LandscapeContainerTagName = "LandscapeContainer";
    public const string LandscapeTypeTagName = "LandscapeType";
    public Landscape Landscape;
    public Chess Chess;
    public Construction Construction;
    public Vector2Int Position;
    public Vector3 FactPosition;
    public GameObject SlotGameObject;

    public Slot()
    {
        this.Landscape = null;
        this.Construction = null;
        this.Chess = null;
    }

    public Slot(LandscapeType new_landscape)
    {
        InitializeOrSwapLandscape(new_landscape);
    }

    public Slot(LandscapeType new_landscape, ConstructionType new_construction)
    {
        InitializeOrSwapLandscape(new_landscape);
        InitializeOrSwapConstruction(new_construction);
    }

    public Slot(LandscapeType new_landscape, ChessType new_chess)
    {
        InitializeOrSwapLandscape(new_landscape);
        InitializeOrSwapChess(new_chess);
    }

    public Slot(LandscapeType new_landscape, ConstructionType new_construction, ChessType new_chess)
    {
        InitializeOrSwapLandscape(new_landscape);
        InitializeOrSwapConstruction(new_construction);
        InitializeOrSwapChess(new_chess);
    }

    public void InitializeOrSwapLandscape(LandscapeType landscape)
    {
        GameObject LastGameObject = null;
        if (this.Landscape != null)
        {
            LastGameObject = this.Landscape.UnitGameObject;
        }
        switch (landscape)
        {
            case LandscapeType.Wilderness:
                this.Landscape = new Wilderness();
                break;
            case LandscapeType.HighGround:
                this.Landscape = new HighGround();
                break;
            case LandscapeType.Ancient:
                this.Landscape = new Ancient();
                break;
            case LandscapeType.Ruin:
                this.Landscape = new Ruin();
                break;
            case LandscapeType.Desert:
                this.Landscape = new Desert();
                break;
            case LandscapeType.Canyon:
                this.Landscape = new Canyon();
                break;
            default:
                if ((this.Landscape != null) && (this.Landscape.UnitGameObject != null))
                {
                    MonoBehaviour.Destroy(this.Landscape.UnitGameObject);
                }
                this.Landscape = null;
                break;
        }
        if (this.Landscape != null)
        {
            this.Landscape.UnitGameObject = LastGameObject;
        }
    }

    public void InitializeOrSwapConstruction(ConstructionType construction)
    {
        GameObject LastGameObject = null;
        if (this.Construction != null)
        {
            LastGameObject = this.Construction.UnitGameObject;
        }
        switch (construction)
        {
            case ConstructionType.City:
                this.Construction = new City();
                break;
            default:
                if ((this.Construction != null) && (this.Construction.UnitGameObject != null))
                {
                    MonoBehaviour.Destroy(this.Construction.UnitGameObject);
                }
                this.Construction = null;
                break;
        }
        if (this.Construction != null)
        {
            this.Construction.UnitGameObject = LastGameObject;
        }
    }

    public void InitializeOrSwapChess(ChessType chess)
    {
        GameObject LastGameObject = null;
        if (this.Chess != null)
        {
            LastGameObject = this.Chess.UnitGameObject;
        }
        switch (chess)
        {
            case ChessType.Infantry:
                this.Chess = new Infantry();
                break;
            case ChessType.AA_Infantry:
                this.Chess = new AA_Infantry();
                break;
            case ChessType.Light:
                this.Chess = new Light();
                break;
            case ChessType.Heavy:
                this.Chess = new Heavy();
                break;
            case ChessType.Mortar:
                this.Chess = new Mortar();
                break;
            case ChessType.Artillery:
                this.Chess = new Artillery();
                break;
            case ChessType.Commander:
                this.Chess = new Commander();
                break;
            default:
                if ((this.Chess != null) && (this.Chess.UnitGameObject != null))
                {
                    MonoBehaviour.Destroy(this.Chess.UnitGameObject);
                }
                this.Chess = null;
                break;
        }
        if (this.Chess != null)
        {
            this.Chess.UnitGameObject = LastGameObject;
        }
    }

    public SerializableSlot NormalSlotSwapToSerializableSlot()
    {
        return new SerializableSlot(this);
    }
}
