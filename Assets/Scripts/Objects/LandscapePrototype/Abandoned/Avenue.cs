/**using UnityEngine;

[System.Serializable]
public class Avenue : Landscape
{
    public Avenue(){
        this.LandscapeName = "Avenue";
//        this.LandscapeType = LandscapeType.Avenue;
    }

    public override void EffectStepChess(ref Chess stepchess)
    {
        stepchess.Movement += 1;
    }

    public override void EffectStepConstruction(ref Construction plantconstruction)
    {
        throw new System.NotImplementedException();
    }
}

**/
