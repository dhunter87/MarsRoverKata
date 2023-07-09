using System;
using MarsRover.Models;
using MarsRoverUnitTests.TestHelpers;
using NUnit.Framework;

namespace PlatauShould
{
	[TestFixture]
	public class PlatauUnitTests
	{
		[SetUp]
		public void Setup()
		{

		}

		[TestCase(5,5)]
		[TestCase(10, 5000)]
		[TestCase(0, 0)]
		public void Platau_Should_Not_Be_Null_When_Initialised_With_Coordinates(int maxXCoordinate, int maxYCoordinate)
		{
			var platau = new Platau(maxXCoordinate, maxYCoordinate);

			Assert.That(platau.MaxXCoordinate, Is.EqualTo(maxXCoordinate));
			Assert.That(platau.MaxYCoordinate, Is.EqualTo(maxYCoordinate));
        }

        [TestCase(-5, 5)]
        [TestCase(-5, -5)]
        [TestCase(0, -1)]
        [TestCase(10, -1)]
        public void Platau_Should_Not_Be_Initialised_When_Initialised_With_A_Negative_Coordinate(int maxXCoordinate, int maxYCoordinate)
        {
            Assert.Throws<ArgumentException>(() => new Platau(maxXCoordinate, maxYCoordinate));
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
            var platau = new Platau(maxXCoordinate, maxYCoordinate);
            var result = platau.IsValildMove(testXCoordinate, testYCoordinate);
            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [Test]
        [Ignore("until implamantaed")]
        public void Platau_Should_Have_At_Least_One_Goal_Point_When_Initiated()
        {
            var platau = new Platau(Constants.MaxXCoordinate, Constants.MaxYCoordinate);

            //var xCoordinate = platau.goalPoint.Item1;
            //var yCoordinate = platau.goalPoint.Item2;

            //var xCoordinateWithinPlatau = xCoordinate >= 0 && xCoordinate <= Constants.MaxXCoordinate;
            //var yCoordinateWithinPlatau = yCoordinate >= 0 && yCoordinate <= Constants.MaxYCoordinate;

            //Assert.That(xCoordinateWithinPlatau, Is.EqualTo(true));
            //Assert.That(yCoordinateWithinPlatau, Is.EqualTo(true));
        }
    }
}

