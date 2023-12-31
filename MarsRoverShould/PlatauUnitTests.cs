﻿using System;
using System.Diagnostics.Metrics;
using System.Reflection;
using MarsRover.Interfaces;
using MarsRover.Models;
using MarsRoverUnitTests.TestHelpers;
using NUnit.Framework;

namespace PlatauShould
{
	[TestFixture]
	public class PlatauUnitTests
	{
        private Plateau _plateau;
        private List<IRoverPosition> _roverPositions;

		[SetUp]
		public void Setup()
		{
            var coordinate = Coordinate.CreateCoordinate(10, 10);
            _plateau = new Plateau(coordinate, Constants.gamePointsCount);
            _roverPositions = new List<IRoverPosition>
            {
                RoverPosition.CreateRoverPosition(0,0,'N'),
                RoverPosition.CreateRoverPosition(1,0,'N'),
                RoverPosition.CreateRoverPosition(0,1,'N'),
            };
        }

        [TestCase(5,5)]
		[TestCase(10, 5000)]
		[TestCase(0, 0)]
		public void Platau_Should_Not_Be_Null_When_Initialised_With_Coordinates(int maxXCoordinate, int maxYCoordinate)
        {
            var coordinate = Coordinate.CreateCoordinate(maxXCoordinate, maxYCoordinate);

            _plateau = new Plateau(coordinate, Constants.gamePointsCount);

            Assert.Multiple(() =>
            {
                Assert.That(_plateau.MaxCoordinates.XCoordinate, Is.EqualTo(maxXCoordinate));
                Assert.That(_plateau.MaxCoordinates.YCoordinate, Is.EqualTo(maxYCoordinate));
            });
        }

        [TestCase(-5, 5)]
        [TestCase(-5, -5)]
        [TestCase(0, -1)]
        [TestCase(10, -1)]
        public void Platau_Should_Not_Be_Initialised_When_Initialised_With_A_Negative_Coordinate(int maxXCoordinate, int maxYCoordinate)
        {
            var coordinate = Coordinate.CreateCoordinate(maxXCoordinate, maxYCoordinate);

            Assert.Throws<ArgumentException>(() => new Plateau(coordinate, Constants.gamePointsCount));
        }

