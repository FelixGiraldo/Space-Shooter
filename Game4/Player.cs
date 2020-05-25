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
    class Player
    {
        
        Vector2 mySpriteOrigin; //Center av sprite
        Vector2 myPosition;
        Vector2 myStartPosition;
        Vector2 myDirection;

        Texture2D mySprite;
        Texture2D myBullet1sprite;
        Texture2D myBullet2sprite;

        List<Bullet1> myBullet1List;
        List<Bullet2> myBullet2List;

        GameWindow myWindow;
        Random myRNG = new Random();


        Rectangle myCollisionBox;

        float mySize;
        float mySpeed;
        float myAngle;

        int myCooldown;
        int myEnemyTimer;
        int myHealth = 100;

        public int myAmmo = 50;

         bool isAlive = true;


        public int AccessHealth
        {
            get
            {
                return myHealth;
            }
            set { myHealth = value; }
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

        public Vector2 AccessDirection
        {
            get
            {
                return myDirection;
            }
            set
            {
                myDirection = value;
            }
        }
        public List<Bullet1> AccessBullets
        {
            get
            {
                return myBullet1List;
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

        public Player(Texture2D aSprite, Vector2 aPosition, Texture2D aBullet1Sprite ,Texture2D aBullet2Sprite, GameWindow aWindow, float someSpeed = 0.6f, float someSize = 10f)
        {
            myBullet1sprite = aBullet1Sprite;
            myBullet2sprite = aBullet2Sprite;
            mySpeed = someSpeed;
            mySprite = aSprite;
            myPosition = aPosition;
            myStartPosition = aPosition;
            mySpriteOrigin = new Vector2(mySprite.Width / 2f, mySprite.Height / 2f);
            myBullet1List = new List<Bullet1>();
            myBullet2List = new List<Bullet2>();

            myWindow = aWindow;
            mySize = someSize;
            myCollisionBox = new Rectangle(myPosition.ToPoint(), new Point(aSprite.Width, aSprite.Height));

        }



        //  Kollar spelarens kollisioner
        public bool Intersects(Vector2 aPos)
        {
            if (aPos.X > myPosition.X && aPos.X < myPosition.X + mySprite.Width && aPos.Y > myPosition.Y && aPos.Y < myPosition.Y + mySprite.Height)
            {
                return true;
            }
            return false;
        }

        public bool Intersects(Rectangle aCollisionBox)
        {
            if (myCollisionBox.Intersects(aCollisionBox) == true)
            {
                return true;
            }
            return false;
        }

        public void LoadContent()
        {
            myBullet1List = new List<Bullet1>();
            myBullet2List = new List<Bullet2>();
        }



        public void Update(GameTime someTime)
        {
            if(myHealth <= 0)
            {
                isAlive = false;
            }

            float movementSpeed = mySpeed * (float)someTime.ElapsedGameTime.TotalMilliseconds;

            myCooldown--;
            myEnemyTimer--;


            MouseState CurMouse = Mouse.GetState();
            Vector2 MouseLoc = new Vector2(CurMouse.X, CurMouse.Y);
            myDirection = MouseLoc - myPosition;
            myAngle = (float)(Math.Atan2(myDirection.X / 2, myDirection.Y / -2));
            myDirection.Normalize();

            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                myPosition += myDirection * movementSpeed;
            }

            for (int i = 0; i < myBullet1List.Count; i++)
            {
                for (int j = 0; j < EnemyManager.AccessEnemies.Count; j++)
                {
                    if (myBullet1List[i].Intersects(EnemyManager.AccessEnemies[j].AccessCollisionBox) == true)
                    {
                        myAmmo += 10;

                        myBullet1List.RemoveAt(i);

                        EnemyManager.AccessEnemies.RemoveAt(j);

                        i--;
                        j--;

                        break;
                    }

                }
            }

            for (int i = 0; i < myBullet2List.Count; i++)
            {

                for (int j = 0; j < EnemyManager.AccessEnemies.Count; j++)
                {
                    if (myBullet2List[i].Intersects(EnemyManager.AccessEnemies[j].AccessCollisionBox) == true)
                    {

                        myBullet2List.RemoveAt(i);
                        EnemyManager.AccessEnemies.RemoveAt(j);


                        i--;
                        j--;
                        break;
                    }
                }
            }


            myCollisionBox.Location = new Point((int)(myPosition.X - mySpriteOrigin.X), (int)(myPosition.Y - mySpriteOrigin.Y));

            if (CurMouse.LeftButton == ButtonState.Pressed && myCooldown <= 0 && myAmmo > 0)
            {

                if (isAlive == true)
                {
                    myBullet1List.Add(new Bullet1(myBullet1sprite, (myPosition), 1000f, myDirection, myWindow));
                }

                myAmmo -= 1;
                myCooldown = 15;
            }


            if (CurMouse.RightButton == ButtonState.Pressed && myCooldown <= 0 && myAmmo > 0)
            {

                if (isAlive == true)
                {
                    myBullet2List.Add(new Bullet2(myBullet1sprite, (myPosition), 2500f, myDirection, myWindow));
                    myBullet2List.Add(new Bullet2(myBullet1sprite, (myPosition), 2500f, myDirection.Rotate(45), myWindow));
                    myBullet2List.Add(new Bullet2(myBullet1sprite, (myPosition), 2500f, myDirection.Rotate(315), myWindow));

                }
                myAmmo -= 3;
                myCooldown = 30;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.B) && myAmmo >= 50)
            {
                EnemyManager.Reset();
                myAmmo -= 50;
            }

            for (int i = 0; i < myBullet1List.Count; i++)
            {

                if (myBullet1List[i].Update(someTime) == false)
                {
                    myBullet1List.RemoveAt(i);

                    i--;
                }

            }


            for (int i = 0; i < myBullet2List.Count; i++)
            {

                if (myBullet2List[i].Update(someTime) == false)
                {
                    myBullet2List.RemoveAt(i);

                    i--;
                }

            }



        }

        public void Initialize()
        {


        }

        public void Draw(SpriteBatch aSpriteBatch)
        {
            aSpriteBatch.Draw(mySprite, myPosition, null, Color.Fuchsia, myAngle, mySpriteOrigin, 0.5f, SpriteEffects.None, 1f);

            for (int i = 0; i < myBullet1List.Count; i++)
            {

                myBullet1List[i].Draw(aSpriteBatch);

            }

            for (int i = 0; i < myBullet2List.Count; i++)
            {

                myBullet2List[i].Draw(aSpriteBatch);

            }

        }

        public void Reset()
        {
            myHealth = 100;
            isAlive = true;

            myPosition = myStartPosition;

            myBullet1List.Clear();
            myBullet1List = new List<Bullet1>();
            myBullet2List.Clear();
            myBullet2List = new List<Bullet2>();
        }

    }
}
