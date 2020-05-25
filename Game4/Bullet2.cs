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
    class Bullet2 : BaseBullet
    {
        public Bullet2(Texture2D aSprite, Vector2 aPosition, float someSpeed, Vector2 aDirection, GameWindow aWindow) : base(aSprite, aPosition, someSpeed, aDirection, aWindow)
        {



        }
    }
}
