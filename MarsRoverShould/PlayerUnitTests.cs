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
        Rover Rover;

		[SetUp]
		public void Setup()
		{
            Platau = new Platau(5,5);
            Rover = new Rover(0,0,'N',Platau, Constants.RoverId);
			Player = new Player(Platau, Constants.TeamLimit, Constants.InstructionLimit);
		}

		[Test]
		public void Player_Should_Not_Be_Null_When_Initialised()
		{
			Assert.That(Player, Is.Not.Null);
		}

        [Test]
        public void Platau_Should_Not_Be_Null_When_Player_Is_Initialised()
        {
            Assert.That(Player.Platau, Is.Not.Null);
        }

        [Test]
        public void Player_Can_Add_New_Team_Members()
        {
			Player.AddTeamMember(0,0,'N', Constants.RoverId);
            
            Assert.That(Player.Team.Count, Is.EqualTo(1));
        }

        [TestCase(1,1)]
        [TestCase(5,3)]
        public void Players_Team_Can_Not_Exceed_Team_Limit(int limit, int expectedCount)
        {
            Player = new Player(Platau, limit, Constants.InstructionLimit);

            Player.AddTeamMember(0, 0, 'N', Constants.RoverId);
            Player.AddTeamMember(0, 0, 'N', Constants.RoverId);
            Player.AddTeamMember(0, 0, 'N', Constants.RoverId);

            Assert.That(Player.Team.Count, Is.EqualTo(expectedCount));
        }

        [Test]
        public void Player_Can_Give_Team_Rovers_Instructions()
        {
            Player.GiveRoverInstructions(Rover, "M");
            Assert.That(Rover.Position.YCoordinate, Is.EqualTo(1));
        }

        [TestCase(0,1,"M")]
        [TestCase(0,0,"LM")]
        [TestCase(0,2, "LMRMM")]
        public void Player_Can_Not_Move_Team_Rovers_Out_Of_Bounds(int expectedXCoord, int expectedYCoord, string instructions)
        {
            Player.GiveRoverInstructions(Rover, instructions);
            Assert.Multiple(() =>
            {
                Assert.That(Rover.Position.XCoordinate, Is.EqualTo(expectedXCoord));
                Assert.That(Rover.Position.YCoordinate, Is.EqualTo(expectedYCoord));
            });
        }

        [TestCase(0, 1, "M",1)]
        [TestCase(0, 1, "MM",1)]
        [TestCase(2, 1, "MRMMMMMM",4)]
        public void Players_Team_Rovers_Instructions_Are_Limited_To_InstructionLimit(int expectedXCoord, int expectedYCoord, string instructions, int limit)
        {
            Player = new Player(Platau, Constants.TeamLimit, limit);

            Player.GiveRoverInstructions(Rover, instructions);
            Assert.Multiple(() =>
            {
                Assert.That(Rover.Position.XCoordinate, Is.EqualTo(expectedXCoord));
                Assert.That(Rover.Position.YCoordinate, Is.EqualTo(expectedYCoord));
            });
        }

        [Test]
        public void Player_Score_Should_Be_Zero_When_Initialised()
        {
            var playerScore = Player.GetScore();
            Assert.That(playerScore, Is.EqualTo(0));
        }

        [Test]
        public void Player_Score_Should_Increase_By_One_When_Rover_Reaches_Goalpoint()
        {
            Platau = new Platau(0, 1);
            Player = new Player(Platau, Constants.TeamLimit, Constants.InstructionLimit);

            var playerScore = Player.GetScore();
            Assert.That(playerScore, Is.EqualTo(0));

            Player.GiveRoverInstructions(Rover, "M");

            Assert.That(playerScore, Is.EqualTo(1));
        }
    }
}

