using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System.IO;
using System;
using Microsoft.Xna.Framework.Audio;

namespace BowlingGame.Screens
{
    public class ScoreScreen : IScreen
    {
        private Game1 _game;
        private SpriteFont scoreFont;
        private Texture2D backgroundScore, displayScore;
        private int score;
        private int highScore;
        private string highScoreFilePath = "highscore.txt";

        private SoundEffect levelFinished;
        private bool soundPlayed = false;

        public ScoreScreen(Game1 game, int score)
        {
            _game = game;
            this.score = score;
            highScore = LoadHighScore(); // Load the previous high score
        }

        public void LoadContent(ContentManager content)
        {
            scoreFont = content.Load<SpriteFont>("Fonts/ScoreFont");
            backgroundScore = content.Load<Texture2D>("Images/ScoreBG"); // Load background image
            displayScore = content.Load<Texture2D>("Images/ScoreBoard");
            levelFinished = content.Load<SoundEffect>("levelComplete");

            // Update high score only if the current score is greater
            if (score > highScore)
            {
                highScore = score; // Set the new high score
                SaveHighScore(highScore); // Save the new high score
            }

            // Play the level completion sound
            if (!soundPlayed)
            {
                levelFinished.Play();
                soundPlayed = true;
            }
        }

        public void Update(GameTime gameTime)
        {
            // Waiting for user input to return to the menu
            if (Keyboard.GetState().IsKeyDown(Keys.Enter))
            {
                _game.ShowMenu(); // Navigate back to the menu
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                _game.Exit();
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                _game.StartLevelTwo();
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(
                backgroundScore,
                new Rectangle(0, 0, _game.GraphicsDevice.Viewport.Width, _game.GraphicsDevice.Viewport.Height),
                Color.White
            );

            // Screen dimensions
            int screenWidth = _game.GraphicsDevice.Viewport.Width;
            int screenHeight = _game.GraphicsDevice.Viewport.Height;

            // ScoreBoard image
            int scoreBoardWidth = 800; 
            int scoreBoardHeight = 400;
            int scoreBoardX = (screenWidth - scoreBoardWidth) / 2;
            int scoreBoardY = screenHeight - 500; 

            // Drawing the ScoreBoard image
            spriteBatch.Draw(
                displayScore,
                new Rectangle(scoreBoardX, scoreBoardY, scoreBoardWidth, scoreBoardHeight),
                Color.White
            );

            string scoreText = $"Your Score: {score}";
            string highScoreText = $"High Score: {highScore}";
            string instructions = "Go Pro with Right Arrow, Back to menu\n with Enter or Exit with Escape";

            // Set padding inside the scoreboard for text alignment
            int paddingX = 50;
            int paddingY = 30;

            float maxTextWidth = Math.Max(scoreFont.MeasureString(scoreText).X,
                                         Math.Max(scoreFont.MeasureString(highScoreText).X, scoreFont.MeasureString(instructions).X));

            // Calculate text positions (aligned by the maximum width)
            float laneSpacing = scoreBoardHeight / 4f; // Divide the board into lanes
            Vector2 scorePosition = new Vector2(scoreBoardX + (scoreBoardWidth - maxTextWidth) / 2, scoreBoardY + laneSpacing - scoreFont.MeasureString(scoreText).Y / 2);
            Vector2 highScorePosition = new Vector2(scoreBoardX + (scoreBoardWidth - maxTextWidth) / 2, scoreBoardY + 2 * laneSpacing - scoreFont.MeasureString(highScoreText).Y / 2);
            Vector2 instructionsPosition = new Vector2(scoreBoardX + (scoreBoardWidth - maxTextWidth) / 2, scoreBoardY + 3 * laneSpacing - scoreFont.MeasureString(instructions).Y / 2);

            // Draw the texts inside the ScoreBoard
            spriteBatch.DrawString(scoreFont, scoreText, scorePosition, Color.Blue, 0f, Vector2.Zero, 2f, SpriteEffects.None, 0f);
            spriteBatch.DrawString(scoreFont, highScoreText, highScorePosition, Color.Red, 0f, Vector2.Zero, 1.5f, SpriteEffects.None, 0f);
            spriteBatch.DrawString(scoreFont, instructions, instructionsPosition, Color.Brown, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
        }

        private int LoadHighScore()
        {
            if (File.Exists(highScoreFilePath))
            {
                string content = File.ReadAllText(highScoreFilePath);
                if (int.TryParse(content, out int highScore))
                {
                    return highScore;
                }
            }

            return 0; // Default high score if no file exists
        }

        private void SaveHighScore(int highScore)
        {
            // Save only the numeric value as high score
            File.WriteAllText(highScoreFilePath, highScore.ToString());
        }
    }
}
