using System;
using System.Reflection;
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
        ICoordinate _maxPlateauCoordinates;

        [SetUp]
		public void Setup()
		{
            _maxPlateauCoordinates = Coordinate.CreateCoordinate(
                    Constants.MaxXCoordinate,
                    Constants.MaxYCoordinate);

            var missionConfig = new MissionConfig(
                _maxPlateauCoordinates,
                Constants.gamePointsCount,
                Constants.TeamLimit,
                Constants.InstructionLimit,
                Constants.initialPlayerCount);

            _mission = new MarsMission(missionConfig);
        }

        [Test]
		public void MarsMission_Should_Not_Be_Null_When__Mission_Is_Initialised()
		{
			Assert.That(_mission, Is.Not.Null);
		}

        [Test]
        public void MarsMission_Platau_Is_Not_Null_When_Mission_Is_Initialised()
        {
            var maxPlateauCoordinates = Coordinate.CreateCoordinate(5,5);

            _mission.CreatePlatau(maxPlateauCoordinates);

            Assert.Multiple(() =>
            {
				Assert.That(_mission.Plateau, Is.Not.Null);
                Assert.That(_mission.Plateau.MaxCoordinates.XCoordinate, Is.EqualTo(5));
                Assert.That(_mission.Plateau.MaxCoordinates.YCoordinate, Is.EqualTo(5));
            });
        }

        [Test]
        public void MarsMission_Player_Is_Not_Null_When_Mission_Is_Initialised()
        {
            var players = _mission.GetPlayers();
            Assert.That(players, Is.Not.Null);
        }

        [TestCase(0,1)] // has at least 1 player
        [TestCase(1,1)]
        [TestCase(5,5)]
        public void MarsMission_Players_List_Should_Be_Configured_With_N_Number_Of_Players_When_ActivateMission_Is_Executed(int configuredCount, int actualCount)
        {
            var missionConfig = new MissionConfig(
                _maxPlateauCoordinates,
                Constants.gamePointsCount,
                Constants.TeamLimit,
                Constants.InstructionLimit,
                configuredCount);

            _mission = new MarsMission(missionConfig);
            _mission.ActivateMission();

            var players = _mission.GetPlayers();

            Assert.That(players.Count(), Is.EqualTo(actualCount));
        }
    }
}