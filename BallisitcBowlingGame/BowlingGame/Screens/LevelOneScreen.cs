using BowlingGame.Services;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System;
using Microsoft.Xna.Framework.Audio;

namespace BowlingGame.Screens
{
    public class LevelOneScreen : IScreen
    {
        // Initialize game-related objects and assets
        private Game1 _game;
        private Texture2D bowlingLane, bowlingBallTexture, bowlingPinTexture;
        private Ball ball;
        private List<Pins> pins;
        private Score score;
        private CollisionHandler collisionHandler;

        private bool ballReleased, ballMovingHorizontally;
        private int attempt;

        // Sounds for rolling the ball and pin collision
        private SoundEffect rollingBallSound;
        private SoundEffectInstance rollingBallSoundInstance;
        private SoundEffect strikePinSound;

        public LevelOneScreen(Game1 game)
        {
            _game = game;
            ball = new Ball(new Vector2(700, GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height - 200));
            InitializePins();
            score = new Score();
            collisionHandler = new CollisionHandler();
            ballReleased = false;
            ballMovingHorizontally = true;
            attempt = 1;
        }

        public void LoadContent(ContentManager content)
        {
            // Load textures for lane, ball, and pins
            bowlingLane = content.Load<Texture2D>("Images/bowlingLane");
            bowlingBallTexture = content.Load<Texture2D>("Images/bowlingBall");
            bowlingPinTexture = content.Load<Texture2D>("Images/bowlingPin");

            // Load sounds
            rollingBallSound = content.Load<SoundEffect>("rollingBall");
            rollingBallSoundInstance = rollingBallSound.CreateInstance();
            strikePinSound = content.Load<SoundEffect>("strikePin");
        }

