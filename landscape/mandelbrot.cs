using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using utility.DataTypes;

namespace SCORCH.landscape
{
    public class mandelbrot : generator
    {
        public mandelbrot(uint landscapecolor) : base(landscapecolor) { }

        private BitmapWrapper wrapper;
        public override void generate(display.bitmap bitmap, ref int seed)
        {
            wrapper = bitmap;
            seed_mandelbrot = seed;

            landscape();
            seed = seed_mandelbrot;
        }
        
        Random rand_mandelbrot;
        int seed_mandelbrot = 0;
        private void rerand(bool newseed, out decimal xwidth, out decimal xmin, out decimal y, out decimal xstep, out decimal ystep)
        {
            Console.WriteLine("rerand seed=" + seed_mandelbrot);
            if (newseed) rand_mandelbrot = new Random(seed_mandelbrot++);

            xwidth = (decimal)rand_mandelbrot.NextDouble() * 3.5m;

            xmin = (decimal)rand_mandelbrot.NextDouble() * (3.5m - xwidth) - 1m;
            y = (decimal)rand_mandelbrot.NextDouble() * 2m - 1m;

            xstep = xwidth / (decimal)wrapper.Width;
            ystep = xwidth;
        }

        private void landscape()
        {
            MandelDot[] generator = new MandelDot[wrapper.Width];

            decimal xwidth, xmin, y, xstep, ystep;
            rerand(true, out xwidth, out xmin, out y, out xstep, out ystep);


            bool good = false;
            int generatormax = wrapper.Height;
            int min = int.MaxValue, max = int.MinValue;
            while (!good)
            {

                min = int.MaxValue;
                max = int.MinValue;

                decimal x = xmin;
                bool stagnated = true;
                for (int n = 0; n < generator.Length; n++)
                {
                    generator[n] = new MandelDot(x, y, xstep, ystep);
                    generator[n].Iterate(generatormax);
                    if (!generator[n].Done) stagnated = false;

                    if (min > generator[n].Iteration) min = generator[n].Iteration;
                    if (max < generator[n].Iteration) max = generator[n].Iteration;

                    x += xmin;
                }

                while (!good)
                {
                    generatormax += wrapper.Height / 4;

                    x = xmin;
                    stagnated = true;
                    for (int n = 0; n < generator.Length; n++)
                    {
                        generator[n].Iterate(generatormax);
                        if (!generator[n].Done) stagnated = false;

                        if (min > generator[n].Iteration) min = generator[n].Iteration;
                        if (max < generator[n].Iteration) max = generator[n].Iteration;

                        x += xmin;
                    }

                    int mins = 0;
                    int maxes = 0;
                    double left = 0.0;
                    double right = 0.0;
                    for (int n = 0; n < generator.Length; n++)
                    {
                        left += (double)generator[n].Iteration / (double)(generator.Length - 1 - n);
                        right += (double)generator[n].Iteration / (double)(generator.Length - 1);

                        if (generator[n].Iteration == min) mins++;
                        else if (generator[n].Iteration == max) maxes++;
                    }


                    if (max - min > generator.Length && maxes < generator.Length / 2 && mins < generator.Length / 2)
                    {
                        good = true;
                    }
                    else if (stagnated || generatormax > wrapper.Height * 16)
                    {
                        if (mins == generator.Length || maxes == generator.Length)
                        {
                            rerand(true, out xwidth, out xmin, out y, out xstep, out ystep);
                        }
                        else
                        {
                            if (left == 0 && right == 0)
                            {
                                rerand(true, out xwidth, out xmin, out y, out xstep, out ystep);
                            }
                            else if (left > right)
                            {
                                xmin -= xwidth;
                                if (xmin < -1.0m) rerand(true, out xwidth, out xmin, out y, out xstep, out ystep);
                            }
                            else
                            {
                                xmin += xwidth;
                                if (xmin + xwidth > 2.5m) rerand(true, out xwidth, out xmin, out y, out xstep, out ystep);
                            }
                        }

                        break;
                    }

                }
            }

            for (int x = 0; x < wrapper.Width; x++)
            {
                int ymax = (int)Math.Round(((double)generator[x].Iteration / (double)(max - min) * (double)(wrapper.Height - 1) + (double)min));
                ymax += wrapper.Height / 3;
                if (ymax >= wrapper.Height) ymax = wrapper.Height - 1;
                for (int _y = wrapper.Height - 1; _y >= ymax; _y--)
                {
                    wrapper.SetPixel(x, _y, LandscapeColor);
                }
            }

            wrapper = null;
        }
    }

    public class MandelDot
    {
        private decimal x, y, xstep, ystep;
        private decimal xsquared, ysquared;

        private int iteration;
        public int Iteration { get { return iteration; } }

        private bool done;
        public bool Done { get { return done; } }

        public MandelDot(decimal x, decimal y, decimal xstep, decimal ystep)
        {
            this.x = x;
            this.y = y;
            this.xstep = xstep;
            this.ystep = ystep;
            this.xsquared = x * x;
            this.ysquared = y * y;

            iteration = 0;
        }

        public void Iterate(int maxvalue)
        {
            if (done) return;

            while (xsquared + ysquared < 4m)
            {
                if (iteration >= maxvalue) return;

                decimal xtemp = xsquared - ysquared + xstep;
                y = 2m * x * y + ystep;
                x = xtemp;

                iteration++;

                xsquared = x * x;
                ysquared = y * y;
            }

            done = true;
        }

        public override string ToString()
        {
            return "MandelDot{iteration=" + iteration + "}";
        }
    }
}
