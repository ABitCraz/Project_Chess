public class City : Construction
{
    public float CaptureCapacity = 20f;
    public int CaptureState = 0;

    //0-Captured;1-Capturing;
    public City()
    {
        this.ConstructionName = "城市";
        this.ConstructionType = ConstructionType.City;
    }

    public string GetCaptureState()
    {
        return CaptureState switch
        {
            -1 => "无人占领",
            0 => "{captureFactor}占领中",
            1 => "{captureFactor}已占领",
            _ => "",
        };
    }

    public void CityCapturing(Player captured_player)
    {
        captured_player.Resource += 100;
    }
}