        [TestCase(5, 5, 5, 5, 'N', true)]
        [TestCase(5, 5, 0, 5, 'N', true)]
        [TestCase(5, 5, 5, 0, 'N', true)]
        [TestCase(5, 5, 0, 0, 'N', true)]
        [TestCase(5, 5, 5, 6, 'N', false)]
        [TestCase(5, 5, 6, 5, 'N', false)]
        [TestCase(0, 0, -1, 0, 'N', false)]
        [TestCase(0, 0, 0, -1, 'N', false)]
        public void IsValildMove_Should_Return_False_If_Coordinates_Are_Out_Of_Bounds_Of_The_Platau(int maxXCoordinate, int maxYCoordinate, int testXCoordinate, int testYCoordinate, char bearing, bool expectedResult)
        {
            var coordinate = Coordinate.CreateCoordinate(maxXCoordinate, maxYCoordinate);
            var testCoordinate = Coordinate.CreateCoordinate(testXCoordinate, testYCoordinate);

            _plateau = new Plateau(coordinate, Constants.gamePointsCount);
            _plateau.SetupGamePoints();

            var roverPosition = RoverPosition.CreateRoverPosition(maxXCoordinate, maxYCoordinate, bearing);

            _plateau.AddRover(roverPosition, Constants.RoverId);

            var result = _plateau.IsValildMove(testCoordinate, Constants.RoverId);

            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [Test]
        public void Platau_Should_Indicate_If_Rover_Reaches_GoalPoint()
        {
            var coordinate = Coordinate.CreateCoordinate(1,1);

            _plateau = new Plateau(coordinate, 1);

            var counter = 1;
            foreach (var position in _roverPositions)
            {
                var roverPosition = RoverPosition.CreateRoverPosition(position.XCoordinate, position.YCoordinate, position.Bearing);

                _plateau.AddRover(roverPosition, Constants.RoverId + counter);
                counter++;
            }

            _plateau.SetupGamePoints();

            var isGamePoint = _plateau.IsGamePointMove(coordinate);

            Assert.That(isGamePoint, Is.EqualTo(true));
        }

        [Test]
        public void Platau_Should_Return_GoalPoint_Treasure_Value()
        {
            var coordinate = Coordinate.CreateCoordinate(1, 1);

            _plateau = new Plateau(coordinate, 1);

            var counter = 1;
            foreach (var position in _roverPositions)
            {
                var roverPosition = RoverPosition.CreateRoverPosition(position.XCoordinate, position.YCoordinate, position.Bearing);

                _plateau.AddRover(roverPosition, Constants.RoverId + counter);
                counter++;
            }

            _plateau.SetupGamePoints();

            var isGamePoint = _plateau.IsGamePointMove(coordinate);
            var gamePoint = _plateau.GetGamePoint(coordinate);

            Assert.That(gamePoint, Is.Not.Null);

            AssertTreasureValueIsValid(gamePoint.TreasureValue);
            AssertTreasureTypeIsValid(gamePoint.TreasureType);
        }

        [Test]
        public void Platau_Should_Remove_Gamepoints_If_Reached_By_Rover()
        {
            var gamePointCount = 1;
            var coordinate = Coordinate.CreateCoordinate(0,0);

            _plateau = new Plateau(coordinate, gamePointCount);
            _plateau.SetupGamePoints();

            var isGamePoint = _plateau.IsGamePointMove(coordinate);
            var gamePoint = _plateau.GetGamePoint(coordinate);

            var result = _plateau.HasGamePoints();

            Assert.That(result, Is.EqualTo(false));
        }

        [Test]
        public void Platau_Should_Have_At_Least_One_Gamepoint_When_SetUpGamePoints_Is_Executed()
        {
            _plateau.SetupGamePoints();
            var hasGamePoint = _plateau.HasGamePoints();

            Assert.That(hasGamePoint, Is.EqualTo(true));
        }

        [Test]
        public void Platau_Should_Throw_Exception_If_It_Cannot_Create_Gamepoint()
        {
            var coordinate = Coordinate.CreateCoordinate(1, 1);

            _plateau = new Plateau(coordinate, 1);

            var counter = 1;
            foreach (var position in _roverPositions)
            {
                var roverPosition = RoverPosition.CreateRoverPosition(position.XCoordinate, position.YCoordinate, position.Bearing);

                _plateau.AddRover(roverPosition, Constants.RoverId + counter);
                counter++;
            }

            var newRoverPosition = RoverPosition.CreateRoverPosition(1, 1, 'N');
            _plateau.AddRover(newRoverPosition, Constants.RoverId);
            
            Assert.Throws<InvalidOperationException>(() => _plateau.SetupGamePoints());
        }

        // temp test (No accessor method needed beyond initial test)
        [Test]
        public void Platau_GamePoints_Should_Not_Clash_With_Rover_Starting_Positions()
        {
            var coordinate = Coordinate.CreateCoordinate(1, 1);

            _plateau = new Plateau(coordinate, 1);

            var counter = 1;
            foreach (var position in _roverPositions)
            {
                var roverPosition = RoverPosition.CreateRoverPosition(position.XCoordinate, position.YCoordinate, position.Bearing);

                _plateau.AddRover(roverPosition, Constants.RoverId + counter);
                counter++;
            }

            _plateau.SetupGamePoints();

            var gamepoints = _plateau.GetGamePoints();

            Assert.That(gamepoints, Is.Not.Null); 
            foreach (var roverPosition in _roverPositions)
            {
                Assert.That(!gamepoints.Any(x => x.Equals(roverPosition)));
            }
        }

        [Test]
        public void Platau_GamePoints_Should_Have_GamePoint_Value()
        {
            var coordinate = Coordinate.CreateCoordinate(1, 1);

            _plateau = new Plateau(coordinate, 1);

            var counter = 1;
            foreach (var position in _roverPositions)
            {
                var roverPosition = RoverPosition.CreateRoverPosition(position.XCoordinate, position.YCoordinate, position.Bearing);

                _plateau.AddRover(roverPosition, Constants.RoverId + counter);
                counter++;
            }

            _plateau.SetupGamePoints();

            var gamepoints = _plateau.GetGamePoints();

            Assert.That(gamepoints, Is.Not.Null);
            foreach (var gamepoint in gamepoints)
            {
                Assert.That(gamepoint.TreasureValue, Is.GreaterThan(0).And.LessThanOrEqualTo(3));

                AssertTreasureTypeIsValid(gamepoint.TreasureType);
            }
        }

        [TestCase(3, 3, 3)]
        [TestCase(5, 5, 10)]
        [TestCase(5, 5, 20)]
        [TestCase(5, 5, 22)]
        public void Platau_GamePoints_Should_Not_Clash_With_Rover_Starting_Positions(int maxXCoord, int maxYCord, int GamepointsCount)
        {
            var coordinate = Coordinate.CreateCoordinate(maxXCoord, maxYCord);

            _plateau = new Plateau(coordinate, GamepointsCount);

            var counter = 1;
            foreach (var position in _roverPositions)
            {
                var roverPosition = RoverPosition.CreateRoverPosition(position.XCoordinate, position.YCoordinate, position.Bearing);

                _plateau.AddRover(roverPosition, Constants.RoverId + counter);
                counter++;
            }

            _plateau.SetupGamePoints();

            var gamepoints = _plateau.GetGamePoints();

            Assert.That(gamepoints, Is.Not.Null);
            foreach (var roverPosition in _roverPositions)
            {
                Assert.That(!gamepoints.Any(x => x.Equals(roverPosition)));
            }
        }

        [TestCase(10, 10, 4)]
        [TestCase(10, 10, 10)]
        public void Platau_GamePoints_Should_Not_Clash_With_Other_Gamepoints(int maxXCoord, int maxYCord, int GamepointsCount)
        {
            var coordinate = Coordinate.CreateCoordinate(maxXCoord, maxYCord);

            _plateau = new Plateau(coordinate, GamepointsCount);

            _plateau.SetupGamePoints();

            var gamepoints = _plateau.GetGamePoints();

            Assert.That(gamepoints.Count(), Is.EqualTo(GamepointsCount));
        }

        // gamepoints should not exceed 20% available Plateau Coords i.e. MaxGamepoints = (maxX * maxY) * .20
        [TestCase(5, 5, 6, 5)]
        [TestCase(10, 10, 50, 20)]
        public void Platau_GamePoints_Should_Not_20_Percent_Of_Plateau(int maxXCoord, int maxYCord, int GamepointsCount, int expectedGamepointCount)
        {
            var coordinate = Coordinate.CreateCoordinate(maxXCoord, maxYCord);

            _plateau = new Plateau(coordinate, GamepointsCount);

            _plateau.SetupGamePoints();

            var gamepoints = _plateau.GetGamePoints();

            Assert.That(gamepoints.Count(), Is.EqualTo(expectedGamepointCount));
        }

        [Test]
        public void Plateau_Should_Keep_Track_Of_All_Rover_Positions()
        {
            List<IRoverPosition> roverPositions = _plateau.GetRoverPositions();
            Assert.That(roverPositions, Is.Not.Null);
        }

        [Test]
        public void Plateau_Should_Not_Allow_More_Than_One_Rover_Per_Coordinate()
        {
            var counter = 1;
            foreach (var position in _roverPositions)
            {

                var roverPosition = RoverPosition.CreateRoverPosition(0, 0, position.Bearing);

                _plateau.AddRover(roverPosition, Constants.RoverId + counter);
                counter++;
            }

            List<IRoverPosition> roverPositions = _plateau.GetRoverPositions();
            Assert.That(roverPositions.Count, Is.EqualTo(1));
        }

        [Test]
        public void Plateau_Should_Keep_Track_Of_All_Player_Movements()
        {
            var counter = 0;
            foreach (var position in _roverPositions)
            {

                var roverPosition = RoverPosition.CreateRoverPosition(counter, 0, position.Bearing);

                _plateau.AddRover(roverPosition, Constants.RoverId + counter);
                counter++;
            }
        }

        private static void AssertTreasureTypeIsValid(Prize treasureType)
        {
            switch (treasureType)
            {
                case Prize.Bronze:
                case Prize.Silver:
                case Prize.Gold:
                    break;
                default:
                    Assert.Fail($"Unexpected prize value: {treasureType}");
                    break;
            }
        }

        private void AssertTreasureValueIsValid(int value)
        {
            switch (value)
            {
                case 1:
                case 2:
                case 3:
                    break;
                default:
                    Assert.Fail($"Invalid Treasure Value: {value}");
                    break;
            }
        }
    }
}

    