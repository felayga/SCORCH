using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.ComponentModel;
using utility.DataTypes;

namespace SCORCH.display
{
    public class window : utility.Controls.PictureBoxInterpolationMode
    {
        [DefaultValue(typeof(InterpolationMode), "NearestNeighbor")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override InterpolationMode InterpolationMode { set { base.InterpolationMode = value; } get { return base.InterpolationMode; } }

        [DefaultValue(typeof(PixelOffsetMode), "HighQuality")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override PixelOffsetMode PixelOffsetMode { set { base.PixelOffsetMode = value; } get { return base.PixelOffsetMode; } }

        public uint landscapecolor;

        public window()
        {
            base.InterpolationMode = InterpolationMode.NearestNeighbor;
            base.PixelOffsetMode = PixelOffsetMode.HighQuality;
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);

            if (this.BackgroundImage == null) return;

            if (this.BackgroundImage.Width < this.Width || this.BackgroundImage.Height < this.Height)
            {
                base.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Bicubic;
            }
            else
            {
                base.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            }
        }

        public void step(BitmapWrapper wrapper, double secondfraction)
        {
            foreach (weapon.projectile p in projectiles.ToArray())
            {
                p.wrapper = wrapper;

                p.update(secondfraction);
            }
        }


        HashSet<weapon.projectile> projectiles = new HashSet<weapon.projectile>();
        public void AddProjectile(weapon.warhead warhead, double x, double y, double vx, double vy, double ax, double ay)
        {
            weapon.projectile p = new weapon.projectile(this, warhead, landscapecolor, x, y, vx, vy, ax, ay);
            projectiles.Add(p);
            this.Invalidate(new Rectangle((int)Math.Round(x) - 2, (int)Math.Round(y) - 2, 5, 5));
        }

        protected override void OnPaint(System.Windows.Forms.PaintEventArgs pevent)
        {
            base.OnPaint(pevent);

            List<weapon.projectile> pendingremoval = new List<weapon.projectile>();
            foreach (weapon.projectile p in projectiles)
            {
                if (!p.Remove)
                {
                    p.Draw(pevent.Graphics,
                        this.Width,
                        this.Height,
                        this.BackgroundImage.Width,
                        this.BackgroundImage.Height
                    );
                }
                else
                {
                    pendingremoval.Add(p);
                }
            }

            for (int n = 0; n < pendingremoval.Count; n++)
            {
                projectiles.Remove(pendingremoval[n]);
            }
        }
    }
}
