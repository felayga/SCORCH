using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SCORCH.weapon
{
    public class splitter : warhead
    {
        Type subwarhead;
        public splitter(Type subwarhead)
        {
            trigger = 40.0;
            this.subwarhead = subwarhead;
        }

        private Random rand = new Random();

        public override warhead[] Spread()
        {
            return new warhead[] {
                (warhead)subwarhead.GetConstructor(new Type[]{}).Invoke(new object[]{}),
                (warhead)subwarhead.GetConstructor(new Type[]{}).Invoke(new object[]{}),
                (warhead)subwarhead.GetConstructor(new Type[]{}).Invoke(new object[]{}),
                (warhead)subwarhead.GetConstructor(new Type[]{}).Invoke(new object[]{}),
                (warhead)subwarhead.GetConstructor(new Type[]{}).Invoke(new object[]{})
            };
        }

        public override bool Explode(int xpoint, int ypoint, utility.DataTypes.BitmapWrapper wrapper)
        {
            return true;
        }

        public override warhead Clone()
        {
            return new splitter(subwarhead);
        }
    }
}
