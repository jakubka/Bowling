using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Bowling.Domain;

namespace Bowling.Console
{
    public class BowlingLineParser
    {
        // capture in frames group can be for example: 52, 5/, 5-. --, X
        // capture in bonus group can be for example: 5, -, X
        private static readonly Regex InputRegex =
            new Regex(@"^((?<frames>[1-9-]{2}|[1-9-]\/|X)\|){10}\|(?<bonus>[1-9-X]){0,2}$",
                RegexOptions.ExplicitCapture | RegexOptions.Compiled | RegexOptions.Singleline);

        public Line ParseLine(
            string input)
        {
            if (input == null)
            {
                throw new ArgumentNullException(nameof(input));
            }

            var match = InputRegex.Match(input);

            if (!match.Success)
            {
                throw new ParseException("Input is in invalid format");
            }

            try
            {
                var frames = 
                    CreateFrames(match.Groups["frames"])
                        .ToList();

                var bonusBalls = 
                    CreateBonusBalls(match.Groups["bonus"])
                        .ToList();

                return new Line(frames, bonusBalls);
            }
            catch (Exception e)
            {
                throw new ParseException($"Error while parsing input line: {e.Message}");
            }
        }

        private IEnumerable<Frame> CreateFrames(
            Group framesGroup)
        {
            return 
                from Capture capture in framesGroup.Captures
                select CreateFrame(capture.Value);
        }

        private static Frame CreateFrame(
            string frameStringRepresentation)
        {
            if (frameStringRepresentation == "X")
            {
                return Frame.CreateStrike();
            }

            var ball1 = CreateBall(frameStringRepresentation[0]);

            if (frameStringRepresentation[1] == '/')
            {
                return Frame.CreateSpare(ball1);
            }

            var ball2 = CreateBall(frameStringRepresentation[1]);

            return Frame.Create(ball1, ball2);
        }

        private IEnumerable<Ball> CreateBonusBalls(
            Group bonusBallsGroup)
        {
            return 
                from Capture capture in bonusBallsGroup.Captures
                let value = capture.Value[0]
                select value == 'X'
                    ? Ball.CreateStrike()
                    : CreateBall(capture.Value[0]);
        }

        private static Ball CreateBall(
            char ballCharRepresentation)
        {
            return ballCharRepresentation == '-'
                ? Ball.Create(0)
                : Ball.Create((int) char.GetNumericValue(ballCharRepresentation));
        }
    }
}