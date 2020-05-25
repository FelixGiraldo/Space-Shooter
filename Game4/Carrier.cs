using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game4
{
    class Carrier : Enemy2
    {
        Texture2D mySprite;
        float mySpeed;
        Color myColor;
        Player myPlayer;
        GameWindow myWindow;

        float myEnemyTimer;


        int myHealth = 100;

        bool isAlive = true;


        public int AccessHealth
        {
            get
            {
                return myHealth;
            }
            set { myHealth = value; }
        }

        public Carrier(Texture2D aSprite, Vector2 aPosition, float someSpeed, Color aColor, Player aPlayer, GameWindow aWindow) : base(aSprite, aPosition, someSpeed, aColor, aPlayer)
        {

            mySprite = aSprite;
            myPosition = aPosition;
            mySpeed = someSpeed;
            myColor = aColor;
            myPlayer = aPlayer;
            myWindow = aWindow;

        }

        public override void Update(GameTime someTime)
        {
             base.Update(someTime);
            // myEnemyTimer--;

            myPosition += myDirection * mySpeed * (float)someTime.ElapsedGameTime.TotalSeconds;

            if (myHealth <= 0)
            {
                isAlive = false;
            }
            
            myDirection = myPlayer.AccessPosition - myPosition;
            myDirection.Normalize();
            myDirection = myDirection + myMoveModifier;
            myDirection.Normalize();
            myPosition += myDirection;
            myMoveModifier = Vector2.Zero;
            myCollisionBox.Location = myPosition.ToPoint();

            myEnemyTimer += (float)someTime.ElapsedGameTime.TotalSeconds;

            if (isAlive == true && myEnemyTimer > 8f)
            {

                EnemyManager.AddEnemy(new Enemy1(mySprite, myPosition, 1000f, Color.DarkCyan, myPlayer));
                myEnemyTimer = 0f;
            }

        }

        public override void Draw(SpriteBatch aSrpiteBatch)
        {
            aSrpiteBatch.Draw(mySprite, myPosition, null, myColor, 0f, mySpriteOrigin, 5f, SpriteEffects.None, 0f);
        }
    }
}
