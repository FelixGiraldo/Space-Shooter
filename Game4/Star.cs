using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game4
{
    class Star
    {
        public Vector2 myPosition;
        public Texture2D myTexture;

        public float mySpeed;
        public float mySize;

        public Vector2 myTextureOrigin;
        public Rectangle myRectangle;
        public float myLayer = 3;

        Random random = new Random();

        public Star(Texture2D aTexture)
        {
            myTexture = aTexture;
        }

        public void Init()
        {
            mySpeed = 5;// random.Next(1, 6);
            mySize = random.Next(1, 4);
            myTextureOrigin = new Vector2(myTexture.Width / 2, myTexture.Height / 2);



            myPosition = new Vector2(random.Next(1920), 0); //Random between 0 and roomsize

        }

        public void Update(GameTime someTime)
        {
            myRectangle = new Rectangle((int)myPosition.X, (int)myPosition.Y, myTexture.Width, myTexture.Height);
            myPosition.Y += mySpeed * (float)someTime.ElapsedGameTime.TotalSeconds;
            
        }

        public void Draw(SpriteBatch aSpriteBatch)
        {
            aSpriteBatch.Draw(myTexture, myPosition, myRectangle, Color.White, 0, myTextureOrigin, 1, SpriteEffects.None, 0);
        }

    }
}
