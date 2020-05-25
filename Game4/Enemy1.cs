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
    class Enemy1 : BaseEnemy
    {
        public Enemy1(Texture2D aSprite, Vector2 aPosition, float someSpeed, Color aColor, Player aPlayer) : base(aSprite, aPosition, someSpeed, aColor, aPlayer)
        {

        }
       

    } 
}
