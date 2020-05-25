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
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        public enum GameStates
        {
            Menu,
            Playing,
            Paused,
            Resume
        }


        GameStates gameState = GameStates.Playing;




        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D myBullet1Sprite;
        Texture2D myBullet2Sprite;
        Texture2D myRangerBulletSprite;
        Texture2D Menu;

        Texture2D myShip;
        Texture2D myAlien1;
        Texture2D myAlien2;
        Texture2D mySight;
        Texture2D myStarSprite;
        Texture2D myBlock;

        //Star myStar;
        //Room myRoom;

        Player myPlayer;
        Background myBackground;
        App myApp;
        BaseBullet myBullet;

        GameWindow myWindow;

        Random myRNG = new Random();

        SpriteFont myScoreFont;
        SpriteFont myHealthFont;
        SpriteFont myMenu1Font;
        SpriteFont AmmoFont;

        public static bool isPaused(GameStates gameState)
        {
            switch (gameState)
            {
                case GameStates.Menu:
                case GameStates.Paused:
                default:
                    return false;
                case GameStates.Playing:
                    return true;
            }
        }

        public Game1()
        {

            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        

            graphics.PreferredBackBufferWidth = 1920;
            graphics.PreferredBackBufferHeight = 1080;


        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();

            this.IsMouseVisible = true;

            myBackground = new Background();
            myBackground.Init(Window.ClientBounds.Size, myStarSprite, myStarSprite, myStarSprite);
            myPlayer = new Player(myShip, new Vector2(1920 / 2, 1080 / 2), myBullet1Sprite, myBullet2Sprite, Window);
            myApp = new App(Window, myPlayer, myStarSprite, myStarSprite, myBullet1Sprite);

            gameState = GameStates.Paused;

        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            myShip = Content.Load<Texture2D>("Player");
            myAlien1 = Content.Load<Texture2D>("Alien");
            myAlien2 = Content.Load<Texture2D>("Enemy2");

            myBullet1Sprite = Content.Load<Texture2D>("Bullet1");
            myBullet2Sprite = Content.Load<Texture2D>("Bullet2");
            myRangerBulletSprite = Content.Load<Texture2D>("Bullet1");

            myScoreFont = Content.Load<SpriteFont>("Score");
            myHealthFont = Content.Load<SpriteFont>("PlayerHealth");
            myMenu1Font = Content.Load<SpriteFont>("Menu1");

            AmmoFont = Content.Load<SpriteFont>("Menu/Ammo");

            myStarSprite = Content.Load<Texture2D>("Star"); //SKAFFA STARTEXTURE

            Menu = Content.Load<Texture2D>("Menu/PauseButton");

            myBlock = LoadTexture("bullet"); //Loading the backgroundtile


            //myRoom = new Room(myBlock, myStarSprite); //Create the room
            //myRoom.Init(); //Setting the values

            


        }

        public Texture2D LoadTexture(string aPath)
        {
            return Content.Load<Texture2D>("bullet");
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {

            KeyboardState CurrentKeyboardState = Keyboard.GetState();
            /**
                        if (CurrentKeyboardState.IsKeyDown(Keys.Escape))
                            this.Exit();
                      */
            // TODO: Add your update logic here

            //myRoom.Update(gameTime);
             
            if(myPlayer.AccessHealth <= 0)
            {
               // this.Reset();
                gameState = GameStates.Paused;
            }

            if (isPaused(gameState) && CurrentKeyboardState.IsKeyDown(Keys.Escape))
            {

                gameState = GameStates.Paused;

            }

            if (gameState == GameStates.Paused && CurrentKeyboardState.IsKeyDown( Keys.Space))
            {
                gameState = GameStates.Playing;

            }

            if (myPlayer.AccessHealth <= 0 && CurrentKeyboardState.IsKeyDown(Keys.Space))
            {
                this.Reset();
                gameState = GameStates.Playing;

            }

            if (gameState == GameStates.Paused && CurrentKeyboardState.IsKeyDown(Keys.Back))
            {
                this.Exit();
            }

            if (gameState == GameStates.Playing)
            {

                myPlayer.Update(gameTime);

                myApp.Update(gameTime);
                EnemyManager.Update(gameTime);
                myBackground.Update(gameTime);

           
            }
            base.Update(gameTime);

        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here
            Vector2 myScreenCentre = new Vector2(Window.ClientBounds.Width/2, Window.ClientBounds.Height/2);
      

            spriteBatch.Begin();

            //myRoom.Draw(spriteBatch); //Drawing the room

            myBackground.Draw(spriteBatch);

            spriteBatch.DrawString(myScoreFont, "Score:" + ScoreManager.AccessScore, new Vector2(10, 10), Color.YellowGreen);
            spriteBatch.DrawString(myHealthFont, "Health:" + myPlayer.AccessHealth, new Vector2(10, 60), Color.Crimson);
            //  spriteBatch.DrawString(myMenu1Font, "Press P to play", new Vector2(1920 / 2, 1080 / 2), Color.White);

            if (myPlayer.myAmmo <= 49)
            {
                spriteBatch.DrawString(AmmoFont, "Ammo: " + myPlayer.myAmmo, new Vector2(myPlayer.AccessPosition.X - 10, myPlayer.AccessPosition.Y - 80), Color.DeepPink);
            }
            else if(myPlayer.myAmmo >= 50)
            {
                spriteBatch.DrawString(AmmoFont, "Ammo: " + myPlayer.myAmmo, new Vector2(myPlayer.AccessPosition.X - 10, myPlayer.AccessPosition.Y - 80), Color.Green);

            }
            myApp.Draw(spriteBatch);
            myPlayer.Draw(spriteBatch);

            if (gameState == GameStates.Paused)
            {
                if (myPlayer.AccessHealth <= 0)
                {
                    spriteBatch.DrawString(myScoreFont, "YOU DIED!", new Vector2(Window.ClientBounds.Width / 2 - 80, Window.ClientBounds.Height / 2 - 100), Color.Red);
                    spriteBatch.DrawString(myScoreFont, "RESTART(Spacebar)", new Vector2(Window.ClientBounds.Width / 2 - 200, Window.ClientBounds.Height / 2), Color.Red);
                    spriteBatch.DrawString(myScoreFont, "GIVE UP(Backspace)", new Vector2(Window.ClientBounds.Width / 2 - 200, Window.ClientBounds.Height / 2 + 100), Color.Red);

                }

                else 
                {
                    spriteBatch.DrawString(myScoreFont, "PAUSED", new Vector2(Window.ClientBounds.Width / 2 - 80, Window.ClientBounds.Height / 2 - 100), Color.Gold);
                    spriteBatch.DrawString(myScoreFont, "Resume(Spacebar)", new Vector2(Window.ClientBounds.Width / 2 - 200, Window.ClientBounds.Height / 2), Color.Orange);
                    spriteBatch.DrawString(myScoreFont, "Exit(Backspace)", new Vector2(Window.ClientBounds.Width / 2 - 150, Window.ClientBounds.Height / 2 + 100), Color.DarkOrange);
                }


            }

            spriteBatch.End();

            base.Draw(gameTime);

        }

        public void Reset()
        {
            myPlayer.Reset();
            EnemyManager.Reset();
            ScoreManager.AccessScore = 0;
        }
    }
}
