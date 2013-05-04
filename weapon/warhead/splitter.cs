using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SCORCH.weapon
{
    public class spread : warhead
    {
        Type subwarhead;
        public spread(Type subwarhead)
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
            return new digger(subwarhead);
        }
    }
}
