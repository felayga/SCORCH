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

            if (rand.Next(0, 2) == 0) Projectile.vx += 30.0;
            else Projectile.vx -= 30.0;
            Projectile.vy = 0.0;

            warhead explosion = (warhead)(subwarhead.GetConstructor(new Type[] { }).Invoke(new object[] { }));
            explosion.Trigger = 0.0;
            this.Projectile.window.AddProjectile(explosion, Projectile.x, Projectile.y, -Projectile.vx, Projectile.vy, Projectile.ax, Projectile.ay);

            if (charges > 0 && rand.Next(0,3)==0)
            {
                digger clone = (digger)this.Clone();
                clone.charges = rand.Next(0, charges);
                charges -= clone.charges;
                this.Projectile.window.AddProjectile(clone, Projectile.x, Projectile.y, -Projectile.vx, Projectile.vy, Projectile.ax, Projectile.ay);
            }

            return charges < 1;
        }

        public override warhead Clone()
        {
            return new digger(subwarhead);
        }
    }
}
