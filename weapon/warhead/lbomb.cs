using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SCORCH.weapon
{
    public class lbomb : warhead
    {
        public lbomb()
        {
            trigger = 40.0;
        }

        int count = 10;
        Random rand = new Random();
        public override bool Explode(int xpoint, int ypoint, utility.DataTypes.BitmapWrapper wrapper)
        {
            //Host.window.landscapecolor

            //if (rand.Next(0, 10) == 0) return true;
            for (count=10; count >= -1; count--)
            {
                for (int y = -1; y <= 2; y++)
                {            
                        if (y + ypoint < 0 || y + ypoint >= wrapper.Height) continue;

                        for (int x = -1; x <= 2; x++)
                        {
                            if (x + xpoint < 0 || x + xpoint >= wrapper.Width) continue;
                            
                            wrapper.SetPixel(x + xpoint, y + ypoint, 220);
                            int move = rand.Next(-10, 10) * 2;
                            xpoint += move;
                            ypoint += move;
                        }
                        Projectile.vx += rand.Next(-1, 1) * 1.0;
                        Projectile.vy += rand.Next(-1, 1) * 1.0;               
                }
            }
            return count<0;
            //wrapper.GetPixel(x, y
        }
    }
}
