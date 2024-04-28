using Palmmedia.ReportGenerator.Core.Reporting.Builders;
using UnityEngine;

[System.Serializable]
public class Slot
{
    public const string SlotTagName = "Slot";
    public const string LandscapeContainerTagName = "LandscapeContainer";
    public const string LandscapeTypeTagName = "LandscapeType";
    public Landscape Landscape;
    public Chess Chess;
    public Construction Construction;

    [HideInInspector]
    public Vector2 Position;

    public Slot()
    {
        this.Landscape = null;
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
    }

    public void InitializeOrSwapConstruction(ConstructionType construction)
    {
        switch (construction)
        {
            case ConstructionType.City:
                this.Construction = new City();
                break;
        }
    }

    public void InitializeOrSwapChess(ChessType chess)
    {
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
    }

    public void ChessEnterSlot(ref Chess passchess)
    {
        this.Chess = passchess;
        passchess.StepLandscape = this.Landscape;
        passchess.StepSlot = this;
    }

    public void ChessQuitSlot()
    {
        this.Chess.StepLandscape = null;
        this.Chess.StepSlot = null;
        this.Chess = null;
    }
}
