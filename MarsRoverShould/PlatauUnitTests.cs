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
        Plateau Plateau;

		[SetUp]
		public void Setup()
		{
            Plateau = new Plateau(10, 10);

        }

        [TestCase(5,5)]
		[TestCase(10, 5000)]
		[TestCase(0, 0)]
		public void Platau_Should_Not_Be_Null_When_Initialised_With_Coordinates(int maxXCoordinate, int maxYCoordinate)
        {
            Plateau = new Plateau(maxXCoordinate, maxYCoordinate);

            Assert.Multiple(() =>
            {
                Assert.That(Plateau.MaxXCoordinate, Is.EqualTo(maxXCoordinate));
                Assert.That(Plateau.MaxYCoordinate, Is.EqualTo(maxYCoordinate));
            });
        }

        [TestCase(-5, 5)]
        [TestCase(-5, -5)]
        [TestCase(0, -1)]
        [TestCase(10, -1)]
        public void Platau_Should_Not_Be_Initialised_When_Initialised_With_A_Negative_Coordinate(int maxXCoordinate, int maxYCoordinate)
        {
            Assert.Throws<ArgumentException>(() => new Plateau(maxXCoordinate, maxYCoordinate));
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
            Plateau = new Plateau(maxXCoordinate, maxYCoordinate);

            var result = Plateau.IsValildMove(testXCoordinate, testYCoordinate);

            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [Test]
        public void Platau_Should_Have_At_Least_One_Goal_Point_When_Initiated()
        {
            var plateau = new Plateau(Constants.MaxXCoordinate, Constants.MaxYCoordinate);

            Assert.That(plateau.HasGamePoints(), Is.EqualTo(true));
        }

        [Test]
        public void Platau_Should_Indicate_If_Rover_Reaches_GoalPoint()
        {
            Plateau = new Plateau(0, 0);

            var isGamePoint = Plateau.IsGamePointMove(0,0);

            Assert.That(isGamePoint, Is.EqualTo(true));
        }

        [Test]
        public void Platau_Should_Remove_Gamepoints_If_Reached_By_Rover()
        {
            Plateau = new Plateau(0, 0);

            var isGamePoint = Plateau.IsGamePointMove(0, 0);

            var result = Plateau.HasGamePoints();

            Assert.That(result, Is.EqualTo(false));
        }

        [Test]
        public void Platau_Should_Have_At_Least_One_Gamepoint()
        {
            var hasGamePoint = Plateau.HasGamePoints();

            Assert.That(hasGamePoint, Is.EqualTo(true));
        }
    }
}

    