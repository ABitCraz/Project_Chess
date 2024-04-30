using System;
using UnityEngine;

[Serializable]
public class Slot
{
    public const string SlotTagName = "Slot";
    public const string LandscapeContainerTagName = "LandscapeContainer";
    public const string LandscapeTypeTagName = "LandscapeType";
    public Landscape Landscape;
    public Chess Chess;
    public Construction Construction;
    public Vector2 Position;
    public Vector3 FactPosition;

    public Slot()
    {
        this.Landscape = null;
        this.Construction = null;
        this.Chess = null;
    }

    public Slot(LandscapeType newlandscape)
    {
        InitializeOrSwapLandscape(newlandscape);
    }

    public Slot(LandscapeType newlandscape, ConstructionType newconstruction)
    {
        InitializeOrSwapLandscape(newlandscape);
        InitializeOrSwapConstruction(newconstruction);
    }

    public Slot(LandscapeType newlandscape, ChessType newchess)
    {
        InitializeOrSwapLandscape(newlandscape);
        InitializeOrSwapChess(newchess);
    }

    public Slot(LandscapeType newlandscape, ConstructionType newconstruction, ChessType newchess)
    {
        InitializeOrSwapLandscape(newlandscape);
        InitializeOrSwapConstruction(newconstruction);
        InitializeOrSwapChess(newchess);
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
            case LandscapeType.Wildlessness:
                this.Landscape = new Wildlessness();
                break;
            case LandscapeType.Highground:
                this.Landscape = new Highground();
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
        this.Landscape.UnitGameObject = LastGameObject;
    }

    public void InitializeOrSwapConstruction(ConstructionType construction)
    {
        GameObject LastGameObject = null;
        if(this.Construction != null)
        {
            LastGameObject = this.Construction.UnitGameObject;
        }
        switch (construction)
        {
            case ConstructionType.City:
                this.Construction = new City();
                break;
        }
        this.Construction.UnitGameObject = LastGameObject;
    }

    public void InitializeOrSwapChess(ChessType chess)
    {
        GameObject LastGameObject =null;
        if(this.Chess != null)
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
        }
        this.Chess.UnitGameObject = LastGameObject;
    }

    public void ChessEnterSlot(ref Chess passchess)
    {
        this.Chess = passchess;
    }

    public void ChessQuitSlot()
    {
        this.Chess = null;
    }
}
