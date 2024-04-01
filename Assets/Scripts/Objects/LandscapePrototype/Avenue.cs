using UnityEngine;

[System.Serializable]
public class Avenue : Landscape
{
    public Avenue(){
        this.LandscapeName = "Avenue";
        this.LandscapeType = LandscapeType.Avenue;
    }

    public override void EffectChess(ref Chess stepchess)
    {
        stepchess.Movement += 1;
    }
}
