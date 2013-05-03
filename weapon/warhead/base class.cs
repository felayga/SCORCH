using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SCORCH.weapon
{
    public abstract class warhead
    {
        public abstract bool Explode(int xpoint, int ypoint, utility.DataTypes.BitmapWrapper wrapper);

        protected double trigger;
        public virtual double Trigger { set { trigger = value; } get { return trigger; } }

        public projectile Host;

        public virtual warhead Clone()
        {
            warhead retval = (warhead)(this.GetType().GetConstructor(new Type[] { }).Invoke(new object[] { }));

            Host.window.AddProjectile(retval,
                this.Host.x,
                this.Host.y,
                this.Host.vx,
                this.Host.vy,
                this.Host.ax,
                this.Host.ay
            );

            return retval;
        }
    }
}
