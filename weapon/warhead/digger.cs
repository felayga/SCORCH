using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SCORCH.weapon
{
    public class digger : warhead
    {
        Type subwarhead;
        public digger(Type subwarhead)
        {
            trigger = 40.0;
            this.subwarhead = subwarhead;
        }

        private Random rand = new Random();

        int charges = 10;
        public override bool Explode(int xpoint, int ypoint, utility.DataTypes.BitmapWrapper wrapper)
        {
            charges--;
            trigger = rand.NextDouble() * 20.0 + 20.0;

            if (rand.Next(0, 2) == 0) Host.vx += 30.0;
            else Host.vx -= 30.0;
            Host.vy = 0.0;

            warhead explosion = (warhead)(subwarhead.GetConstructor(new Type[] { }).Invoke(new object[] { }));
            explosion.Trigger = 0.0;
            this.Host.window.AddProjectile(explosion, Host.x, Host.y, -Host.vx, Host.vy, Host.ax, Host.ay);

            if (charges > 0 && rand.Next(0,3)==0)
            {
                digger clone = (digger)this.Clone();
                clone.charges = rand.Next(0, charges);
                charges -= clone.charges;
                this.Host.window.AddProjectile(clone, Host.x, Host.y, -Host.vx, Host.vy, Host.ax, Host.ay);
            }

            return charges < 1;
        }

        public override warhead Clone()
        {
            return new digger(subwarhead);
        }
    }
}
