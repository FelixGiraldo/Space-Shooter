using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game4
{
    class EnemyRanger : Enemy2
    {
        Texture2D mySprite;
        float mySpeed;
        float shootingRange = 40f;
        float myBulletSpeed = 1000f;
        Color myColor;
        Player myPlayer;
        GameWindow myWindow;
        int myBulletCoolDown = 15;
        Texture2D myBulletSprite;
        List<Bullet1> myBulletList;


        public List<Bullet1> AccessBullets
        {
            get
            {
                return myBulletList;
            }
        }

        public EnemyRanger(Texture2D aSprite, Texture2D aBulletSprite, Vector2 aPosition, float someSpeed, float someBulletSpeed, Color aColor, Player aPlayer, GameWindow aWindow) : base(aSprite, aPosition, someSpeed, aColor, aPlayer)
        {
            mySprite = aSprite;
            myBulletSprite = aBulletSprite;
            myPosition = aPosition;
            mySpeed = someSpeed;
            myColor = aColor;
            myPlayer = aPlayer;
            myBulletList = new List<Bullet1>();
            myDirection = myPlayer.AccessPosition - myPosition;
            myBulletSpeed = someBulletSpeed;
            myWindow = aWindow;
        }



        public override void Update(GameTime someTime)
        {

            myDirection = myPlayer.AccessPosition - myPosition;
            myDirection.Normalize();
            myDirection = myDirection + myMoveModifier;
            myDirection.Normalize();
            myPosition += myDirection;
            myMoveModifier = Vector2.Zero;
            myCollisionBox.Location = myPosition.ToPoint();

            myBulletCoolDown--;

            if (myBulletCoolDown <= 0)
            {
                myBulletList.Add(new Bullet1(myBulletSprite, (myPosition), myBulletSpeed, myDirection, myWindow));

                myBulletCoolDown = 200;

            }

            for (int i = 0; i < myBulletList.Count; i++)
            {

                if (myBulletList[i].Update(someTime)== false)
                {
                    myBulletList.RemoveAt(i);
                    i--;
                }
            }

        }
            

        public override void Draw(SpriteBatch aSrpiteBatch)
        {
            aSrpiteBatch.Draw(mySprite, myPosition, null, myColor, 0f, mySpriteOrigin, 2f, SpriteEffects.None, 0f);

            for (int i = 0; i < myBulletList.Count; i++)
            {

                myBulletList[i].Draw(aSrpiteBatch);

            }
        }


            
        }
    
    }

