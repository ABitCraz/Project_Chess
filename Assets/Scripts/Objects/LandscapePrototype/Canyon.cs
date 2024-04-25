using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class Canyon : Landscape
{
    public Canyon()
    {
        this.LandscapeName = "峡谷";
        this.LandscapeType = LandscapeType.Canyon;
    }

    public override void EffectChess(ref Chess stepchess) { }
}
