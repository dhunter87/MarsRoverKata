using System;
using MarsRover.Models;
using MarsRoverUnitTests.TestHelpers;
using NUnit.Framework;

namespace PlayerShould
{
	[TestFixture]
	public class PlayerUnitTests
	{
		Player Player;
		Platau Platau;

		[SetUp]
		public void Setup()
		{
            Platau = new Platau(5,5);
			Player = new Player(Platau, Constants.TeamLimit, Constants.InstructionLimit);
		}

		[Test]
		public void Player_Should_Not_Be_Null_When_Initialised()
		{
			Assert.That(Player, Is.Not.Null);
		}

        [Test]
        public void Player_Score_Should_Not_Be_Null_When_Initialised()
        {
			var playerScore = Player.GetScore();
			Assert.That(playerScore, Is.Not.Null) ;
        }
    }
}

