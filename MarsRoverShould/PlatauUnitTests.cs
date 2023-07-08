using System;
using MarsRoverKata.Models;
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
		[TestCase(10,5000)]
		[TestCase(0,0)]
        public void Platau_Should_Not_Be_Null_When_Initialised_With_Coordinates(int maxXCoordinate, int maxYCoordinate)
		{
			var platau = new Platau(maxXCoordinate, maxYCoordinate);

			Assert.That(platau.MaxXCoordinate, Is.EqualTo(maxXCoordinate));
			Assert.That(platau.MaxYCoordinate, Is.EqualTo(maxYCoordinate));

        }

        [TestCase(-5, 5)]
        public void Platau_Should_Not_Be_Initialised_When_Initialised_With_A_Negative_Coordinate(int maxXCoordinate, int maxYCoordinate)
        {
			Assert.Throws<Exception>(() => new Platau(maxXCoordinate, maxYCoordinate));
        }
    }
}

