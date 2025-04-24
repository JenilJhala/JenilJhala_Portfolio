using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace BowlingGame.Services
{
    internal class CollisionHandler
    {
        // Handling ball and pin collision like if the ball hits the pin then pin is been knocked
        public int HandleBallPinCollision(Ball ball, List<Pins> pins, Score score)
        {
            int pinsKnocked = 0;
            foreach (var pin in pins)
            {
                if (!pin.IsKnockedDown && Vector2.Distance(ball.Position, pin.Position) < pin.Radius + ball.Radius)
                {
                    pin.KnockDown();
                    pin.Velocity = ball.Velocity * 0.8f;
                    pinsKnocked++;
                }
            }
            return pinsKnocked;
        }

        // Handling the pin collision like if the pin has been collided with the ball then it will move for some time and then can knock pins around it
        public void HandlePinCollisions(List<Pins> pins)
        {
            for (int i = 0; i < pins.Count; i++)
            {
                for (int j = i + 1; j < pins.Count; j++)
                {
                    Pins pin1 = pins[i];
                    Pins pin2 = pins[j];

                    if (!pin1.IsKnockedDown && !pin2.IsKnockedDown)
                    {
                        float distance = Vector2.Distance(pin1.Position, pin2.Position);
                        if (distance < pin1.Radius + pin2.Radius)
                        {
                            Vector2 overlap = Vector2.Normalize(pin1.Position - pin2.Position) * (pin1.Radius + pin2.Radius - distance);
                            pin1.Position += overlap / 2;
                            pin2.Position -= overlap / 2;

                            Vector2 tempVelocity = pin1.Velocity;
                            pin1.Velocity = pin2.Velocity * 0.8f;
                            pin2.Velocity = tempVelocity * 0.8f;
                        }
                    }
                }
            }
        }

        public void UpdatePinPositions(List<Pins> pins, float friction = 0.98f)
        {
            foreach (var pin in pins)
            {
                if (!pin.IsKnockedDown)
                {
                    pin.Position += pin.Velocity;
                    pin.Velocity *= friction;
                }
            }
        }

        // Calculation for strike, if the ball has been hit by perfect time and place then it will make the strike and the player will gain 500 points
        public bool IsPerfectStrike(Ball ball, List<Pins> pins)
        {
            float screenWidthCenter = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width / 2;
            bool ballReleasedFromCenter = Math.Abs(ball.Position.X - screenWidthCenter) < 20;

            return ballReleasedFromCenter && pins.TrueForAll(pin => pin.IsKnockedDown);
        }
    }
}
