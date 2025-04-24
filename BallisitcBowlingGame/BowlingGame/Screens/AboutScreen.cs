using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Audio;

namespace BowlingGame.Screens
{
    public class AboutScreen : IScreen
    {
        private Game1 _game;

        private SpriteFont titleFont;
        private SpriteFont contentFont;
        private SpriteFont footerFont;

        private Texture2D background;

        private SoundEffect aboutSound;
        private SoundEffectInstance aboutSoundInstance;

        private string title = "About Ballistic Bowling";
        private string description = "Ballistic Bowling is a fast-paced and fun bowling game\n " +
                                      "where precision meets chaos! Knock down pins, score big,\n and aim for " +
                                      "a perfect strike. Challenge yourself with levels\n that test both " +
                                      "accuracy and timing. Every roll counts, and every pin\n matters. " +
                                      "Step onto the lane, and let the strikes roll!\n";
        private string credits = "Created by: Jenil Jhala & Shreya Ghimire\nYear: 2024\n\n-Press Backspace or Escape to Return to Menu";

        public AboutScreen(Game1 game)
        {
            _game = game;
        }

        public void LoadContent(ContentManager content)
        {
            titleFont = content.Load<SpriteFont>("Fonts/LevelFont");
            contentFont = content.Load<SpriteFont>("Fonts/ScoreFont");
            footerFont = content.Load<SpriteFont>("Fonts/AboutFont");
            background = content.Load<Texture2D>("Images/ContentAbout");

            aboutSound = content.Load<SoundEffect>("aboutSound");
            aboutSoundInstance = aboutSound.CreateInstance();
            aboutSoundInstance.IsLooped = true;
            
        }

        public void Update(GameTime gameTime)
        {
            // Return to menu if Backspace or Escape is pressed
            if (Keyboard.GetState().IsKeyDown(Keys.Back) || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                // Stops the sound before navigating to the menu
                aboutSoundInstance.Stop();
                _game.ShowMenu();
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            aboutSoundInstance.Play();

            spriteBatch.Draw(
                background,
                new Rectangle(0, 0, _game.GraphicsDevice.Viewport.Width, _game.GraphicsDevice.Viewport.Height),
                Color.White
            );

            Vector2 titleSize = titleFont.MeasureString(title);
            Vector2 titlePosition = new Vector2(
                100,
                250
            );
            spriteBatch.DrawString(titleFont, title, titlePosition + new Vector2(2, 2), Color.Black * 0.5f);
            spriteBatch.DrawString(titleFont, title, titlePosition, Color.Red);

            Vector2 descriptionPosition = new Vector2(
                100,
                titlePosition.Y + titleSize.Y + 30
            );
            spriteBatch.DrawString(contentFont, description, descriptionPosition + new Vector2(2, 2), Color.Blue * 0.5f);
            spriteBatch.DrawString(contentFont, description, descriptionPosition, Color.Blue);

            Vector2 creditsPosition = new Vector2(
                100,
                _game.GraphicsDevice.Viewport.Height - 250
            );
            spriteBatch.DrawString(footerFont, credits, creditsPosition + new Vector2(2, 2), Color.White * 0.5f);
            spriteBatch.DrawString(footerFont, credits, creditsPosition, Color.Red);
        }
    }
}
