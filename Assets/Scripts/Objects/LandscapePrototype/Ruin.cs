using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class Ruin : Landscape
{
    public Ruin()
    {
        this.LandscapeName = "废墟";
        this.LandscapeType = LandscapeType.Ruin;
    }

    public override void EffectChess(ref Chess stepchess) { }
}
