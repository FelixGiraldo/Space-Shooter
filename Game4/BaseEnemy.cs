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
    class BaseEnemy
    {
       protected Vector2 myPosition;
      protected Vector2 mySpriteOrigin;
        float mySpeed;
        Texture2D mySprite;
        Color myColor = Color.White;
       protected Rectangle myCollisionBox;
       protected Vector2 myDirection;
       protected Vector2 myMoveModifier = Vector2.Zero;
        Player myPlayer;
        public void setMoveModifier(Vector2 moveModifier)
        {
            myMoveModifier = moveModifier;
        }

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

        public Vector2 AccessPosition
        {
            get
            {
                return myPosition;
            }
            set
            {
                myPosition = value;
            }
        }

        public Color AccessColor
        {
            get
            {
                return myColor;
            }
            set
            {
                myColor = value;
            }

        }

        public BaseEnemy(Texture2D aSprite, Vector2 aPosition, float someSpeed, Color aColor, Player aPlayer )
        {
            myPlayer = aPlayer;
            mySprite = aSprite;
            myPosition = aPosition;
            mySpeed = someSpeed;
            mySpriteOrigin = new Vector2(mySprite.Width / 2f, mySprite.Height / 2f);
            myCollisionBox = new Rectangle(new Point((int)mySpriteOrigin.X - (mySprite.Bounds.X / 2), (int)mySpriteOrigin.Y - (mySprite.Bounds.Y / 2)) , new Point(aSprite.Width, aSprite.Height));
            myColor = aColor;

        }
        
        public virtual void Update(GameTime someTime)
        {

          //  myPosition.Y +=  mySpeed * (float)someTime.ElapsedGameTime.TotalSeconds;
            myCollisionBox.Location = myPosition.ToPoint();

            myDirection = myPlayer.AccessPosition - myPosition;
            myDirection.Normalize();
            myDirection = myDirection + myMoveModifier;
            myDirection.Normalize();
            myPosition += myDirection;
            myMoveModifier = Vector2.Zero;
            myCollisionBox.Location = myPosition.ToPoint();

        }

        public bool Intersects(Rectangle aCollisionBox)
        {
            if (myCollisionBox.Intersects(aCollisionBox) == true)
            {
                return true;
            }
            return false;
        }


        public virtual void Draw(SpriteBatch aSpriteBatch)
        {
            aSpriteBatch.Draw(mySprite, myPosition, null, myColor, 0f, mySpriteOrigin, 2f, SpriteEffects.None, 0f);
        }

    }
}
