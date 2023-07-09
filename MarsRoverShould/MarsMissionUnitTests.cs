using System;
using MarsRover.Models;
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
            _mission = new MarsMission();
		}

		[Test]
		public void MarsMission_Should_Not_Be_Null_When_Initialised()
		{
			Assert.That(_mission, Is.Not.Null);
		}

        [Test]
        public void MarsMission_Platau_Is_Not_Null_When_Initialised()
        {           
            _mission.CreatePlatau(5, 5);

            Assert.Multiple(() =>
            {
				Assert.That(_mission.Platau, Is.Not.Null);
                Assert.That(_mission.Platau.MaxXCoordinate, Is.EqualTo(5));
                Assert.That(_mission.Platau.MaxYCoordinate, Is.EqualTo(5));
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
            _mission.CreateRover(1,0,'N');

            Assert.That(_mission.Player, Is.Not.Null);
        }

        [Test]
        public void MarsMission_StartMission_()
        {
            Assert.That(_mission.Player, Is.Not.Null);
        }
    }
}