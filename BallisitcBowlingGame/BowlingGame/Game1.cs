// Final Project created by Jenil Jhala and Shreya Ghimire.

/* This project is an engaging bowling game built with rich graphics, immersive sound effects, and dynamic gameplay. 
 * Players progress through levels, aiming to achieve high scores while enjoying realistic ball physics and challenging
 * pin setups. Each level introduces new challenges, with seamless transitions and personalized features like score 
 * tracking and high-score persistence.The game includes a vibrant menu system, interactive feedback, and fun soundtracks
 * to keep players entertained. Thhe goal is to provide a fun and interactive gaming experience, blending creativity, 
 * logic, and user-friendly design into a cohesive and addictive bowling simulation.*/

using BowlingGame.Screens;
using Microsoft.Xna.Framework;   
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BowlingGame
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        public IScreen currentScreen;
        private MenuScreen menuScreen;
        private HelpScreen helpScreen;
        private LevelOneScreen levelOneScreen;
        private LevelTwoScreen levelTwoScreen;
        private AboutScreen aboutScreen;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            // Initialize `currentScreen` to avoid null references
            currentScreen = null; // Will be assigned in Initialize
        }

        protected override void Initialize()
        {
            // Set the window size and initialize screens
            _graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            _graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            _graphics.ApplyChanges();

            // Initialize screens
            menuScreen = new MenuScreen(this);
            helpScreen = new HelpScreen(this);
            levelOneScreen = new LevelOneScreen(this);
            levelTwoScreen = new LevelTwoScreen(this);
            aboutScreen = new AboutScreen(this);

            // Set the initial screen to the menu
            currentScreen = menuScreen;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // Loading content for all screens
            menuScreen.LoadContent(Content);
            helpScreen.LoadContent(Content);
            levelOneScreen.LoadContent(Content);
            levelTwoScreen.LoadContent(Content);
            aboutScreen.LoadContent(Content);
        }

        protected override void Update(GameTime gameTime)
        {
            if (currentScreen == null)
            {
                currentScreen = menuScreen; 
            }

            // Updating the current screen
            currentScreen.Update(gameTime);

            base.Update(gameTime);
        }

        // Drawing the current screen
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            _spriteBatch.Begin();
            currentScreen.Draw(_spriteBatch);
            _spriteBatch.End();

            base.Draw(gameTime);
        }

        // Methods for different screens
        public void ShowMenu()
        {
            currentScreen = menuScreen;
        }

        public void ShowHelp()
        {
            currentScreen = helpScreen;
        }

        public void StartLevelOne()
        {
            levelOneScreen.Reset();
            currentScreen = levelOneScreen;
        }

        public void StartLevelTwo()
        {
            levelTwoScreen.Reset();
            currentScreen = levelTwoScreen;
            currentScreen.LoadContent(Content);
        }

        public void ShowAboutScreen()
        {
            currentScreen = aboutScreen;
            currentScreen.LoadContent(Content);
        }
    }
}
