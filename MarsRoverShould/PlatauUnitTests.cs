using System;
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
            _plateau = new Plateau(10, 10, Constants.gamePointsCount);
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
            _plateau = new Plateau(maxXCoordinate, maxYCoordinate, Constants.gamePointsCount);

            Assert.Multiple(() =>
            {
                Assert.That(_plateau.MaxXCoordinate, Is.EqualTo(maxXCoordinate));
                Assert.That(_plateau.MaxYCoordinate, Is.EqualTo(maxYCoordinate));
            });
        }

        [TestCase(-5, 5)]
        [TestCase(-5, -5)]
        [TestCase(0, -1)]
        [TestCase(10, -1)]
        public void Platau_Should_Not_Be_Initialised_When_Initialised_With_A_Negative_Coordinate(int maxXCoordinate, int maxYCoordinate)
        {
            Assert.Throws<ArgumentException>(() => new Plateau(maxXCoordinate, maxYCoordinate, Constants.gamePointsCount));
        }

        [TestCase(5, 5, 5, 5, true)]
        [TestCase(5, 5, 0, 5, true)]
        [TestCase(5, 5, 5, 0, true)]
        [TestCase(5, 5, 0, 0, true)]
        [TestCase(5, 5, 5, 6, false)]
        [TestCase(5, 5, 6, 5, false)]
        [TestCase(0, 0, -1, 0, false)]
        [TestCase(0, 0, 0, -1, false)]
        public void IsValildMove_Should_Return_False_If_Coordinates_Are_Out_Of_Bounds_Of_The_Platau(int maxXCoordinate, int maxYCoordinate, int testXCoordinate, int testYCoordinate, bool expectedResult)
        {
            _plateau = new Plateau(maxXCoordinate, maxYCoordinate, Constants.gamePointsCount);
            _plateau.SetupGamePoints();

            var result = _plateau.IsValildMove(testXCoordinate, testYCoordinate);

            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [Test]
        public void Platau_Should_Indicate_If_Rover_Reaches_GoalPoint()
        {
            _plateau = new Plateau(0, 0, Constants.gamePointsCount);
            _plateau.SetupGamePoints();

            var isGamePoint = _plateau.IsGamePointMove(0,0);

            Assert.That(isGamePoint, Is.EqualTo(true));
        }

        [Test]
        public void Platau_Should_Remove_Gamepoints_If_Reached_By_Rover()
        {
            var gamePointCount = 1;
            _plateau = new Plateau(0, 0, gamePointCount);
            _plateau.SetupGamePoints();

            var isGamePoint = _plateau.IsGamePointMove(0, 0);

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

        // temp test (No accessor method needed beyond initial test)
        [Test]
        public void Platau_GamePoints_Should_Not_Clash_With_Rover_Starting_Positions()
        {
            _plateau = new Plateau(1, 1, 1);
            foreach (var position in _roverPositions)
            {
                _plateau.AddRover(position.XCoordinate, position.YCoordinate, position.Bearing);
            }

            _plateau.SetupGamePoints();

            var gamepoints = _plateau.GetGamePoints();

            Assert.That(gamepoints, Is.Not.Null); 
            foreach (var roverPosition in _roverPositions)
            {
                Assert.That(!gamepoints.Any(x => x.Equals(roverPosition)));
            }
        }

        [TestCase(3,3,3)]
        [TestCase(5,5,10)]
        [TestCase(5,5,20)]
        [TestCase(5,5,22)]
        public void Platau_GamePoints_Should_Not_Clash_With_Rover_Starting_Positions(int maxXCoord, int maxYCord, int GamepointsCount)
        {
            _plateau = new Plateau(maxXCoord, maxYCord, GamepointsCount);

            foreach (var position in _roverPositions)
            {
                _plateau.AddRover(position.XCoordinate, position.YCoordinate, position.Bearing);
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
            _plateau = new Plateau(maxXCoord, maxYCord, GamepointsCount);

            _plateau.SetupGamePoints();

            var gamepoints = _plateau.GetGamePoints();

            Assert.That(gamepoints.Count(), Is.EqualTo(GamepointsCount));
        }

        // gamepoints snould not exceed 20% available Plateau Coords i.e. MaxGamepoints = (maxX * maxY) * .20
        [TestCase(5, 5, 3, 2)]
        public void Platau_GamePoints_Should_Not_20_Percent_Of_Plateau(int maxXCoord, int maxYCord, int GamepointsCount, int expectedGamepointCount)
        {
            _plateau = new Plateau(maxXCoord, maxYCord, GamepointsCount);

            _plateau.SetupGamePoints();

            var gamepoints = _plateau.GetGamePoints();

            Assert.That(gamepoints.Count(), Is.EqualTo(expectedGamepointCount));
        }
    }
}

    