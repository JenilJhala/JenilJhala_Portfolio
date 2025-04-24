using BowlingGame.Services;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Audio;

namespace BowlingGame.Screens
{
    public class LevelTwoScreen : IScreen
    {
        // Initializing main things
        private Game1 _game;

        private Texture2D bowlingLane, bowlingBallTexture, bowlingPinTexture;

        private Ball ball;
        private List<Pins> pins;
        private Score score;
        private CollisionHandler collisionHandler;

        private bool ballReleased, ballMovingHorizontally;
        private int attempt;
        private float laneGlowIntensity;
        private float laneGlowDirection;

        // Sound effect fields
        private SoundEffect rollingBallSound;
        private SoundEffectInstance rollingBallSoundInstance;
        private SoundEffect strikePinSound;

        // Constructor
        public LevelTwoScreen(Game1 game)
        {
            _game = game;
            ball = new Ball(new Vector2(700, GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height - 200));
            InitializePins();
            score = new Score();
            collisionHandler = new CollisionHandler();
            ballReleased = false;
            ballMovingHorizontally = true;
            attempt = 1;

            laneGlowIntensity = 0.5f;
            laneGlowDirection = 0.01f; // Controls glow speed
        }

        public void LoadContent(ContentManager content)
        {
            // Loading the files
            bowlingLane = content.Load<Texture2D>("Images/bowlingLane");
            bowlingBallTexture = content.Load<Texture2D>("Images/bowlingBall");
            bowlingPinTexture = content.Load<Texture2D>("Images/bowlingPin");

            // Load sound effects
            rollingBallSound = content.Load<SoundEffect>("rollingBall");
            rollingBallSoundInstance = rollingBallSound.CreateInstance();

            strikePinSound = content.Load<SoundEffect>("strikePin");
        }

        public void Update(GameTime gameTime)
        {
            var keyboardState = Keyboard.GetState();
            var mouseState = Mouse.GetState();

            if (keyboardState.IsKeyDown(Keys.Escape))
            {
                _game.ShowMenu();
                return;
            }

            // Lane starts glowing using animations
            UpdateLaneGlow();

            // Positioning the ball and the pins when the ball is not released yet
            if (!ballReleased && ballMovingHorizontally)
            {
                int laneStartX = 700;
                int laneEndX = _game.GraphicsDevice.Viewport.Width - 750;

                ball.Position = new Vector2(ball.Position.X + 7 * ball.Direction, ball.Position.Y);

                if (ball.Position.X >= laneEndX - ball.Radius || ball.Position.X <= laneStartX)
                    ball.Direction *= -1;

                if (keyboardState.IsKeyDown(Keys.Space) || mouseState.LeftButton == ButtonState.Pressed)
                {
                    ballReleased = true;
                    ballMovingHorizontally = false;

                    // Adding stronger wind deviation for level 2
                    float deviation = (float)new Random().NextDouble() * 6 - 3;
                    ball.Velocity = new Vector2(deviation, -14);

                    // Play the rolling ball sound
                    if (rollingBallSoundInstance.State != SoundState.Playing)
                    {
                        rollingBallSoundInstance.Play();
                    }
                }
            }

            if (ballReleased)
            {
                ball.Update();

                // Handle collision and play sound when ball hits pins
                int pinsKnocked = collisionHandler.HandleBallPinCollision(ball, pins, score);
                if (pinsKnocked > 0)
                {
                    // Play strike pin sound
                    strikePinSound.Play();

                    score.IncrementScore(pinsKnocked, attempt);
                }

                collisionHandler.HandlePinCollisions(pins);
                collisionHandler.UpdatePinPositions(pins);

                if (ball.Position.Y < 0 || ball.Velocity.Length() < 0.1f)
                {
                    if (pins.TrueForAll(pin => pin.IsKnockedDown))
                    {
                        if (collisionHandler.IsPerfectStrike(ball, pins))
                        {
                            score.IncrementScore(500, attempt);
                        }

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

            foreach (var pin in pins)
            {
                pin.Update();

                if (pin.IsKnockedDown)
                {
                    pin.Position += new Vector2((float)Math.Sin(gameTime.TotalGameTime.TotalSeconds * 10) * 2, 0); // Horizontal wobble
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(bowlingLane, new Rectangle(100, 0, _game.GraphicsDevice.Viewport.Width - 200, _game.GraphicsDevice.Viewport.Height), Color.White * laneGlowIntensity);

            spriteBatch.Draw(bowlingBallTexture, new Rectangle((int)ball.Position.X, (int)ball.Position.Y, 100, 100), Color.White);

            foreach (var pin in pins)
            {
                if (!pin.IsKnockedDown || pin.Velocity.Length() > 0.1f)
                {
                    spriteBatch.Draw(bowlingPinTexture, new Rectangle((int)pin.Position.X, (int)pin.Position.Y, 60, 60), Color.White);
                }
            }

            int laneStartX = 250; 
            int levelPositionY = 50;
            int scoreBoxPositionY = levelPositionY + 70;

            string levelText = "Level: 2";
            string scoreText = $"Score: {score.CurrentScore}";

            float levelScale = 1.3f;
            float scoreScale = 0.8f;

            SpriteFont font = _game.Content.Load<SpriteFont>("Fonts/levelFont");
            Vector2 levelSize = font.MeasureString(levelText) * levelScale;

            Vector2 levelPosition = new Vector2(laneStartX - levelSize.X / 2, levelPositionY);

            Vector2 shadowOffset = new Vector2(3, 3);
            spriteBatch.DrawString(font, levelText, levelPosition + shadowOffset, Color.White * 0.8f, 0f, Vector2.Zero, levelScale, SpriteEffects.None, 0f);
            spriteBatch.DrawString(font, levelText, levelPosition, Color.Black, 0f, Vector2.Zero, levelScale, SpriteEffects.None, 0f);

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

            Vector2 scoreTextSize = font.MeasureString(scoreText) * scoreScale;
            Vector2 scoreTextPosition = new Vector2(
                scoreBoxRectangle.X + (scoreBoxRectangle.Width - scoreTextSize.X) / 2,
                scoreBoxRectangle.Y + (scoreBoxRectangle.Height - scoreTextSize.Y) / 2
            );

            spriteBatch.DrawString(font, scoreText, scoreTextPosition + shadowOffset, Color.White * 0.8f, 0f, Vector2.Zero, scoreScale, SpriteEffects.None, 0f);
            spriteBatch.DrawString(font, scoreText, scoreTextPosition, Color.SaddleBrown, 0f, Vector2.Zero, scoreScale, SpriteEffects.None, 0f);
        }

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
            int spacing = 140; 

            // Lining up the pins in 4 different rows
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

        private void UpdateLaneGlow()
        {
            laneGlowIntensity += laneGlowDirection;

            if (laneGlowIntensity >= 1.0f || laneGlowIntensity <= 0.5f)
            {
                laneGlowDirection *= -1;
            }
        }
    }
}
