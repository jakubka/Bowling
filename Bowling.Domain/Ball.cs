using System;

namespace Bowling.Domain
{
    public class Ball
    {
        private Ball(
            int pinsKnockedDown,
            bool isSpare,
            bool isStrike)
        {
            PinsKnockedDown = pinsKnockedDown;
            IsSpare = isSpare;
            IsStrike = isStrike;
        }

        public int PinsKnockedDown { get; }

        public bool IsSpare { get; }

        public bool IsStrike { get; }

        public static Ball Create(
            int pinsKnockedDown)
        {
            if (pinsKnockedDown < 0 || pinsKnockedDown > 9)
            {
                throw new ArgumentException("A classic ball (not spare or strike) can knock down from 0 to 9 pins");
            }

            return new Ball(pinsKnockedDown, false, false);
        }

        public static Ball CreateSpare(
            int pinsKnockedDown)
        {
            if (pinsKnockedDown < 1 || pinsKnockedDown > 10)
            {
                throw new ArgumentException("A spare ball can knock down from 1 to 10 pins");
            }

            return new Ball(pinsKnockedDown, true, false);
        }

        public static Ball CreateStrike() => new Ball(10, false, true);
    }
}