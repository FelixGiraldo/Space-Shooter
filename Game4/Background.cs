using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game4
{
    class Background
    {
        //List<string> list;

        //Dictionary<int, List<Vector2>> myStars;

        List<Vector2> myFStars;
        List<Vector2> myMStars;
        List<Vector2> myBStars;


        float myFSpeed = 150;
        float myMSpeed = 100;
        float myBSpeed = 50;

        Random myRNG = new Random();

        //Texture2D mySprite;
        Texture2D fSprite;
        Texture2D mSprite;
        Texture2D bSprite;

        Point myScreenSize;

        public void Init(Point aSize, Texture2D aFsprite, Texture2D aMsprite, Texture2D aBsprite, float fSpeed = 75, float mSpeed = 50, float bSpeed = 25, int aNumberOfStars = 75)
        {
            myScreenSize = aSize;

            fSprite = aFsprite;
            mSprite = aMsprite;
            bSprite = aBsprite;

            myFSpeed = fSpeed;
            myMSpeed = mSpeed;
            myBSpeed = bSpeed;

            //myStars = new Dictionary<int, List<Vector2>>();

            myFStars = new List<Vector2>();
            myMStars = new List<Vector2>();
            myBStars = new List<Vector2>();



            //Lägger till antalet stjärnor per lista
            for (int i = 0; i < aNumberOfStars; i++)
            {
                myFStars.Add(new Vector2(myRNG.Next(myScreenSize.X), myRNG.Next(myScreenSize.Y)));
                myMStars.Add(new Vector2(myRNG.Next(myScreenSize.X), myRNG.Next(myScreenSize.Y)));
                myBStars.Add(new Vector2(myRNG.Next(myScreenSize.X), myRNG.Next(myScreenSize.Y)));
            }
        }

        public void Update(GameTime someTime)
        {
            Vector2 tempPos;
            for (int i = 0; i < myFStars.Count; i++)
            {
                tempPos = myFStars[i];
                tempPos.Y += myFSpeed * (float)someTime.ElapsedGameTime.TotalSeconds;
                if(tempPos.Y >= myScreenSize.Y)
                {
                    tempPos.Y = 0;
                }
                myFStars[i] = tempPos;
            }

            for (int i = 0; i < myMStars.Count; i++)
            {
                tempPos = myMStars[i];
                tempPos.Y += myMSpeed * (float)someTime.ElapsedGameTime.TotalSeconds;
                if (tempPos.Y >= myScreenSize.Y)
                {
                    tempPos.Y = 0;
                }
                myMStars[i] = tempPos;
            }
            for (int i = 0; i < myBStars.Count; i++)
            {
                tempPos = myBStars[i];
                tempPos.Y += myBSpeed * (float)someTime.ElapsedGameTime.TotalSeconds;
                if (tempPos.Y >= myScreenSize.Y)
                {
                    tempPos.Y = 0;
                }
                myBStars[i] = tempPos;
            }


        }
        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < myFStars.Count; i++)
            {
                spriteBatch.Draw(fSprite, myFStars[i], Color.SlateGray);
            }
            for (int i = 0; i < myMStars.Count; i++)
            {
                spriteBatch.Draw(mSprite, myMStars[i], Color.SlateGray);
            }
            for (int i = 0; i < myBStars.Count; i++)
            {
                spriteBatch.Draw(bSprite, myBStars[i], Color.SlateGray);
            }


        }


    }
}
