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
    class App
    {

        Player myPlayer;

        GameWindow myWindow;
        Texture2D myEnemySprite;
        Texture2D myEnemy2Sprite;
        Texture2D myBulletSprite;
        Vector2 myPosition;

        Vector2 randomSpawn1;

        Vector2 randomSpawn2;
        List<GameWindow> mySpawn;

        float mySpeed;
        float myEnemyBulletSpeed = 200;
        float myEnemyTimer;
        Random myRNG = new Random();

        public App( GameWindow aWindow, Player aPlayer, Texture2D anEnemySprite, Texture2D anEnemy2Sprite, Texture2D aBulletSprite)
        {


            EnemyManager.Initialize();
            myPlayer = aPlayer;
            myWindow = aWindow;
            myEnemySprite = anEnemySprite;
            myEnemy2Sprite = anEnemy2Sprite;
         //   mySpeed = 80;
            myBulletSprite = aBulletSprite;

        }
        
        public void Update(GameTime someTime)
        {

            randomSpawn1 = new Vector2(0, myWindow.ClientBounds.Height);
            randomSpawn2 = new Vector2(myWindow.ClientBounds.Width, 0);

            EnemyManager.Update(someTime);
            myEnemyTimer += (float)someTime.ElapsedGameTime.TotalSeconds;

            myPosition = new Vector2(myRNG.Next(0, myWindow.ClientBounds.Width - myEnemySprite.Width), -myEnemySprite.Height);

            if (myEnemyTimer > 3f)
            {
                // Funkar?? 
                myPosition = new Vector2(myRNG.Next(0, myWindow.ClientBounds.Width - myEnemySprite.Width), -myEnemySprite.Height);
                EnemyManager.AddEnemy(new Enemy2(myEnemy2Sprite, myPosition, 50f, Color.Plum, myPlayer));

                myPosition = new Vector2(myRNG.Next(0, myWindow.ClientBounds.Width - myEnemySprite.Width), -myEnemySprite.Height);
                EnemyManager.AddEnemy(new EnemyRanger(myEnemySprite, myBulletSprite, myPosition, 50, myEnemyBulletSpeed, Color.HotPink, myPlayer, myWindow));

                myPosition = new Vector2(myRNG.Next(0, myWindow.ClientBounds.Width - myEnemySprite.Width), -myEnemySprite.Height);
                EnemyManager.AddEnemy(new Carrier(myEnemySprite, myPosition, mySpeed, Color.GreenYellow, myPlayer, myWindow));

                myEnemyTimer = 0f;
            }

            //Kollar kollisioner mellan spelaren och fiender
            for (int i = 0; i < EnemyManager.AccessEnemies.Count; i++)
            {
               EnemyManager.AccessEnemies [i].Update(someTime);

                if (myPlayer.AccessCollisionBox.Intersects(EnemyManager.AccessEnemies[i].AccessCollisionBox))
                {
                    EnemyManager.AccessEnemies.RemoveAt(i);
                    i--;
                    ScoreManager.AccessScore += 10;
                    myPlayer.AccessHealth -= 10;

                }



                //HJÄLP Enemy2 kollisioner funkar ej
                
                //if (EnemyManager.AccessEnemies[i].GetType() == typeof(Enemy2) )
                //{
                //    if (myPlayer.AccessCollisionBox.Intersects(((Enemy2)(EnemyManager.AccessEnemies[i])).AccessCollisionBox))
                //    {
                //        EnemyManager.AccessEnemies.RemoveAt(i);
                //        i--;
                //        ScoreManager.AccessScore += 10;
                //        myPlayer.AccessHealth -= 10;
                //    }
                //}
            }

            //Kollar kollisioner mellan fienden och ens bullets
            for (int i = 0; i < EnemyManager.AccessEnemies.Count; i++)
            {
                for (int j = 0; j < myPlayer.AccessBullets.Count; j++)
                {
                    if (myPlayer.AccessBullets[j].AccessCollisionBox.Intersects(EnemyManager.AccessEnemies[i].AccessCollisionBox))
                    {
                        myPlayer.AccessBullets.RemoveAt(j);
                        EnemyManager.AccessEnemies.RemoveAt(i);
                        ScoreManager.AccessScore += 10;

                       myPlayer.myAmmo += 5;

                        i--;
                        break;
                    }
                }
            }


        }

        public void Draw(SpriteBatch aSpriteBatch)
        {

            EnemyManager.Draw(aSpriteBatch);
        }
    }
}
