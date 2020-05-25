using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;

namespace Game4
{
    static class EnemyManager
    {

       static List<BaseEnemy> myEnemies;
        static float friendDistacne = 50;
        static public void Initialize()
        {
            myEnemies = new List<BaseEnemy>();
        }

        static public void Update(GameTime someTime)
        {
            for (int i = 0; i < myEnemies.Count; i++)
            {
                myEnemies[i].Update(someTime);
            }
            //Kollar om enemy är nära en annan enemy och hindrar dem från att "Klumpa" ihop sig
            for (int i = 0; i < myEnemies.Count; i++)
            {
                for (int j = 0; j < myEnemies.Count; j++)
                {
                    if(i != j)
                    {
                        if (Vector2.Distance(myEnemies[i].AccessPosition, myEnemies[j].AccessPosition) <= friendDistacne)
                        {
                            myEnemies[i].setMoveModifier(-Vector2.Normalize(myEnemies[j].AccessPosition - myEnemies[i].AccessPosition));
                        }

                    }
                }
            }

          

        }

        static public void AddEnemy(BaseEnemy anEnemy)
        {
            myEnemies.Add(anEnemy);
        }

        static public void Draw(SpriteBatch aSpriteBatch)
        {
            for (int i = 0; i < myEnemies.Count; i++)
            {
                myEnemies[i].Draw(aSpriteBatch);
            }
          
        }

        static public void Reset()
        {
            myEnemies.Clear();
            myEnemies = new List<BaseEnemy>();
        }

        static public List<BaseEnemy> AccessEnemies
        {
            get { return myEnemies; }
        }
    }

}
