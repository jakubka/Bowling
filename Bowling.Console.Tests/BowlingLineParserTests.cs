using System;
using System.Collections.Generic;
using Bowling.Domain;
using FluentAssertions;
using NUnit.Framework;

namespace Bowling.Console.Tests
{
    [TestFixture]
    public class BowlingLineParserTests
    {
        [Test]
        public void ParseLine_ShouldParseInputCorrectly()
        {
            var input = "X|7/|9-|X|-8|8/|-6|--|X|X||81";

            var parser = new BowlingLineParser();

            var line = parser.ParseLine(input);

            var expectedFrames = new List<Frame>
            {
                Frame.CreateStrike(),
                Frame.CreateSpare(Ball.Create(7)),
                Frame.Create(Ball.Create(9), Ball.Create(0)),
                Frame.CreateStrike(),
                Frame.Create(Ball.Create(0), Ball.Create(8)),
                Frame.CreateSpare(Ball.Create(8)),
                Frame.Create(Ball.Create(0), Ball.Create(6)),
                Frame.Create(Ball.Create(0), Ball.Create(0)),
                Frame.CreateStrike(),
                Frame.CreateStrike()
            };

            var expectedBonusBalls = new List<Ball>
            {
                Ball.Create(8),
                Ball.Create(1)
            };

            var expectedLine = new Line(expectedFrames, expectedBonusBalls);

            line.ShouldBeEquivalentTo(expectedLine);
        }

        [TestCase("")]
        [TestCase("X|X|X|X|X|X|X|X|X||XX")] // only 9 frames
        [TestCase("X|X|X|X|X|X|X|X|X|X||X")] // not enough bonus balls
        [TestCase("X|X|X|X|X|X|X|X|X|X||")] // not enough bonus balls
        [TestCase("X|X|X|X|X|X|X|X|X|5/||")] // not enough bonus balls
        [TestCase("X|X|X|X|X|X|X|X|X|X||XXX")] // too many bonus balls
        [TestCase("X5|X|X|X|X|X|X|X|X|X||XX")] // number after strike
        [TestCase("X|X|X|y|X|X|X|X|X|X||XX")] // invalid ball
        [TestCase("X|X|X|x|X|X|X|X|X|X||XX")] // invalid ball
        [TestCase("X|X|X|X|XX|X|X|X|X|X||XX")] // two strikes
        [TestCase("X|X|X|X|222|X|X|X|X|X||XX")] // three balls in one frame
        [TestCase("X|X|X|X|88|X|X|X|X|X||XX")] // more than 10 pins
        [TestCase("X|X|X|X|X|X|X|X|X|X|X||XX")] // 11 frames
        public void ParseLine_ShouldThrowExceptionWhenInputIsNotValid(
            string input)
        {
            var parser = new BowlingLineParser();

            Action call = () => parser.ParseLine(input);

            call.ShouldThrow<ParseException>();
        }
    }
}