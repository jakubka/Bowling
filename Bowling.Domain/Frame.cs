using System;

namespace Bowling.Domain
{
    public class Frame
    {
        private Frame()
        {
        }

        public Ball Ball1 { get; private set; }

        /// <summary>
        /// Is null when Frame is strike.
        /// </summary>
        public Ball Ball2 { get; private set; }

        public bool IsStrike => Ball1.IsStrike;

        public bool IsSpare => Ball2 != null && Ball2.IsSpare;

        public static Frame Create(
            Ball ball1,
            Ball ball2)
        {
            if (ball1 == null)
            {
                throw new ArgumentNullException(nameof(ball1));
            }
            if (ball2 == null)
            {
                throw new ArgumentNullException(nameof(ball2));
            }

            if (ball1.IsSpare || ball1.IsStrike || ball2.IsSpare || ball2.IsStrike)
            {
                throw new ArgumentException("Not spare or strike frame cannot be composed of spare or strike balls");
            }
            if (ball1.PinsKnockedDown + ball2.PinsKnockedDown > 9)
            {
                throw new ArgumentException("Not spare or strike frame must knock 9 pins at most");
            }

            return new Frame
            {
                Ball1 = ball1,
                Ball2 = ball2,
            };
        }

        public static Frame CreateSpare(
            Ball ball1)
        {
            if (ball1 == null)
            {
                throw new ArgumentNullException(nameof(ball1));
            }

            if (ball1.IsSpare || ball1.IsStrike)
            {
                throw new ArgumentException("First ball in spare frame cannot be spare or strike ball");
            }

            return new Frame
            {
                Ball1 = ball1,
                Ball2 = Ball.CreateSpare(10 - ball1.PinsKnockedDown),
            };
        }

        public static Frame CreateStrike() => new Frame
        {
            Ball1 = Ball.CreateStrike(),
            Ball2 = null,
        };
    }
}