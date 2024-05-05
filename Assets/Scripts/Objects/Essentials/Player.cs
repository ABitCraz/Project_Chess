using System;
using System.Collections.Generic;

[Serializable]
public class Player
{
    public int PlayerNo;
    public string PlayerName;
    public int Resource;
    public List<Chess> FactorChesses;
    public List<Construction> CapturedConstructions;
}