        public void Update(GameTime gameTime)
        {
            var keyboardState = Keyboard.GetState();
            var mouseState = Mouse.GetState();

            // Check if player exits to the menu
            if (keyboardState.IsKeyDown(Keys.Escape))
            {
                _game.ShowMenu();
                return;
            }

            // Ball movement before release (horizontal oscillation)
            if (!ballReleased && ballMovingHorizontally)
            {
                int laneStartX = 700;
                int laneEndX = _game.GraphicsDevice.Viewport.Width - 750;

                ball.Position = new Vector2(ball.Position.X + 5 * ball.Direction, ball.Position.Y);

                // Reverse direction at lane boundaries
                if (ball.Position.X >= laneEndX - ball.Radius || ball.Position.X <= laneStartX)
                {
                    ball.Direction *= -1;
                    ball.Position = new Vector2(
                        MathHelper.Clamp(ball.Position.X, laneStartX + ball.Radius, laneEndX - ball.Radius),
                        ball.Position.Y
                    );
                }

                // Release ball on spacebar or mouse click
                if (keyboardState.IsKeyDown(Keys.Space) || mouseState.LeftButton == ButtonState.Pressed)
                {
                    ballReleased = true;
                    ballMovingHorizontally = false;
                    ball.Velocity = new Vector2(0, -10);

                    // Play rolling sound
                    if (rollingBallSoundInstance.State != SoundState.Playing)
                    {
                        rollingBallSoundInstance.Play();
                    }
                }
            }

            if (ballReleased)
            {
                // Update ball movement and check for pin collisions
                ball.Update();

                if (ball.Velocity.Length() < 0.1f && ball.Position.Y > 0)
                {
                    ball.Velocity = new Vector2(0, -10);
                }

                int pinsKnocked = collisionHandler.HandleBallPinCollision(ball, pins, score);
                if (pinsKnocked > 0)
                {
                    // Play strike sound when pins are hit
                    strikePinSound.Play();
                    score.IncrementScore(pinsKnocked, attempt);
                }

                collisionHandler.HandlePinCollisions(pins);
                collisionHandler.UpdatePinPositions(pins);

                // Reset ball or end game after 3 attempts
                if (ball.Position.Y < 0 || ball.Velocity.Length() < 0.1f)
                {
                    if (pins.TrueForAll(pin => pin.IsKnockedDown))
                    {
                        ShowScoreScreen();
                    }
                    else if (attempt < 3)
                    {
                        attempt++;
                        ResetBall();
                    }
                    else
                    {
                        ShowScoreScreen();
                    }
                }
            }

            // Update pin positions
            foreach (var pin in pins)
            {
                pin.Update();
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            // Draw lane background
            spriteBatch.Draw(bowlingLane, new Rectangle(100, 0, _game.GraphicsDevice.Viewport.Width - 200, _game.GraphicsDevice.Viewport.Height), Color.White);

            // Draw ball
            spriteBatch.Draw(bowlingBallTexture, new Rectangle((int)ball.Position.X, (int)ball.Position.Y, 100, 100), Color.White);

            // Draw pins
            foreach (var pin in pins)
            {
                if (!pin.IsKnockedDown || pin.Velocity.Length() > 0.1f)
                {
                    spriteBatch.Draw(bowlingPinTexture, new Rectangle((int)pin.Position.X, (int)pin.Position.Y, 60, 60), Color.White);
                }
            }

            // Display level and score
            int laneStartX = 250;
            int levelPositionY = 50;
            int scoreBoxPositionY = levelPositionY + 70;

            string levelText = $"Level: {score.Level}";
            string scoreText = $"Score: {score.CurrentScore}";

            float levelScale = 1.3f;
            float scoreScale = 0.8f;

            SpriteFont font = _game.Content.Load<SpriteFont>("Fonts/levelFont");
            Vector2 levelSize = font.MeasureString(levelText) * levelScale;
            Vector2 levelPosition = new Vector2(laneStartX - levelSize.X / 2, levelPositionY);

            Vector2 shadowOffset = new Vector2(3, 3);
            spriteBatch.DrawString(font, levelText, levelPosition + shadowOffset, Color.White * 0.8f, 0f, Vector2.Zero, levelScale, SpriteEffects.None, 0f);
            spriteBatch.DrawString(font, levelText, levelPosition, Color.Black, 0f, Vector2.Zero, levelScale, SpriteEffects.None, 0f);

            // Draw score box
            Texture2D scoreBoxImage = _game.Content.Load<Texture2D>("Images/ScoreBox");
            int scoreBoxWidth = 250;
            int scoreBoxHeight = 100;
            Rectangle scoreBoxRectangle = new Rectangle(
                laneStartX - scoreBoxWidth / 2,
                scoreBoxPositionY,
                scoreBoxWidth,
                scoreBoxHeight
            );
            spriteBatch.Draw(scoreBoxImage, scoreBoxRectangle, Color.White);

            // Draw score text
            Vector2 scoreTextSize = font.MeasureString(scoreText) * scoreScale;
            Vector2 scoreTextPosition = new Vector2(
                scoreBoxRectangle.X + (scoreBoxRectangle.Width - scoreTextSize.X) / 2,
                scoreBoxRectangle.Y + (scoreBoxRectangle.Height - scoreTextSize.Y) / 2
            );
            spriteBatch.DrawString(font, scoreText, scoreTextPosition + shadowOffset, Color.White * 0.8f, 0f, Vector2.Zero, scoreScale, SpriteEffects.None, 0f);
            spriteBatch.DrawString(font, scoreText, scoreTextPosition, Color.SaddleBrown, 0f, Vector2.Zero, scoreScale, SpriteEffects.None, 0f);
        }

        // Reset the ball and pins for a new attempt
        public void Reset()
        {
            score = new Score();
            InitializePins();
            ResetBall();
            attempt = 1;
        }

        private void ResetBall()
        {
            ball.Reset(new Vector2(700, _game.GraphicsDevice.Viewport.Height - 200));
            ballReleased = false;
            ballMovingHorizontally = true;
            ball.Velocity = Vector2.Zero;
            ball.IsPerfectStrike = false;
        }

        private void ShowScoreScreen()
        {
            var scoreScreen = new ScoreScreen(_game, score.CurrentScore);
            scoreScreen.LoadContent(_game.Content);
            _game.currentScreen = scoreScreen;
        }

        private void InitializePins()
        {
            float startX = _game.GraphicsDevice.Viewport.Width / 2.049f;
            int startY = 50;
            int spacing = 85;

            // Set up pins in a triangular formation
            pins = new List<Pins>
            {
                new Pins(new Vector2(startX, startY)),

                new Pins(new Vector2(startX - spacing / 2, startY + spacing)),
                new Pins(new Vector2(startX + spacing / 2, startY + spacing)),

                new Pins(new Vector2(startX - spacing, startY + 2 * spacing)),
                new Pins(new Vector2(startX, startY + 2 * spacing)),
                new Pins(new Vector2(startX + spacing, startY + 2 * spacing)),

                new Pins(new Vector2(startX - 3 * spacing / 2, startY + 3 * spacing)),
                new Pins(new Vector2(startX - spacing / 2, startY + 3 * spacing)),
                new Pins(new Vector2(startX + spacing / 2, startY + 3 * spacing)),
                new Pins(new Vector2(startX + 3 * spacing / 2, startY + 3 * spacing))
            };
        }
    }
}
