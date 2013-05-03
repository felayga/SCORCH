using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using utility.DataTypes;

namespace SCORCH.landscape
{
    public class midpointdisplacement : generator
    {
        public midpointdisplacement(uint landscapecolor) : base(landscapecolor) { }

        BitmapWrapper wrapper;
        Random rand;
        public override void generate(display.bitmap bitmap, ref int seed)
        {
            rand = new Random(seed++);
            wrapper = bitmap;

            ymin = bitmap.Height / 8;
            ymax = bitmap.Height - ymin;

            bouncemin = 2;
            bouncemax = bitmap.Width / 8;

            variancereducer = 0.10 + 0.8 * rand.NextDouble();

            int variance = (int)Math.Round(bitmap.Height / (1.5 + 2.5 * rand.NextDouble()) * rand.NextDouble());
            int bounce = rand.Next(bouncemin, bouncemax);

            doop(0, (int)Math.Round(bitmap.Height / 2.0 * rand.NextDouble() + bitmap.Height / 4.0), bitmap.Width - 1, (int)Math.Round(bitmap.Height / 2.0 * rand.NextDouble() + bitmap.Height / 4.0), variance, bounce);
        }

        int ymin, ymax;
        int bouncemin, bouncemax;
        double variancereducer;

        private int boundvalue(int value, int min, int max)
        {
            if (value < min) value = min;
            if (value > max) value = max;
            return value;
        }

        private void doop(int xleft, int yleft, int xright, int yright, int variance, int bounce)
        {
            if (xright - xleft > bounce)
            {
                int xmid = (xright + xleft) / 2;
                int ymid = (int)Math.Round((double)(yright + yleft) / 2.0 + (double)variance * (2.0 * (rand.NextDouble() - 0.5)));

                if (ymid < 0) ymid = 0;
                if (ymid >= wrapper.Height) ymid = wrapper.Height - 1;

                fillcolumn(xleft, yleft, LandscapeColor);
                fillcolumn(xright, yright, LandscapeColor);

                int newvariance, newbounce;

                if (rand.Next(0, 2) == 0)
                {
                    newvariance = (int)Math.Round((double)variance * variancereducer);
                    newbounce = boundvalue(bounce + rand.Next(-bouncemax / 2, bouncemax / 2), bouncemin, bouncemax);
                    doop(xleft + 1, yleft, xmid, ymid, newvariance, newbounce);

                    newvariance = (int)Math.Round((double)variance * variancereducer);
                    newbounce = boundvalue(bounce + rand.Next(-bouncemax / 2, bouncemax / 2), bouncemin, bouncemax);
                    doop(xmid + 1, ymid, xright - 1, yright, newvariance, newbounce);
                }
                else
                {
                    newvariance = (int)Math.Round((double)variance * variancereducer);
                    newbounce = boundvalue(bounce + rand.Next(-bouncemax / 2, bouncemax / 2), bouncemin, bouncemax);
                    doop(xleft + 1, yleft, xmid - 1, ymid, newvariance, newbounce);

                    newvariance = (int)Math.Round((double)variance * variancereducer);
                    newbounce = boundvalue(bounce + rand.Next(-bouncemax / 2, bouncemax / 2), bouncemin, bouncemax);
                    doop(xmid, ymid, xright - 1, yright, newvariance, newbounce);
                }
            }
            else
            {
                fillcolumn(xleft, yleft, LandscapeColor);

                double ystep = ((double)yright - (double)yleft) / (double)(xright - xleft);
                double ymid = yleft + ystep;

                for (int xmid = xleft + 1; xmid < xright; xmid++)
                {
                    fillcolumn(xmid, (int)Math.Round(ymid), LandscapeColor);

                    ymid += ystep;
                }

                fillcolumn(xright, yright, LandscapeColor);
            }
        }

        private void fillcolumn(int x, int y, uint color)
        {
            if (y < 0) y = 0;
            if (y >= wrapper.Height) y = wrapper.Height - 1;
            for (int n = wrapper.Height - 1; n >= y; n--)
            {
                wrapper.SetPixel(x, n, color);
            }
        }
    }
}
