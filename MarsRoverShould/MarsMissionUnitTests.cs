using System;
using MarsRover.Helpers;
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
            var missionConfig = new MissionConfig(
                (Constants.MaxXCoordinate,
                Constants.MaxYCoordinate),
                Constants.gamePointsCount,
                Constants.TeamLimit,
                Constants.InstructionLimit,
                Constants.initialPlayerCount);

            _mission = new MarsMission(missionConfig);
		}

		[Test]
		public void MarsMission_Should_Not_Be_Null_When_Initialised()
		{
			Assert.That(_mission, Is.Not.Null);
		}

        [TestCase(1, 1)]
        public void MarsMission_GetCommandList_Should_Return_Correct_Limit_Value(int actualInstructionLimit, int expectedValue)
        {
            var missionConfig = new MissionConfig(
                (Constants.MaxXCoordinate,
                Constants.MaxYCoordinate),
                Constants.gamePointsCount,
                Constants.TeamLimit,
                actualInstructionLimit,
                Constants.initialPlayerCount);

            _mission = new MarsMission(missionConfig);

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
            var players = _mission.GetConfiguredPlayers();
            Assert.That(players, Is.Not.Null);
        }

        [TestCase(0,1)] // has at least 1 player
        [TestCase(1,1)]
        [TestCase(5,5)]
        public void MarsMission_Players_List_Should_Be_Configured_With_N_Number_Of_Players(int configuredCount, int actualCount)
        {
            var missionConfig = new MissionConfig(
                (Constants.MaxXCoordinate,
                Constants.MaxYCoordinate),
                Constants.gamePointsCount,
                Constants.TeamLimit,
                Constants.InstructionLimit,
                configuredCount);

            _mission = new MarsMission(missionConfig);

            var players = _mission.GetConfiguredPlayers();

            Assert.That(players.Count(), Is.EqualTo(actualCount));
        }

        [Test]
        public void MarsMission_Player_Can_Create_New_Rover()
        {
            var players = _mission.GetConfiguredPlayers();

            var currentPlayer = players.FirstOrDefault();

            if (currentPlayer != null)
            {
                _mission.CreateRover(currentPlayer ,3,2,'N', Constants.RoverId);

                Assert.That(currentPlayer.Team[0], Is.Not.Null);
            }
        }

        [TestCase(5, 10, 5)]
        [TestCase(5, 5, 5)]
        [TestCase(5, 1, 1)]
        public void MarsMission_Player_Can_Not_Create_More_Than_X_Number_Of_New_Rovers(int testInstancesCount, int limit, int expectedCount)
        {
            var maxCoord = 5;

            var missionConfig = new MissionConfig(
                (maxCoord,
                maxCoord),
                Constants.gamePointsCount,
                limit,
                Constants.InstructionLimit,
                Constants.initialPlayerCount);

            _mission = new MarsMission(missionConfig);

            var players = _mission.GetConfiguredPlayers();
            var currentPlayer = players.FirstOrDefault();

            if (currentPlayer != null)
            {
                for (int i = 0; i < testInstancesCount; i++)
                {
                    _mission.CreateRover(currentPlayer,3, 2, 'N', Constants.RoverId);
                }

                Assert.That(currentPlayer.Team.Count(), Is.EqualTo(expectedCount));
            }
        }

        [TestCase(0, 6)]
        [TestCase(-1, 0)]
        [TestCase(6, 0)]
        [TestCase(0, -1)]
        public void MarsMission_Player_Can_Not_Create_New_Rovers_Outside_The_Bounds_Of_The_Platau(int xCoord, int yCoord)
        {
            var maxCoord = 5;

            var missionConfig = new MissionConfig(
                (maxCoord,
                maxCoord),
                Constants.gamePointsCount,
                Constants.TeamLimit,
                Constants.InstructionLimit,
                Constants.initialPlayerCount);

            _mission = new MarsMission(missionConfig);

            var players = _mission.GetConfiguredPlayers();
            var currentPlayer = players.FirstOrDefault();

            if (currentPlayer != null)
            {
                Assert.Throws<ArgumentException>(() =>
                _mission.CreateRover(currentPlayer, xCoord, yCoord, 'N', Constants.RoverId));
            }
        }

        [TestCase(1,0,'W')]
        [TestCase(5,5,'E')]
        [TestCase(1, 5, 'S')]
        public void MarsMission_Player_Can_Create_New_Rover(int xCoordinate, int yCoordinate, char bearing)
        {
            var players = _mission.GetConfiguredPlayers();
            var currentPlayer = players.FirstOrDefault();

            if (currentPlayer != null)
            {
                _mission.CreateRover(
                    currentPlayer,
                    xCoordinate,
                    yCoordinate,
                    bearing,
                    Constants.RoverId);

                var rover = currentPlayer.Team[0];

                Assert.Multiple(() =>
                {
                    Assert.That(rover.Position.XCoordinate, Is.EqualTo(xCoordinate));
                    Assert.That(rover.Position.YCoordinate, Is.EqualTo(yCoordinate));
                    Assert.That(rover.Position.Bearing, Is.EqualTo(bearing));
                });
            }
        }


        [TestCase(0, 0, 'E', 1, 0, 'E')]
        [TestCase(5, 5, 'W', 4, 5, 'W')]
        [TestCase(1, 1, 'S', 1, 0, 'S')]
        public void MarsMission_Player_Can_Give_Rover_Instructions(int xCoordinate, int yCoordinate, char bearing, int expectedXCoordinate, int expectedYCoordinate, char expectedBearing)
        {
            var players = _mission.GetConfiguredPlayers();
            var currentPlayer = players.FirstOrDefault();

            if (currentPlayer != null)
            {
                _mission.CreateRover(
                    currentPlayer,
                    xCoordinate,
                    yCoordinate,
                    bearing,
                    Constants.RoverId);

                var rover = currentPlayer.Team[0];

                AssertBeforeAction(xCoordinate, yCoordinate, bearing, rover);

                currentPlayer.GiveRoverInstructions(rover, "M");

                AssertAfterAction(expectedXCoordinate, expectedYCoordinate, expectedBearing, rover);
            }
        }

        [TestCase(0, 0, 'N', 0, 1, 'W', "MLM")]
        [TestCase(5, 5, 'S', 4, 3, 'W', "MMLMMRRM")]
        [TestCase(1, 1, 'S', 1, 0, 'S', "MMMMM")]
        public void MarsMission_Rover_Does_Not_Move_Out_Of_Bounds_If_Given_Out_Of_Bounds_Instructions(int xCoordinate, int yCoordinate, char bearing, int expectedXCoordinate, int expectedYCoordinate, char expectedBearing, string instructions)
        {
            var players = _mission.GetConfiguredPlayers();
            var currentPlayer = players.FirstOrDefault();

            if (currentPlayer !=null)
            {
                _mission.CreateRover(
                    currentPlayer,
                    xCoordinate,
                    yCoordinate,
                    bearing,
                    Constants.RoverId);

                var rover = currentPlayer.Team[0];

                AssertBeforeAction(xCoordinate, yCoordinate, bearing, rover);

                currentPlayer.GiveRoverInstructions(rover, instructions);

                AssertAfterAction(expectedXCoordinate, expectedYCoordinate, expectedBearing, rover);
            }
        }

        [TestCase(0, 0, 'N', 0, 1, 'N', 1, "MLM")]
        [TestCase(5, 5, 'S', 5, 3, 'E', 4, "MMLMMRRM")]
        [TestCase(2, 2, 'S', 2, 1, 'S', 1, "MMMMM")]
        public void MarsMission_Player_Can_Give_X_Number_Of_M_Instructions(int xCoordinate, int yCoordinate, char bearing, int expectedXCoordinate, int expectedYCoordinate, char expectedBearing, int instructionLimit, string instructions)
        {
            var playerCount = 1;
            var missionConfig = new MissionConfig(
                (Constants.MaxXCoordinate,
                Constants.MaxYCoordinate),
                Constants.gamePointsCount,
                Constants.TeamLimit,
                instructionLimit,
                playerCount);

            _mission = new MarsMission(missionConfig);

            var players = _mission.GetConfiguredPlayers();
            var currentPlayer = players.FirstOrDefault();

            if (currentPlayer  != null)
            {
                _mission.CreateRover(
                    currentPlayer,
                    xCoordinate,
                    yCoordinate,
                    bearing,
                    Constants.RoverId);

                var rover = currentPlayer.Team[0];

                AssertBeforeAction(xCoordinate, yCoordinate, bearing, rover);

                currentPlayer.GiveRoverInstructions(rover, instructions);

                AssertAfterAction(expectedXCoordinate, expectedYCoordinate, expectedBearing, rover);
            }
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