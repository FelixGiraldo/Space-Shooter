using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game4
{
    class Room
    {

        public Texture2D myTileSprite;
        public int[,] mySize;
        public int mySpriteSize;
        public Color myBackgroundColor;
        public Texture2D myStarTexture;

        List<Star> myStars;
        
        public Room(Texture2D aTileSprite, Texture2D aStarTexture)
        {
            myTileSprite = aTileSprite;
            myStarTexture = aStarTexture;

        }

        public void Init()
        {
            mySize = new int[64, 64];
            mySpriteSize = 32;
            myBackgroundColor = Color.Black; //Change backgoundcolor here

            myStars = new List<Star>();

        }

        int myTimer; 

        Random random = new Random();

        public void Update(GameTime someTime)
        {
            if (myTimer >= random.Next(3,8))
            {
                myStars.Add(new Star(myStarTexture));
                myStars[myStars.Count -1].Init();

                myTimer = 0;
            }

            for (int i = 0; i < myStars.Count; i++)
            {
                myStars[i].Update(someTime);
            }

            myTimer += 1;

        }


        public void Draw(SpriteBatch aSpriteBatch)
        {
            for (int i = 0; i < mySize.GetLength(0); i++)
            {
                for (int j = 0; j < mySize.GetLength(1); j++)
                {
                    aSpriteBatch.Draw(myTileSprite, new Vector2(i * mySpriteSize, j * mySpriteSize), myBackgroundColor);

                }
            }
            foreach (var aStar in myStars)
            {
                aStar.Draw(aSpriteBatch);
            }
        }
    }
}
