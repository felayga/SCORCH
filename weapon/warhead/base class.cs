using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SCORCH.weapon
{
    public abstract class warhead
    {
        /// <summary>
        /// Function which is called when the warhead has started to fall downward.
        /// Should be used for warheads which spread.
        /// </summary>
        /// <returns>The warheads created as a result of the spreading action.</returns>
        public virtual warhead[] Spread()
        {
            return null;
        }

        /// <summary>
        /// Function which is called when the trigger has fallen below 0.0, which usually implies an impact.
        /// </summary>
        /// <param name="xpoint">The current x position of the projectile.</param>
        /// <param name="ypoint">The current y position of the projectile.</param>
        /// <param name="wrapper">The landscape in which the explosion was triggered.</param>
        /// <returns>True if the bomb has actually exploded, and should be removed from the playing field.</returns>
        public abstract bool Explode(int xpoint, int ypoint, utility.DataTypes.BitmapWrapper wrapper);


        protected double trigger;
        public virtual double Trigger { set { trigger = value; } get { return trigger; } }

        /// <summary>
        /// The projectile which is hosting the warhead.
        /// </summary>
        protected internal projectile Projectile;

        ~warhead()
        {
            Projectile = null;
        }

        public virtual warhead Clone()
        {
            warhead retval = (warhead)(this.GetType().GetConstructor(new Type[] { }).Invoke(new object[] { }));

            Projectile.window.AddProjectile(retval,
                this.Projectile.x,
                this.Projectile.y,
                this.Projectile.vx,
                this.Projectile.vy,
                this.Projectile.ax,
                this.Projectile.ay
            );

            return retval;
        }
    }
}
