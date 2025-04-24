using Microsoft.Xna.Framework;

namespace BowlingGame
{
    internal class Ball
    {
        public Vector2 Position { get; set; }
        public Vector2 Velocity { get; set; }
        public float Radius { get; private set; }
        public int Direction { get; set; }
        public bool IsPerfectStrike { get; set; }

        public Ball(Vector2 startPosition)
        {
            Position = startPosition;
            Velocity = Vector2.Zero;
            Radius = 50;
            Direction = 1;
            IsPerfectStrike = false;
        }

        public void Update()
        {
            Position += Velocity;
        }

        public void Reset(Vector2 startPosition)
        {
            Position = startPosition;
            Velocity = Vector2.Zero;
            IsPerfectStrike = false;
        }
    }
}
