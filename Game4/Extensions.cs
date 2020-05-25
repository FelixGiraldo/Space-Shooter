using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game4
{
    static class Extensions
    {
        public static Vector2 Rotate(this Vector2 v, float degrees)
        {
            float sin = (float)Math.Sin((double)(degrees * (Math.PI / 180)));
            float cos = (float)Math.Cos((double)(degrees * (Math.PI / 180)));

            float tx = v.X;
            float ty = v.Y;
            v.X = (cos * tx) - (sin * ty);
            v.Y = (sin * tx) + (cos * ty);
            return v;

        }
    }
}
