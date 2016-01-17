using System;
using FluentAssertions;
using NUnit.Framework;

namespace Bowling.Domain.Tests
{
    [TestFixture]
    public class BallTests
    {
        [TestCase(-1)]
        [TestCase(10)]
        [TestCase(15)]
        public void Create_ShouldFailWhenNumberOfKnockedPinsIsNotValid(
            int numberOfKnockedDownPins)
        {
            Action call = () => Ball.Create(numberOfKnockedDownPins);

            call.ShouldThrow<ArgumentException>();
        }

        [Test]
        public void Create_ShouldCreateABallWhichIsNotSpareNorStrike()
        {
            var ball = Ball.Create(5);

            ball.IsSpare.Should().BeFalse();
            ball.IsStrike.Should().BeFalse();
            ball.PinsKnockedDown.Should().Be(5);
        }

        [TestCase(-5)]
        [TestCase(0)]
        [TestCase(11)]
        [TestCase(15)]
        public void CreateSpare_ShouldFailWhenNumberOfKnockedPinsIsNotValid(
            int numberOfKnockedDownPins)
        {
            Action call = () => Ball.CreateSpare(numberOfKnockedDownPins);

            call.ShouldThrow<ArgumentException>();
        }

        [Test]
        public void CreateSpare_ShouldCreateABallWhichIsSpare()
        {
            var ball = Ball.CreateSpare(5);

            ball.IsSpare.Should().BeTrue();
            ball.IsStrike.Should().BeFalse();
            ball.PinsKnockedDown.Should().Be(5);
        }

        [Test]
        public void CreateStrike_ShouldCreateABallWith10PinsKnockedDown()
        {
            var ball = Ball.CreateStrike();

            ball.IsSpare.Should().BeFalse();
            ball.IsStrike.Should().BeTrue();
            ball.PinsKnockedDown.Should().Be(10);
        }
    }
}