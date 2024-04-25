using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class Wildlessness:Landscape { 
    public Wildlessness()
    {
        this.LandscapeName = "荒地";
        this.LandscapeType = LandscapeType.Wildlessness;
    }

    public override void EffectChess(ref Chess stepchess)
    {
        
    }
}
