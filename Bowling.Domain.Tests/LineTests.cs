using System;
using System.Collections.Generic;
using System.Linq;
using Bowling.Console;
using FluentAssertions;
using NUnit.Framework;

namespace Bowling.Domain.Tests
{
    [TestFixture]
    public class LineTests
    {
        [TestCase("X|X|X|X|X|X|X|X|X|X||XX", 300)]
        [TestCase("9-|9-|9-|9-|9-|9-|9-|9-|9-|9-||", 90)]
        [TestCase("5/|5/|5/|5/|5/|5/|5/|5/|5/|5/||5", 150)]
        [TestCase("X|7/|9-|X|-8|8/|-6|X|X|X||81", 167)]
        public void CalculateTotalScore_ShouldCalculateScoreCorrectly(
            string lineStringSpecification,
            int expectedScore)
        {
            var parser = new BowlingLineParser();

            var line = parser.ParseLine(lineStringSpecification);

            line
                .CalculateTotalScore()
                .Should()
                .Be(expectedScore, $"because total score of line {lineStringSpecification} should be {expectedScore}");
        }


        [TestCase(0)]
        [TestCase(9)]
        [TestCase(11)]
        public void Constructor_ShouldThrowWhenNumberOfFramesIsNotCorrect(
            int numberOfFrames)
        {
            var frames =
                Enumerable
                    .Repeat(Frame.Create(Ball.Create(3), Ball.Create(3)), numberOfFrames)
                    .ToList();

            var bonusBalls = new List<Ball>();

            Action call = () => new Line(frames, bonusBalls);

            call.ShouldThrow<ArgumentException>();
        }

        [Test]
        public void Constructor_ShouldThrowWhenThereIsNotEnoughBonusBallsWhanListFrameIsSpare()
        {
            var frames =
                Enumerable
                    .Repeat(Frame.CreateSpare(Ball.Create(3)), 10)
                    .ToList();

            var bonusBalls = new List<Ball>();

            Action call = () => new Line(frames, bonusBalls);

            call.ShouldThrow<ArgumentException>();
        }

        [Test]
        public void Constructor_ShouldThrowWhenThereIsNotEnoughBonusBallsWhenLastFrameIsStrike()
        {
            var frames =
                Enumerable
                    .Repeat(Frame.CreateStrike(), 10)
                    .ToList();

            var bonusBalls = new List<Ball>
            {
                Ball.Create(5)
            };

            Action call = () => new Line(frames, bonusBalls);

            call.ShouldThrow<ArgumentException>();
        }
    }
}