using System;
using System.Collections.Generic;
using System.Linq;

namespace Bowling.Domain
{
    public class Line
    {
        public Line(
            IReadOnlyList<Frame> frames,
            IReadOnlyList<Ball> bonusBalls)
        {
            if (frames == null)
            {
                throw new ArgumentNullException(nameof(frames));
            }
            if (bonusBalls == null)
            {
                throw new ArgumentNullException(nameof(bonusBalls));
            }

            if (frames.Count != 10)
            {
                throw new ArgumentException("A line must be composed of 10 frames");
            }
            if (frames.Last().IsStrike && bonusBalls.Count != 2)
            {
                throw new ArgumentException("Two bonus balls are required when last frame is strike");
            }
            if (frames.Last().IsSpare && bonusBalls.Count != 1)
            {
                throw new ArgumentException("One bonus ball is required when last frame is spare");
            }

            Frames = frames;
            BonusBalls = bonusBalls;
        }

        public IReadOnlyList<Frame> Frames { get; }

        public IReadOnlyList<Ball> BonusBalls { get; }

        public int CalculateTotalScore()
        {
            var previousBall = BonusBalls.Count > 0 ? BonusBalls[0] : null;
            var ballBeforePrevious = BonusBalls.Count > 1 ? BonusBalls[1] : null;

            var totalScore = 0;

            foreach (var ball in GetAllBalls().Reverse())
            {
                totalScore += ball.PinsKnockedDown;

                if (ball.IsSpare)
                {
                    totalScore += previousBall.PinsKnockedDown;
                }
                else if (ball.IsStrike)
                {
                    totalScore += previousBall.PinsKnockedDown;
                    totalScore += ballBeforePrevious.PinsKnockedDown;
                }

                ballBeforePrevious = previousBall;
                previousBall = ball;
            }

            return totalScore;
        }

        private IEnumerable<Ball> GetAllBalls()
        {
            foreach (var frame in Frames)
            {
                yield return frame.Ball1;
                if (frame.Ball2 != null)
                {
                    yield return frame.Ball2;
                }
            }
        }
    }
}