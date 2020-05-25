using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Game4
{
    class TextrueGenerator
    {
        GraphicsDevice myGraphicsDevice;
        protected Texture2D GenerateStarMap(int width, int height, int numStars)
        {
            var size = width * height;
            Color[] mapColors = new Color[size];

            Random myRNG = new Random();

            for (int i = 0; i < numStars; i++)
            {
                var n = myRNG.Next(size);
                mapColors[n] = Color.White;
            }

            var myTex = new Texture2D(myGraphicsDevice, width, height, false, SurfaceFormat.Color);
            myTex.SetData(mapColors);
            return myTex;
        }
    }
}
