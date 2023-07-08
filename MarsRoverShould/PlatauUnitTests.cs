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

		[TestCase()]
		public void Platau_Should_Not_Be_Null_When_Initialised_With_Coordinates()
		{
			var platau = new Platau(5, 5);

			Assert.That(platau.MaxXCoordinate, Is.EqualTo(5));
			Assert.That(platau.MaxYCoordinate, Is.EqualTo(5));

        }
	}
}

