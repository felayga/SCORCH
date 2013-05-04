using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using utility.DataTypes;

namespace SCORCH
{
    public partial class Form1 : Form
    {
        private const uint landscapecolor = 2;

        landscape.generator what;

        Bitmap bitmap;
        public Form1()
        {
            InitializeComponent();
            this.pictureBox1.landscapecolor = landscapecolor;

            what = new landscape.midpointdisplacement(landscapecolor);
            this.button_newmap.Click += new EventHandler(button_newmap_Click);
            this.button_digger.Click += new EventHandler(button_digger_Click);
            this.button_square.Click += new EventHandler(button_square_Click);
            this.button_tracer.Click += new EventHandler(button_tracer_Click);
            this.button_lbomb.Click += new EventHandler(button_lbomb_Click);
            this.button_spread.Click += new EventHandler(button_spread_Click);

            bitmap = new Bitmap(36, 16);

            BitmapData data = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadWrite, PixelFormat.Format8bppIndexed);
            BitmapWrapper wrapper = new BitmapWrapper(data.Scan0, bitmap.Width, bitmap.Height, 1);

            uint value = 40;
            for (int y = 0; y < bitmap.Height; y++)
            {
                for (int x = 0; x < bitmap.Width; x++)
                {
                    wrapper.SetPixel(x, y, value++);
                }
            }

            bitmap.UnlockBits(data);

            this.pictureBox1.BackgroundImage = bitmap;

            seed = rand.Next();

            timer = new Timer();
            timer.Interval = 50;
            timer.Tick += new EventHandler(timer_Tick);
        }

        void button_spread_Click(object sender, EventArgs e)
        {
            this.pictureBox1.AddProjectile(new weapon.spread(typeof(weapon.square)), 20, bitmap.Height - 20, 10, 10, 0, -9.8);
        }

        void button_lbomb_Click(object sender, EventArgs e)
        {
            this.pictureBox1.AddProjectile(new weapon.lbomb(), 10, bitmap.Height - 10, 10, 0, 0, -9.8);
        }

        void button_digger_Click(object sender, EventArgs e)
        {
            this.pictureBox1.AddProjectile(new weapon.digger(typeof(weapon.square)), 10, bitmap.Height - 10, 10, 0, 0.0, -9.8);
        }

        void button_square_Click(object sender, EventArgs e)
        {
            this.pictureBox1.AddProjectile(new weapon.square(), 10, bitmap.Height - 10, 10, 0, 0.0, -9.8);
        }

        void button_tracer_Click(object sender, EventArgs e)
        {
            this.pictureBox1.AddProjectile(new weapon.tracer(), 10, bitmap.Height - 10, 10, 0, 0.0, -9.8);
        }

        Timer timer;
        void timer_Tick(object sender, EventArgs e)
        {
            timer.Stop();

            BitmapData data = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadWrite, PixelFormat.Format8bppIndexed);
            display.bitmap wrapper = new display.bitmap(data.Scan0, bitmap.Width, bitmap.Height);

            for (int n = 0; n < wrapper.Count; n++)
            {
                uint r,g,b;

                if (wrapper.GetPixel(n, out r, out g, out b))
                {
                    if (r > 0) r--;
                    if (g > 0) g--;
                    if (b > 0) b--;

                    if (r == 0 && g == 0 && b == 0) wrapper.SetPixel(n, 0);
                    else wrapper.SetPixel(n, r, g, b);
                }
            }

            this.pictureBox1.step(wrapper, (double)timer.Interval / 250.0);

            for (int n = 0; n < 3; n++)
            {
                for (int x = 0; x < wrapper.Width; x++)
                {
                    int ytake = -1;
                    for (int y = 0; y < wrapper.Height; y++)
                    {
                        if (ytake < 0)
                        {
                            if (wrapper.GetPixel(x, y) == landscapecolor) ytake = y;
                        }
                        else
                        {
                            if (wrapper.GetPixel(x, y) == 0)
                            {
                                wrapper.SetPixel(x, y, landscapecolor);
                                wrapper.SetPixel(x, ytake, 0);
                                ytake = -1;
                            }
                        }
                    }
                }
            }

            bitmap.UnlockBits(data);
            this.pictureBox1.Invalidate();

            timer.Start();
        }

        Random rand = new Random();
        int seed = 0;
        void button_newmap_Click(object sender, EventArgs e)
        {
            bitmap = new Bitmap(256, 256, PixelFormat.Format8bppIndexed);
            BitmapData data = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadWrite, PixelFormat.Format8bppIndexed);
            display.bitmap wrapper = new display.bitmap(data.Scan0, bitmap.Width, bitmap.Height);

            what.generate(wrapper, ref seed);

            bitmap.UnlockBits(data);

            this.pictureBox1.BackgroundImage = bitmap;
            this.pictureBox1.Refresh();

            timer.Start();
        }



        
/*
For each pixel on the screen do:
{
  x0 = scaled x coordinate of pixel (must be scaled to lie somewhere in the mandelbrot X scale (-2.5, 1)
  y0 = scaled y coordinate of pixel (must be scaled to lie somewhere in the mandelbrot Y scale (-1, 1)

  x = 0
  y = 0

  iteration = 0
  max_iteration = 1000

  while ( x*x + y*y < 2*2  AND  iteration < max_iteration )
  {
    xtemp = x*x - y*y + x0
    y = 2*x*y + y0

    x = xtemp

    iteration = iteration + 1
  }

  color = iteration

  plot(x0,y0,color)
}
*/
    }
}
