public class Canyon : Landscape
{
    public bool IsPassable = false;
    public Canyon()
    {
        this.LandscapeName = "峡谷";
        this.LandscapeType = LandscapeType.Canyon;
    }
}
