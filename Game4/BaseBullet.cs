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
    class BaseBullet
    {
        Vector2 mySpriteOrigin;
        Vector2 myPosition;
        Texture2D mySprite;
        Vector2 myDirection;
        GameWindow myWindow;
        float mySpeed;
       
        Rectangle myCollisionBox;

        public Rectangle AccessCollisionBox
        {
            get
            {
                return myCollisionBox;
            }

            set
            {
                myCollisionBox = value;
            }
        }

        public BaseBullet(Texture2D aSprite, Vector2 aPosition, float someSpeed , Vector2 aDirection, GameWindow aWindow)
        {
            myWindow = aWindow;
            myDirection = aDirection;
            mySpeed = someSpeed;
            mySprite = aSprite;
            myPosition = aPosition;
            mySpriteOrigin = new Vector2(mySprite.Height / 2, mySprite.Width / 2);

            myCollisionBox = new Rectangle(myPosition.ToPoint(), new Point(aSprite.Width, aSprite.Height));

        }

        public bool Intersects(Rectangle aCollisionBox)
        {
            return (myCollisionBox.Intersects(aCollisionBox));
        }


        public void Draw(SpriteBatch aSpriteBatch)
        {
            aSpriteBatch.Draw(mySprite, myPosition, null, Color.Crimson, 0f, mySpriteOrigin, 0.5f, SpriteEffects.None, 1f);
        }


        public virtual bool Update(GameTime someTime)
        {
            myPosition += myDirection * mySpeed * (float)someTime.ElapsedGameTime.TotalSeconds;
            myCollisionBox.Location = new Point((int)(myPosition.X - mySpriteOrigin.X), (int)(myPosition.Y - mySpriteOrigin.Y));


            if (myPosition.X >= myWindow.ClientBounds.Width || myPosition.Y >= myWindow.ClientBounds.Height || myPosition.X <= 0 || myPosition.Y <= 0)
            {
                  
                return false;
            }

            return true;
        }
    }
}
