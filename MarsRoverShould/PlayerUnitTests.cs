using System;
using MarsRover.Helpers;
using MarsRover.Interfaces;
using MarsRover.Models;
using MarsRoverUnitTests.Dummies;
using MarsRoverUnitTests.Fakes;
using MarsRoverUnitTests.TestHelpers;
using Moq;
using NUnit.Framework;

namespace PlayerShould
{
	[TestFixture]
	public class PlayerUnitTests
	{
		Player Player;
        PlateauFake FakePlateau;
        Mock<IRover> MockRover;

        [SetUp]
        public void Setup()
        {
            MockRover = new Mock<IRover>();
            FakePlateau = new PlateauFake(10,10);

            Player = new Player(FakePlateau, Constants.TeamLimit, Constants.InstructionLimit, Constants.PlayerOneId);

            MockRover.Setup(r => r.ExecuteInstructions(
                It.IsAny<string>()))
                .Returns(new List<IGamePoint>
                {
                    { new GamePointFake(1, Prize.Bronze) }
                });
        }

        [Test]
        public void Player_Should_Not_Be_Null_When_Initialised()
        {
            Assert.That(Player, Is.Not.Null);
        }

        [Test]
        public void Platau_Should_Not_Be_Null_When_Player_Is_Initialised()
        {
            Assert.That(Player.Plateau, Is.Not.Null);
        }

        [TestCase(3, 1, 1)]
        [TestCase(3, 5, 3)]
        [TestCase(3, 3, 3)]
        public void Players_Team_Can_Not_Exceed_Team_Limit(int actualInstanceCount, int limit, int expectedCount)
        {
            Player = new Player(FakePlateau, limit, Constants.InstructionLimit, Constants.PlayerOneId);
            for (int i = 0; i < actualInstanceCount; i++)
            {
                Player.AddTeamMember(i, 0, 'N', $"{Constants.RoverId}{i}");
            }

            Assert.That(Player.Team.Count, Is.EqualTo(expectedCount));
        }

        [Test]
        public void Player_Can_Give_Team_Rovers_Instructions()
        {
            Player.GiveRoverInstructions(MockRover.Object, "M");

            MockRover.Verify(x => x.ExecuteInstructions(It.IsAny<string>()), Times.Once);
        }

        [TestCase("MMMMM", 3, "MMM")]
        [TestCase("MMM", 3, "MMM")]
        [TestCase("M", 3, "M")]
        public void Players_Team_Rovers_Instructions_Are_Limited_To_InstructionLimit(string instructions, int limit, string actualInstructions)
        {
            Player = new Player(FakePlateau, teamLimit: 1, limit, Constants.PlayerOneId);
            Player.GiveRoverInstructions(MockRover.Object, instructions);

            MockRover.Verify(x => x.ExecuteInstructions(It.Is<string>(s => s == actualInstructions)),
                Times.Once);
        }

        [Test]
        public void Player_Score_Should_Be_Zero_When_Initialised()
        {
            var playerScore = Player.GetScore();
            Assert.That(playerScore, Is.EqualTo(0));
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public void Player_Score_Should_Increase_By_TreasureValue_When_Rover_Reaches_Goalpoint(int treasureVal)
        {
            var instructions = "M";

            MockRover.Setup(r => r.ExecuteInstructions(
                It.Is<string>(s => s == instructions)))
            .Returns(new List<IGamePoint>
            {
                { new GamePointFake(treasureVal, Prize.Bronze) }
            });

            var playerScore = Player.GetScore();
            Assert.That(playerScore, Is.EqualTo(0));

            playerScore = GiveInstructionsAndCheckScore(MockRover.Object, Player, instructions);

            Assert.That(playerScore, Is.EqualTo(treasureVal));
        }

        [TestCase("", 1)]
        [TestCase("M", 1)]
        [TestCase("M", 2)]
        [TestCase("MM", 1)]
        [TestCase("MM", 2)]
        public void Player_Score_Increments_By_Value_Returned_By_Rover_Multiple_Moves(string instructions, int treasureVal)
        {
            var instructionsCount = instructions.ToArray().Length;
            var valuePerMove = (instructionsCount * treasureVal);

            MockRover.Setup(r => r.ExecuteInstructions(
                    It.Is<string>(s => s == instructions)))
                .Returns(new List<IGamePoint>
                {
                    { new GamePointFake(valuePerMove, Prize.Bronze) }
                });

            var playerScore = Player.GetScore();
            Assert.That(playerScore, Is.EqualTo(0));

            playerScore = GiveInstructionsAndCheckScore(MockRover.Object, Player, instructions);
            Assert.That(playerScore, Is.EqualTo(valuePerMove));

            playerScore = GiveInstructionsAndCheckScore(MockRover.Object, Player, instructions);
            Assert.That(playerScore, Is.EqualTo(valuePerMove * 2));
        }

        private static int GiveInstructionsAndCheckScore(IRover rover, Player player, string instructions)
        {
            int playerScore;
            player.GiveRoverInstructions(rover, instructions);

            playerScore = player.GetScore();
            
            return playerScore;
        }
    }
}