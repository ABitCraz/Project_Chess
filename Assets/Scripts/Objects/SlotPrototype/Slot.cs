using UnityEngine;

[System.Serializable]
public class Slot
{
    public Landscape Landscape;
    public Chess Chess;
    [HideInInspector]
    public Vector2 Position;

    public Slot()
    {
        this.Landscape = null;
        this.Chess = null;
    }

    public Slot(ref Landscape plotlandscape,ref Chess plotchess)
    {
        this.Landscape = plotlandscape;
        this.Chess = plotchess;
    }

    public void ChessQuitSlot()
    {
        this.Chess = null;
    }
}
