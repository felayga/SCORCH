using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using utility.DataTypes;

namespace SCORCH.weapon
{
    public class projectile
    {
        protected internal display.window window;
        warhead warhead;
        private readonly uint landscapecolor;
        protected internal double x, y, vx, vy, ax, ay;
        public BitmapWrapper wrapper;

        public projectile(display.window window, weapon.warhead warhead, uint landscapecolor, double x, double y, double vx, double vy, double ax, double ay)
        {
            this.window = window;

            this.warhead = warhead;
            warhead.Projectile = this;

            this.landscapecolor = landscapecolor;
            this.x = x;
            this.y = y;
            this.vx = vx;
            this.vy = vy;
            this.ax = ax;
            this.ay = ay;
        }

        ~projectile()
        {
            window = null;
            warhead = null;
            wrapper = null;
        }

        protected internal bool hasspread = false;

        public void update(double secondfraction)
        {
            if (x < 0 || x >= window.Width || y < 0 || y >= window.Height) remove = true;
            if (remove) return;

            try
            {
                int oldx = (int)Math.Round(x);
                int oldy = wrapper.Height - 1 - (int)Math.Round(y);

                vx += ax * secondfraction;
                vy += ay * secondfraction;

                x += vx * secondfraction;
                y += vy * secondfraction;

                int newx = (int)Math.Round(x);
                int newy = wrapper.Height - 1 - (int)Math.Round(y);

                if (this.x == newx && this.y == newy) return;

                double xstep = (double)newx - (double)oldx;
                double ystep = (double)newy - (double)oldy;

                if (!hasspread && vy < 0.0)
                {
                    Random rand = new Random();
                    hasspread = true;
                    warhead[] spread = warhead.Spread();
                    if (spread != null)
                    {
                        for (int n = 0; n < spread.Length; n++)
                        {
                            window.AddProjectile(spread[n], true, x, y, vx + (rand.NextDouble() - 0.5)*10.0, vy + (rand.NextDouble() - 0.5)*10.0, ax, ay);
                        }
                    }
                }

                if (Math.Abs(xstep) < Math.Abs(ystep))
                {
                    xstep /= Math.Abs(ystep);

                    int step, endpos;
                    if (oldy < newy)
                    {
                        step = 1;
                        endpos = newy + 1;
                    }
                    else
                    {
                        step = -1;
                        endpos = newy - 1;
                    }

                    double xpos = oldx;
                    for (int ypos = oldy; ypos != endpos; ypos += step, xpos += xstep)
                    {
                        if (impacttest((int)Math.Round(xpos), ypos, wrapper)) return;
                    }
                }
                else
                {
                    ystep /= Math.Abs(xstep);

                    int step, endpos;
                    if (oldx < newx)
                    {
                        step = 1;
                        endpos = newx + 1;
                    }
                    else
                    {
                        step = -1;
                        endpos = newx - 1;
                    }

                    double ypos = oldy;
                    for (int xpos = oldx; xpos != endpos; xpos += step, ypos += ystep)
                    {
                        if (impacttest(xpos, (int)Math.Round(ypos), wrapper)) return;
                    }
                }
            }
            catch (Exception)
            {
                remove = true;
            }
        }

        private bool remove;
        public bool Remove { get { return remove; } }

        public void Draw(Graphics g, int windowx, int windowy, int landscapex, int landscapey)
        {
            g.DrawEllipse(Pens.Red,
                new Rectangle(
                    (int)Math.Round((double)x / (double)landscapex * (double)windowx),
                    windowy - (int)Math.Round((double)y / (double)landscapey * (double)windowy),
                    3,
                    3
                )
            );
        }

        private bool impacttest(int x, int y, BitmapWrapper wrapper)
        {
            if (x <= 0 || y <= 0 || x >= wrapper.Width - 1 || y >= wrapper.Height - 1)
            {
                remove = warhead.Explode(x, y, wrapper);
                return remove;
            }

            uint pixel = wrapper.GetPixel(x, y);
            wrapper.SetPixel(x, y, 255);
            //Console.WriteLine("x=" + x + " y=" + y);
            if (pixel != landscapecolor) return false;

            _impacttest(ref vx);
            _impacttest(ref vy);

            if (warhead.Trigger < 0.0)
            {
                remove = warhead.Explode(x, y, wrapper);
                return remove;
            }
            return false;
        }

        private void _impacttest(ref double what)
        {
            if (what > 0)
            {
                if (what > 4.0)
                {
                    warhead.Trigger -= 4.0;
                    what -= 4.0;
                }
                else
                {
                    warhead.Trigger -= what;
                    what = 0;
                }
            }
            else
            {
                if (what < -4.0)
                {
                    warhead.Trigger -= 4.0;
                    what += 4.0;
                }
                else
                {
                    warhead.Trigger += what;
                    what = 0;
                }
            }
        }
    }
}
