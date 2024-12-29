using System;
using System.Threading.Tasks;
using UnityEngine;

public class Slot
{
    public const string SlotTagName = "Slot";
    public const string LandscapeContainerTagName = "LandscapeContainer";
    public const string LandscapeTypeTagName = "LandscapeType";
    public Landscape Landscape { get; private set; }
    public Chess Chess { get; private set; }
    public Construction Construction { get; private set; }
    public Vector2Int Position;
    public Vector3 FactPosition;
    public GameObject SlotGameObject;

    public Slot()
    {
        this.Landscape = new Landscape() { LandscapeType = LandscapeType.Empty };
        this.Construction = new Construction() { ConstructionType = ConstructionType.Empty };
        this.Chess = new Chess() { ChessType = ChessType.Empty };
    }

    public void InitializeOrSwapLandscape(LandscapeType target_landscape)
    {
        switch (target_landscape)
        {
            case LandscapeType.Empty:
                this.Landscape = new Landscape() { LandscapeType = LandscapeType.Empty };
                break;
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
        }
        this.Landscape.LoadSpriteAndAnimation();
    }

    public void InitializeOrSwapConstruction(ConstructionType target_construction)
    {
        switch (target_construction)
        {
            case ConstructionType.Empty:
                this.Construction = new Construction()
                {
                    ConstructionType = ConstructionType.Empty,
                };
                break;
            case ConstructionType.City:
                this.Construction = new City();
                break;
            default:
                break;
        }
        this.Construction.LoadSpriteAndAnimation();
    }

    public void InitializeOrSwapChess(ChessType target_chess)
    {
        switch (target_chess)
        {
            case ChessType.Empty:
                this.Chess = new Chess() { ChessType = ChessType.Empty };
                break;
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
                break;
        }
        this.Chess.LoadSpriteAndAnimation();
    }

    public void GenerateCurrentLandscapeGameObject()
    {
        if (Landscape.UnitGameObject != null)
            MonoBehaviour.Destroy(Landscape.UnitGameObject);
        GameObject new_landscape = MonoBehaviour.Instantiate(
            EssentialDatumLoader.GameObjectDictionary[Prefab.Landscape]
        );
        SlotLoader.LoadGameObject(ref new_landscape);
    }

    public void GenerateCurrentConstructionGameObject()
    {
        if (Construction.UnitGameObject != null)
            MonoBehaviour.Destroy(Construction.UnitGameObject);
        GameObject new_construction = MonoBehaviour.Instantiate(
            EssentialDatumLoader.GameObjectDictionary[Prefab.Construction]
        );
        SlotLoader.LoadGameObject(ref new_construction);
    }

    public void GenerateCurrentChessGameObject()
    {
        if (Chess.UnitGameObject != null)
            MonoBehaviour.Destroy(Chess.UnitGameObject);
        GameObject new_chess = MonoBehaviour.Instantiate(
            EssentialDatumLoader.GameObjectDictionary[Prefab.Chess]
        );
        SlotLoader.LoadGameObject(ref new_chess);
    }

    public void ChessMoveIn(Slot origin_slot, Chess target_chess)
    {
        origin_slot.Chess = new Chess() { ChessType = ChessType.Empty };
        this.Chess = target_chess;
        target_chess.TheSlotStepOn = this;
    }

    public void ChessMoveOut(Slot target_slot, Chess target_chess)
    {
        target_slot.Chess = target_chess;
        this.Chess = new Chess() { ChessType = ChessType.Empty };
        target_chess.TheSlotStepOn = target_slot;
    }

    public SerializableSlot NormalSlotSwapToSerializableSlot()
    {
        return new SerializableSlot(this);
    }
}
