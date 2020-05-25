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
    static class ScoreManager
    {
        static int myScore;

       static void Score(int aScore)
        {
            myScore = aScore;
        }

        static public int AccessScore
        {
            get { return myScore; }
            set { myScore = value; }
        }
    }
}
