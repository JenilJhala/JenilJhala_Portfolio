using Microsoft.Xna.Framework;

namespace BowlingGame
{
    internal class Pins
    {
        public Vector2 Position { get; set; } // Current position of the pin
        public Vector2 OriginalPosition { get; private set; } // Original position before the pin was knocked
        public bool IsKnockedDown { get; set; } // Tracks whether the pin is knocked down
        public Vector2 Velocity { get; set; } // Current velocity of the pin (for motion after being hit)
        public float Radius { get; private set; } = 30; // Radius of the pin for collision detection

        public Pins(Vector2 startPosition)
        {
            Position = startPosition;
            OriginalPosition = startPosition; // Save initial position
            IsKnockedDown = false; // Start as standing
            Velocity = Vector2.Zero; // Start with no movement
        }

        public void KnockDown()
        {
            IsKnockedDown = true;

            // Assign random velocity to simulate a realistic knock-down effect
            Velocity = new Vector2(
                (float)(-7 + 14 * RandomHelper.NextDouble()), // Random horizontal movement
                (float)(-5 - 10 * RandomHelper.NextDouble())  // Random vertical movement
            );
        }

        public void Reset(Vector2 startPosition)
        {
            // Reset position and state of the pin
            Position = startPosition;
            IsKnockedDown = false;
            Velocity = Vector2.Zero;
        }

        public void Update()
        {
            if (IsKnockedDown)
            {
                Position += Velocity; // Move pin based on its velocity
                Velocity *= 0.95f; // Gradually slow down the velocity
            }
        }

        public void HandleCollision(Pins otherPin)
        {
            // Only handle collisions if both pins are knocked down
            if (!IsKnockedDown || !otherPin.IsKnockedDown) return;

            float distance = Vector2.Distance(Position, otherPin.Position); // Calculate distance between the two pins

            if (distance < Radius * 2) // Check if the pins are overlapping
            {
                // Swap velocities to simulate collision effect
                Vector2 temp = Velocity;
                Velocity = otherPin.Velocity;
                otherPin.Velocity = temp;

                // Resolve overlapping by pushing pins apart
                Vector2 overlap = Vector2.Normalize(Position - otherPin.Position) * (Radius * 2 - distance);
                Position += overlap / 2;
                otherPin.Position -= overlap / 2;
            }
        }
    }

    internal static class RandomHelper
    {
        private static readonly System.Random Random = new System.Random();

        // Generate a random double value
        public static double NextDouble()
        {
            return Random.NextDouble();
        }
    }
}
