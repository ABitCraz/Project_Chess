using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class Desert : Landscape
{
    public Desert()
    {
        this.LandscapeName = "沙漠";
        this.LandscapeType = LandscapeType.Desert;
    }

    public override void EffectChess(ref Chess stepchess) { }
}
