using BowlingGame.Services;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Audio;

namespace BowlingGame.Screens
{
    public class MenuScreen : IScreen
    {
        private Game1 _game;

        private SpriteFont menuFont; // Font for menu text
        private List<string> menuItems; // List of menu options
        private List<Texture2D> buttonBackgrounds; // Button textures
        private Texture2D menuBackground; // Background image for the menu

        private int selectedIndex; // Tracks currently selected menu item
        private KeyboardState previousKeyboardState; // Tracks keyboard state for detecting key presses

        private SoundEffect menuBgSound; // Background music for the menu
        private SoundEffectInstance menuBgInstance;

        private float elapsedTime; // Tracks time elapsed for potential animations

        public MenuScreen(Game1 game)
        {
            _game = game;

            // Initialize menu items
            menuItems = new List<string> { "Roll First Strike", "Step Up to Pro", "Strike Playbook", "Know the Alley", "Quit the Lanes" };

            selectedIndex = 0;
            buttonBackgrounds = new List<Texture2D>();
            elapsedTime = 0;
        }

        public void LoadContent(ContentManager content)
        {
            // Load fonts, images, and sounds
            menuFont = content.Load<SpriteFont>("Fonts/MenuFont");
            menuBackground = content.Load<Texture2D>("Images/levelBG");

            // Load menu background music
            menuBgSound = content.Load<SoundEffect>("backgroundSound");
            menuBgInstance = menuBgSound.CreateInstance();
            menuBgInstance.IsLooped = true; // Set background music to loop

            // Load button background images
            buttonBackgrounds.Add(content.Load<Texture2D>("Images/butttonsMenu"));
            buttonBackgrounds.Add(content.Load<Texture2D>("Images/HelpButtons"));
            buttonBackgrounds.Add(content.Load<Texture2D>("Images/butttonsMenu"));
            buttonBackgrounds.Add(content.Load<Texture2D>("Images/HelpButtons"));
            buttonBackgrounds.Add(content.Load<Texture2D>("Images/ExitButtons"));
        }

        public void Update(GameTime gameTime)
        {
            elapsedTime += (float)gameTime.ElapsedGameTime.TotalSeconds;

            KeyboardState currentKeyboardState = Keyboard.GetState();

            // Navigate menu using arrow keys
            if (IsKeyPressed(currentKeyboardState, Keys.Down))
            {
                selectedIndex = (selectedIndex + 1) % menuItems.Count;
            }
            else if (IsKeyPressed(currentKeyboardState, Keys.Up))
            {
                selectedIndex = (selectedIndex - 1 + menuItems.Count) % menuItems.Count;
            }
            else if (IsKeyPressed(currentKeyboardState, Keys.Enter))
            {
                HandleSelection(); // Handle selected menu option
            }

            previousKeyboardState = currentKeyboardState;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            // Play background music if not already playing
            if (menuBgInstance.State != SoundState.Playing)
            {
                menuBgInstance.Play();
            }

            // Draw the menu background
            spriteBatch.Draw(menuBackground, new Rectangle(0, 0, _game.GraphicsDevice.Viewport.Width, _game.GraphicsDevice.Viewport.Height), Color.White);

            float totalHeight = (menuItems.Count * 80) + ((menuItems.Count - 1) * 75); // Total height of menu options
            float startY = (_game.GraphicsDevice.Viewport.Height - totalHeight) / 2; // Start drawing vertically centered

            Vector2 position = new Vector2(_game.GraphicsDevice.Viewport.Width / 2, startY);

            float lineSpacing = 70f;
            float scaleFactor = 1.5f;

            // Draw each menu option
            for (int i = 0; i < menuItems.Count; i++)
            {
                Color textColor = (i == selectedIndex) ? Color.Yellow : Color.White; // Highlight selected option

                Vector2 textSize = menuFont.MeasureString(menuItems[i]);

                // Define button background bounds
                Rectangle buttonBounds = new Rectangle(
                    (int)(position.X - ((textSize.X * scaleFactor + 40) / 2)),
                    (int)position.Y - 10,
                    (int)(textSize.X * scaleFactor + 40),
                    (int)(textSize.Y * scaleFactor + 20)
                );

                // Draw button background or fallback to default
                if (i >= buttonBackgrounds.Count)
                {
                    spriteBatch.Draw(menuBackground, buttonBounds, new Color(0, 0, 0, 0.5f));
                }
                else
                {
                    spriteBatch.Draw(buttonBackgrounds[i], buttonBounds, Color.White);
                }

                // Draw the menu text
                Vector2 textPosition = new Vector2(
                    position.X - textSize.X / 2,
                    position.Y
                );
                spriteBatch.DrawString(menuFont, menuItems[i], textPosition, textColor);

                position.Y += (80 + lineSpacing); // Move to the next menu option
            }
        }

        private void HandleSelection()
        {
            // Handle actions based on selected menu option
            switch (menuItems[selectedIndex])
            {
                case "Roll First Strike":
                    menuBgInstance.Stop();
                    _game.StartLevelOne();
                    break;
                case "Step Up to Pro":
                    menuBgInstance.Stop();
                    _game.StartLevelTwo();
                    break;
                case "Strike Playbook":
                    menuBgInstance.Stop();
                    _game.ShowHelp();
                    break;
                case "Know the Alley":
                    menuBgInstance.Stop();
                    _game.ShowAboutScreen();
                    break;
                case "Quit the Lanes":
                    menuBgInstance.Stop();
                    _game.Exit();
                    break;
            }
        }

        private bool IsKeyPressed(KeyboardState currentKeyboardState, Keys key)
        {
            // Check if a key was pressed in the current update cycle
            return currentKeyboardState.IsKeyDown(key) && !previousKeyboardState.IsKeyDown(key);
        }
    }
}
