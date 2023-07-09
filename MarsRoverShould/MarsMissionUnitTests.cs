using System;
using MarsRover.Models;
using NUnit.Framework;

namespace MarsMissionShould
{
	[TestFixture]
	public class MarsMissionUnitTests
	{
        MarsMission _mission;

		[SetUp]
		public void Setup()
		{
            var maxXCoordinate = 5;
            var maxYCoordinate = 5;
            _mission = new MarsMission(maxXCoordinate, maxYCoordinate);
		}

		[Test]
		public void MarsMission_Should_Not_Be_Null_When_Initialised()
		{
			Assert.That(_mission, Is.Not.Null);
		}

        [Test]
        public void MarsMission_Platau_Is_Not_Null_When_Initialised()
        {           
            _mission.CreatePlatau(5, 5);

            Assert.Multiple(() =>
            {
				Assert.That(_mission.Platau, Is.Not.Null);
                Assert.That(_mission.Platau.MaxXCoordinate, Is.EqualTo(5));
                Assert.That(_mission.Platau.MaxYCoordinate, Is.EqualTo(5));
            });
        }

        [Test]
        public void MarsMission_Player_Is_Not_Null_When_Initialised()
        {
            Assert.That(_mission.Player, Is.Not.Null);
        }

        [Test]
        public void MarsMission_Player_Can_Create_New_Rover()
        {
            _mission.CreateRover(3,2,'N');

            Assert.That(_mission.Player.Team[0], Is.Not.Null);
        }

        [TestCase(1,0,'W')]
        [TestCase(5,5,'E')]
        [TestCase(1, 5, 'S')]
        public void MarsMission_Player_Can_Create_New_Rover(int xCoordinate, int yCoordinate, char bearing)
        {
            _mission.CreateRover(xCoordinate, yCoordinate, bearing);

            var rover = _mission.Player.Team[0];

            Assert.Multiple(() =>
            {
                Assert.That(rover.Position.XCoordinate, Is.EqualTo(xCoordinate));
                Assert.That(rover.Position.YCoordinate, Is.EqualTo(yCoordinate));
                Assert.That(rover.Position.Bearing, Is.EqualTo(bearing));
            });
        }

        [Test]
        public void MarsMission_StartMission_()
        {
            Assert.That(_mission.Player, Is.Not.Null);
        }
    }
}