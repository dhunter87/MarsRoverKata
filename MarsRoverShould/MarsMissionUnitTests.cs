using System;
using MarsRover.Interfaces;
using MarsRover.Models;
using MarsRoverUnitTests.TestHelpers;
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
            _mission = new MarsMission(Constants.MaxXCoordinate, Constants.MaxYCoordinate, Constants.TeamLimit, Constants.InstructionLimit);
		}

		[Test]
		public void MarsMission_Should_Not_Be_Null_When_Initialised()
		{
			Assert.That(_mission, Is.Not.Null);
		}

        [TestCase(1, 1)]
        public void MarsMission_GetCommandList_Should_Return_Correct_Limit_Value(int actualInstructionLimit, int expectedValue)
        {
            _mission = new MarsMission(Constants.MaxXCoordinate, Constants.MaxYCoordinate, Constants.TeamLimit, actualInstructionLimit);

            var instructionLimit = _mission.GetCommandLimit();

            Assert.That(instructionLimit, Is.EqualTo(expectedValue));
        }

        [Test]
        public void MarsMission_Platau_Is_Not_Null_When_Initialised()
        {           
            _mission.CreatePlatau(5, 5);

            Assert.Multiple(() =>
            {
				Assert.That(_mission.Plateau, Is.Not.Null);
                Assert.That(_mission.Plateau.MaxXCoordinate, Is.EqualTo(5));
                Assert.That(_mission.Plateau.MaxYCoordinate, Is.EqualTo(5));
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
            _mission.CreateRover(3,2,'N', Constants.RoverId);

            Assert.That(_mission.Player.Team[0], Is.Not.Null);
        }

        [TestCase(5, 10, 5)]
        [TestCase(5, 5, 5)]
        [TestCase(5, 1, 1)]
        public void MarsMission_Player_Can_Not_Create_More_Than_X_Number_Of_New_Rovers(int testInstancesCount, int limit, int expectedCount)
        {
            var maxCoord = 5;
            _mission = new MarsMission(maxCoord, maxCoord, limit, Constants.InstructionLimit);

            for (int i = 0; i < testInstancesCount; i++)
            {
                _mission.CreateRover(3, 2, 'N', Constants.RoverId);
            }

            Assert.That(_mission.Player.Team.Count(), Is.EqualTo(expectedCount));
        }

        [TestCase(0, 6)]
        [TestCase(-1, 0)]
        [TestCase(6, 0)]
        [TestCase(0, -1)]
        public void MarsMission_Player_Can_Not_Create_New_Rovers_Outside_The_Bounds_Of_The_Platau(int xCoord, int yCoord)
        {
            var maxCoord = 5;
            _mission = new MarsMission(maxCoord, maxCoord, Constants.TeamLimit, Constants.InstructionLimit);

            Assert.Throws<ArgumentException>(() => _mission.CreateRover(xCoord, yCoord, 'N', Constants.RoverId));
        }

        [TestCase(1,0,'W')]
        [TestCase(5,5,'E')]
        [TestCase(1, 5, 'S')]
        public void MarsMission_Player_Can_Create_New_Rover(int xCoordinate, int yCoordinate, char bearing)
        {
            _mission.CreateRover(xCoordinate, yCoordinate, bearing, Constants.RoverId);

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
            _mission.CreateRover(xCoordinate, yCoordinate, bearing, Constants.RoverId);

            var rover = _mission.Player.Team[0];

            AssertBeforeAction(xCoordinate, yCoordinate, bearing, rover);

            _mission.Player.GiveRoverInstructions(rover, "M");

            AssertAfterAction(expectedXCoordinate, expectedYCoordinate, expectedBearing, rover);
        }

        [TestCase(0, 0, 'N', 0, 1, 'W', "MLM")]
        [TestCase(5, 5, 'S', 4, 3, 'W', "MMLMMRRM")]
        [TestCase(1, 1, 'S', 1, 0, 'S', "MMMMM")]
        public void MarsMission_Rover_Does_Not_Move_Out_Of_Bounds_If_Given_Out_Of_Bounds_Instructions(int xCoordinate, int yCoordinate, char bearing, int expectedXCoordinate, int expectedYCoordinate, char expectedBearing, string instructions)
        {
            _mission.CreateRover(xCoordinate, yCoordinate, bearing, Constants.RoverId);

            var rover = _mission.Player.Team[0];

            AssertBeforeAction(xCoordinate, yCoordinate, bearing, rover);

            _mission.Player.GiveRoverInstructions(rover, instructions);

            AssertAfterAction(expectedXCoordinate, expectedYCoordinate, expectedBearing, rover);
        }

        [TestCase(0, 0, 'N', 0, 1, 'N', 1, "MLM")]
        [TestCase(5, 5, 'S', 5, 3, 'E', 4, "MMLMMRRM")]
        [TestCase(2, 2, 'S', 2, 1, 'S', 1, "MMMMM")]
        public void MarsMission_Player_Can_Give_X_Number_Of_M_Instructions(int xCoordinate, int yCoordinate, char bearing, int expectedXCoordinate, int expectedYCoordinate, char expectedBearing, int instructionLimit, string instructions)
        {
            _mission = new MarsMission(Constants.MaxXCoordinate, Constants.MaxYCoordinate, Constants.TeamLimit, instructionLimit);

            _mission.CreateRover(xCoordinate, yCoordinate, bearing, Constants.RoverId);

            var rover = _mission.Player.Team[0];

            AssertBeforeAction(xCoordinate, yCoordinate, bearing, rover);

            _mission.Player.GiveRoverInstructions(rover, instructions);

            AssertAfterAction(expectedXCoordinate, expectedYCoordinate, expectedBearing, rover);
        }

        private static void AssertBeforeAction(int xCoordinate, int yCoordinate, char bearing, IRover rover)
        {
            Assert.Multiple(() =>
            {
                Assert.That(rover.Position.XCoordinate, Is.EqualTo(xCoordinate));
                Assert.That(rover.Position.YCoordinate, Is.EqualTo(yCoordinate));
                Assert.That(rover.Position.Bearing, Is.EqualTo(bearing));
            });
        }

        private static void AssertAfterAction(int expectedXCoordinate, int expectedYCoordinate, char expectedBearing, IRover rover)
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