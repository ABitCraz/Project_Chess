using System;
using System.Collections.Generic;

[Serializable]
public class Player
{
    public int PlayerNo;
    public string PlayerName;
    public int Resource;
    public Dictionary<ChessType,int> ChessCostDictionary = new(){
        {ChessType.Infantry,3},
        {ChessType.AA_Infantry,4},
        {ChessType.Light,6},
        {ChessType.Heavy,10},
        {ChessType.Mortar,7},
        {ChessType.Artillery,11}
    };
    public List<Chess> FactorChesses;
    public List<Construction> CapturedConstructions;
}
