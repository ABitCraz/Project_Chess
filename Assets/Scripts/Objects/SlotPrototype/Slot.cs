using UnityEngine;

[System.Serializable]
public class Slot
{
    public const string EmptySlotTagName = "EmptySlot";
    public const string LandscapeContainerTagName = "LandscapeContainer";
    public const string LandscapeTypeTagName = "LandscapeType";
    public Landscape Landscape;
    public Chess Chess;
    [HideInInspector]
    public Vector2 Position;

    public Slot()
    {
        this.Landscape = null;
        this.Chess = null;
    }

    public void InitializeLandscape(LandscapeType landscape)
    {
        switch (landscape)
        {
            case LandscapeType.Plain:
                this.Landscape = new Plain();
                break;
            case LandscapeType.Avenue:
                this.Landscape = new Avenue();
                break;
            case LandscapeType.Highland:
                this.Landscape = new Highland();
                break;
            case LandscapeType.Forest:
                this.Landscape = new Forest();
                break;
            case LandscapeType.River:
                this.Landscape = new River();
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
