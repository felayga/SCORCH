using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SCORCH.weapon
{
    public class tracer : warhead
    {
        public tracer()
        {
            trigger = 0.0;
        }

        public override bool Explode(int xpoint, int ypoint, utility.DataTypes.BitmapWrapper wrapper)
        {
            //this.Host.window.landscapecolor



            return true;
        }
    }
}
