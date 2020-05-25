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
    class Enemy2 : BaseEnemy
    {
        Player myPlayer;
        float mySpeed;

        public Enemy2(Texture2D aSprite, Vector2 aPosition, float someSpeed, Color aColor, Player aPlayer) : base(aSprite, aPosition, someSpeed, aColor, aPlayer)
        {
            myPosition = aPosition;
            mySpeed = someSpeed;
            myPlayer = aPlayer;
        }



        public override  void  Update(GameTime someTime)
        {
            myDirection = myPlayer.AccessPosition - myPosition;
            myDirection.Normalize();
            myDirection = myDirection + myMoveModifier;
            myDirection.Normalize();
            myPosition += myDirection;
            myMoveModifier = Vector2.Zero;
            myCollisionBox.Location = myPosition.ToPoint();

        }
        

    }
}
