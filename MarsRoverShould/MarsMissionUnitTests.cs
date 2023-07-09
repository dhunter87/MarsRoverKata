using System;
using MarsRover.Models;
using NUnit.Framework;

namespace MarsMissionShould
{
	[TestFixture]
	public class MarsMissionUnitTests
	{
		[SetUp]
		public void Setup()
		{

		}

		[Test]
		public void MarsMission_Should_Not_Be_Null_When_Initialised()
		{
			var marsMission = new MarsMission();

			Assert.That(marsMission, Is.Not.Null);
		}

        [Test]
        public void MarsMission_Platau_Is_Not_Null_When_Initialised()
        {
            var marsMission = new MarsMission();

			marsMission.CreatePlatau(5, 5);

            Assert.Multiple(() =>
            {
				Assert.That(marsMission.Platau, Is.Not.Null);
                Assert.That(marsMission.Platau.MaxXCoordinate, Is.EqualTo(5));
                Assert.That(marsMission.Platau.MaxYCoordinate, Is.EqualTo(5));
            });
        }

        [Test]
        public void MarsMission_Player_Is_Not_Null_When_Initialised()
        {
            var marsMission = new MarsMission();

            Assert.That(marsMission.Player, Is.Not.Null);
        }
    }
}