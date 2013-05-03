using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using utility.DataTypes;

namespace SCORCH.landscape
{
    public abstract class generator
    {
        public readonly uint LandscapeColor;
        public generator(uint landscapecolor)
        {
            LandscapeColor = landscapecolor;
        }

        public abstract void generate(display.bitmap bitmap, ref int seed);
    }
}
