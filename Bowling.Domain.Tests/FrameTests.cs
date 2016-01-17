using System;
using FluentAssertions;
using NUnit.Framework;

namespace Bowling.Domain.Tests
{
    [TestFixture]
    public class FrameTests
    {
        [Test]
        public void Create_ShouldCreateFrame()
        {
            var frame = Frame.Create(Ball.Create(3), Ball.Create(4));

            frame.Ball1.PinsKnockedDown.Should().Be(3);
            frame.Ball2.PinsKnockedDown.Should().Be(4);
            frame.IsSpare.Should().BeFalse();
            frame.IsStrike.Should().BeFalse();
        }

        [Test]
        public void Create_ShouldFailWhenTotalNumberOfKnockedDownPinsIsMoreThan9()
        {
            Action call = () => Frame.Create(Ball.Create(5), Ball.Create(5));

            call.ShouldThrow<ArgumentException>();
        }

        [Test]
        public void CreateSpare_ShouldCreateSpareFrame()
        {
            var frame = Frame.CreateSpare(Ball.Create(3));

            frame.Ball1.PinsKnockedDown.Should().Be(3);
            frame.Ball2.PinsKnockedDown.Should().Be(7);
            frame.Ball2.IsSpare.Should().BeTrue();
            frame.IsSpare.Should().BeTrue();
            frame.IsStrike.Should().BeFalse();
        }

        [Test]
        public void CreateStrike_ShouldCreateStrikeFrame()
        {
            var frame = Frame.CreateStrike();

            frame.Ball1.PinsKnockedDown.Should().Be(10);
            frame.Ball1.IsStrike.Should().BeTrue();
            frame.Ball2.Should().BeNull();
            frame.IsSpare.Should().BeFalse();
            frame.IsStrike.Should().BeTrue();
        }
    }
}