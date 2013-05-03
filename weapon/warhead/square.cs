using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SCORCH.weapon
{
    public class square : warhead
    {
        public square()
        {
            trigger = 40.0;
        }

        public override bool Explode(int xpoint, int ypoint, utility.DataTypes.BitmapWrapper wrapper)
        {
            for (int y = -2; y <= 2; y++)
            {
                if (y + ypoint < 0 || y + ypoint >= wrapper.Height) continue;
                for (int x = -2; x <= 2; x++)
                {
                    if (x + xpoint < 0 || x + xpoint >= wrapper.Width) continue;

                    wrapper.SetPixel(x + xpoint, y + ypoint, 220);
                }
            }

            return true;
        }
    }
}
