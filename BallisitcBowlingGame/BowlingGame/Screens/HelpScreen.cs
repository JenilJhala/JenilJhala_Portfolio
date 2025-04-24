using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;

namespace BowlingGame.Screens
{
    public class HelpScreen : IScreen
    {
        private Game1 _game;
        private SpriteFont titleFont;
        private SpriteFont controlsFont;
        private Texture2D background;

        private SoundEffect helpBgSound;
        private SoundEffectInstance helpBgInstance;

        public HelpScreen(Game1 game)
        {
            _game = game;
        }

        public void LoadContent(ContentManager content)
        {
            titleFont = content.Load<SpriteFont>("Fonts/MenuFont");
            controlsFont = content.Load<SpriteFont>("Fonts/LevelFont");
            background = content.Load<Texture2D>("Images/HelpBGImage");

            helpBgSound = content.Load<SoundEffect>("aboutSound");
            helpBgInstance = helpBgSound.CreateInstance();
            helpBgInstance.IsLooped = true;
        }

        public void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Back) || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                helpBgInstance.Stop();
                _game.ShowMenu();
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            // Draw the background
            spriteBatch.Draw(
                background,
                new Rectangle(0, 0, _game.GraphicsDevice.Viewport.Width, _game.GraphicsDevice.Viewport.Height),
                Color.White
            );

            // Screen dimensions
            int screenWidth = _game.GraphicsDevice.Viewport.Width;
            int screenHeight = _game.GraphicsDevice.Viewport.Height;

            // Title text
            string title = "Help & Controls";
            Vector2 titleSize = titleFont.MeasureString(title);

            Vector2 titlePosition = new Vector2((screenWidth / 1.5f) - titleSize.X / 2, screenHeight / 4);

            // Controls text
            string controls1 = "Arrow Keys: Navigate";
            string controls2 = "Space: Release Ball";
            string controls3 = "Backspace/Escape: Back to Menu";

            float lineSpacing = 90f; // Spacing between control lines

            
            Vector2 controls1Position = new Vector2(screenWidth / 1.5f - controlsFont.MeasureString(controls1).X / 2, titlePosition.Y + 160);
            Vector2 controls2Position = new Vector2(screenWidth / 1.5f - controlsFont.MeasureString(controls2).X / 2, controls1Position.Y + lineSpacing);
            Vector2 controls3Position = new Vector2(screenWidth / 1.5f - controlsFont.MeasureString(controls3).X / 2, controls2Position.Y + lineSpacing);

            // Colors for text
            Color titleColor = Color.Red; 
            Color controlsColor = Color.Blue; 

            // Draw shadows for better visibility
            Vector2 shadowOffset = new Vector2(2, 2); // Offset for shadow
            spriteBatch.DrawString(titleFont, title, titlePosition + shadowOffset, Color.White);
            spriteBatch.DrawString(controlsFont, controls1, controls1Position + shadowOffset, Color.White);
            spriteBatch.DrawString(controlsFont, controls2, controls2Position + shadowOffset, Color.White);
            spriteBatch.DrawString(controlsFont, controls3, controls3Position + shadowOffset, Color.White);

            // Draw the text
            spriteBatch.DrawString(titleFont, title, titlePosition, titleColor);
            spriteBatch.DrawString(controlsFont, controls1, controls1Position, controlsColor);
            spriteBatch.DrawString(controlsFont, controls2, controls2Position, controlsColor);
            spriteBatch.DrawString(controlsFont, controls3, controls3Position, controlsColor);
        }
    }
}
