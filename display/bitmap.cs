using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SCORCH.display
{
    public class bitmap : utility.DataTypes.BitmapWrapper
    {
        public bitmap(IntPtr buffer, int width, int height) : base(buffer, width, height, 1) { }

        public void SetPixel(int x, int y, uint r, uint g, uint b)
        {
            base.SetPixel(x, y, 40 + (r * 6 + g) * 6 + b);
        }

        public bool GetPixel(int index, out uint r, out uint g, out uint b)
        {
            uint value = base[index];
            if (value >= 40)
            {
                value -= 40;
                b = value % 6;
                value /= 6;
                g = value % 6;
                r = value / 6;
                return true;
            }
            else if (value < 16)
            {
                b = value % 2;
                value /= 2;
                g = value % 2;
                r = value / 2;
                return false;
            }
            else
            {
                r = 0;
                g = 0;
                b = 0;
                return false;
            }
        }

        public void SetPixel(int index, uint r, uint g, uint b)
        {
            base[index] = 40 + (r * 6 + g) * 6 + b;
        }
    }
}
