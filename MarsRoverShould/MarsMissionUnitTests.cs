using System;
using MarsRover.Models;
using NUnit.Framework;

namespace MarsMissionShould
{
	[TestFixture]
	public class MarsMissionUnitTests
	{
        MarsMission _mission;
        private readonly int _teamLimit = 5;
        private readonly int _instructionLimit = 10000;
        private readonly int _maxXCoordinate = 5;
        private readonly int _maxYCoordinate = 5;

        [SetUp]
		public void Setup()
		{
            _mission = new MarsMission(_maxXCoordinate, _maxYCoordinate, _teamLimit, _instructionLimit);
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

        [Test]
        public void MarsMission_Player_Can_Create_Multiple_New_Rovers()
        {
            _mission.CreateRover(3, 2, 'N');
            _mission.CreateRover(3, 2, 'N');

            Assert.That(_mission.Player.Team.Count(), Is.EqualTo(2));
        }

        [TestCase(10, 6)]
        [TestCase(6, 6)]
        [TestCase(1, 1)]
        public void MarsMission_Player_Can_Not_Create_More_Than_X_Number_Of_New_Rovers(int limit, int expectedCount)
        {
            var maxCoord = 5;
            _mission = new MarsMission(maxCoord, maxCoord, limit, _instructionLimit);

            _mission.CreateRover(3, 2, 'N');
            _mission.CreateRover(3, 2, 'N');
            _mission.CreateRover(3, 2, 'N');
            _mission.CreateRover(3, 2, 'N');
            _mission.CreateRover(3, 2, 'N');
            _mission.CreateRover(3, 2, 'N');

            Assert.That(_mission.Player.Team.Count(), Is.EqualTo(expectedCount));
        }

        [TestCase(0, 6)]
        [TestCase(-1, 0)]
        [TestCase(6, 0)]
        [TestCase(0, -1)]
        public void MarsMission_Player_Can_Not_Create_New_Rovers_Outside_The_Bounds_Of_The_Platau(int xCoord, int yCoord)
        {
            var maxCoord = 5;
            _mission = new MarsMission(maxCoord, maxCoord, _teamLimit, _instructionLimit);

            Assert.Throws<ArgumentException>(() => _mission.CreateRover(xCoord, yCoord, 'N'));
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


        [TestCase(0, 0, 'E', 1, 0, 'E')]
        [TestCase(5, 5, 'W', 4, 5, 'W')]
        [TestCase(1, 1, 'S', 1, 0, 'S')]
        public void MarsMission_Player_Can_Give_Rover_Instructions(int xCoordinate, int yCoordinate, char bearing, int expectedXCoordinate, int expectedYCoordinate, char expectedBearing)
        {
            _mission.CreateRover(xCoordinate, yCoordinate, bearing);

            var rover = _mission.Player.Team[0];

            AssertBeforeAction(xCoordinate, yCoordinate, bearing, rover);

            _mission.Player.GiveRoverInstructions("M");

            AssertAfterAction(expectedXCoordinate, expectedYCoordinate, expectedBearing, rover);
        }

        [TestCase(0, 0, 'N', 0, 1, 'W', "MLM")]
        [TestCase(5, 5, 'S', 4, 3, 'W', "MMLMMRRM")]
        [TestCase(1, 1, 'S', 1, 0, 'S', "MMMMM")]
        public void MarsMission_Player_Can_Give_Rover_Move_Out_Of_Bounds_Instructions(int xCoordinate, int yCoordinate, char bearing, int expectedXCoordinate, int expectedYCoordinate, char expectedBearing, string instructions)
        {
            _mission.CreateRover(xCoordinate, yCoordinate, bearing);

            var rover = _mission.Player.Team[0];

            AssertBeforeAction(xCoordinate, yCoordinate, bearing, rover);

            _mission.Player.GiveRoverInstructions(instructions);

            AssertAfterAction(expectedXCoordinate, expectedYCoordinate, expectedBearing, rover);
        }

        [TestCase(0, 0, 'N', 0, 1, 'N', 1, "MLM")]
        [TestCase(5, 5, 'S', 5, 3, 'E', 4, "MMLMMRRM")]
        [TestCase(2, 2, 'S', 2, 1, 'S', 1, "MMMMM")]
        public void MarsMission_Player_Can_Give_X_Number_Of_M_Instructions(int xCoordinate, int yCoordinate, char bearing, int expectedXCoordinate, int expectedYCoordinate, char expectedBearing, int instructionLimit, string instructions)
        {
            _mission = new MarsMission(_maxXCoordinate, _maxYCoordinate, _teamLimit, instructionLimit);

            _mission.CreateRover(xCoordinate, yCoordinate, bearing);

            var rover = _mission.Player.Team[0];

            AssertBeforeAction(xCoordinate, yCoordinate, bearing, rover);

            _mission.Player.GiveRoverInstructions(instructions);

            AssertAfterAction(expectedXCoordinate, expectedYCoordinate, expectedBearing, rover);
        }

        private static void AssertBeforeAction(int xCoordinate, int yCoordinate, char bearing, Rover rover)
        {
            Assert.Multiple(() =>
            {
                Assert.That(rover.Position.XCoordinate, Is.EqualTo(xCoordinate));
                Assert.That(rover.Position.YCoordinate, Is.EqualTo(yCoordinate));
                Assert.That(rover.Position.Bearing, Is.EqualTo(bearing));
            });
        }

        private static void AssertAfterAction(int expectedXCoordinate, int expectedYCoordinate, char expectedBearing, Rover rover)
        {
            Assert.Multiple(() =>
            {
                Assert.That(rover.Position.XCoordinate, Is.EqualTo(expectedXCoordinate));
                Assert.That(rover.Position.YCoordinate, Is.EqualTo(expectedYCoordinate));
                Assert.That(rover.Position.Bearing, Is.EqualTo(expectedBearing));
            });
        }
    }
}