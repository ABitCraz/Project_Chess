using UnityEngine;

public class GameMap
{
    public Vector2Int Size;
    public int Weather = -1;

    public GameMap(int sizex,int sizey)
    {
        this.Size.x = sizex;
        this.Size.y = sizey;
    }
}
