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
    class Boss
    {
        Vector2 myPosition;
        Vector2 myDirection;

        Texture2D mySprite;

        float mySpeed;

        public Boss(Texture2D aSprite, Vector2 aPosition, float someSpeed)
        {

            mySprite = aSprite;
            myPosition = aPosition;
            mySpeed = someSpeed;

        }

        public void Update(GameTime someTime)
        {

        }

        public void Draw(SpriteBatch aSpriteBatch)
        {

        }

    }
}
