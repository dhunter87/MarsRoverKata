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
        private ICoordinate _coordinate;

        [SetUp]
        public void Setup()
        {
            MockRover = new Mock<IRover>();
            FakePlateau = new PlateauFake(10,10);

            Player = new Player(FakePlateau, Constants.TeamLimit, Constants.InstructionLimit, Constants.PlayerOneId);

            _coordinate = Coordinate.CreateCoordinate(1,1);

            MockRover.Setup(r => r.Position).Returns(RoverPosition.CreateRoverPosition(_coordinate,'N'));

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
                _coordinate = Coordinate.CreateCoordinate(i, 0);

                var roverPosition = RoverPosition.CreateRoverPosition(_coordinate, 'N');

                Player.AddTeamMember(roverPosition, Constants.RoverId + i);
            }

            Assert.That(Player.Team, Has.Count.EqualTo(expectedCount));
        }

        [Test]
        public void Player_Can_Give_Team_Rovers_Instructions()
        {
            Player.Team.Add(MockRover.Object);
            Player.GiveRoverInstructions("M");

            MockRover.Verify(x => x.ExecuteInstructions(It.IsAny<string>()), Times.Once);
        }

        [TestCase("MMMMM", 3, "MMM")]
        [TestCase("MMM", 3, "MMM")]
        [TestCase("M", 3, "M")]
        public void Players_Team_Rovers_Instructions_Are_Limited_To_InstructionLimit(string instructions, int limit, string actualInstructions)
        {
            Player = new Player(FakePlateau, teamLimit: 1, limit, Constants.PlayerOneId);
            Player.Team.Add(MockRover.Object);
            Player.GiveRoverInstructions(instructions);

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
            _coordinate = Coordinate.CreateCoordinate(0,0);

            SetupMockRover(MockRover, RoverPosition.CreateRoverPosition(_coordinate, 'N'), instructions, treasureVal);

            Player.Team.Add(MockRover.Object);

            var playerScore = Player.GetScore();
            Assert.That(playerScore, Is.EqualTo(0));

            playerScore = GiveInstructionsAndCheckScore(Player, instructions);

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
            _coordinate = Coordinate.CreateCoordinate(0, 0);

            var startingPosition = RoverPosition.CreateRoverPosition(_coordinate, 'N');

            SetupMockRover(MockRover, startingPosition, instructions, valuePerMove);

            Player.Team.Add(MockRover.Object);

            var playerScore = Player.GetScore();
            Assert.That(playerScore, Is.EqualTo(0));

            playerScore = GiveInstructionsAndCheckScore(Player, instructions);
            Assert.That(playerScore, Is.EqualTo(valuePerMove));

            playerScore = GiveInstructionsAndCheckScore(Player, instructions);
            Assert.That(playerScore, Is.EqualTo(valuePerMove * 2));
        }

        [TestCase(1, 1, 0)]
        [TestCase(2, 1, 1)]
        [TestCase(3, 2, 0)]
        public void Given_Only_Two_Rovers_Per_Team_Player_Should_Move_The_Correct_Rover_For_That_Player_Turn(int playerMovesCount, int expectedYCoord, int roverIndex)
        {
            var instructions = "M";
            var mockRover2 = new Mock<IRover>();
            _coordinate = Coordinate.CreateCoordinate(0, 0);

            SetupMockRover(MockRover, RoverPosition.CreateRoverPosition(_coordinate, 'N'), instructions);
            _coordinate.XCoordinate++;
            SetupMockRover(mockRover2, RoverPosition.CreateRoverPosition(_coordinate, 'N'), instructions);

            Player.Team.Add(MockRover.Object);
            Player.Team.Add(mockRover2.Object);

            for (int i = 0; i < playerMovesCount; i++)
            {
                Player.GiveRoverInstructions(instructions);
            }

            Assert.Multiple(() =>
            {
                Assert.That(Player.Team[roverIndex].Position.YCoordinate, Is.EqualTo(expectedYCoord));
            });
        }

        private static void SetupMockRover(Mock<IRover> ThisMockRover, IRoverPosition startingPosition, string instructions, int gamepointValue = 1)
        {
            ThisMockRover.SetupProperty(r => r.Position);
            ThisMockRover.Object.Position = startingPosition;
            ThisMockRover.Setup(r => r.ExecuteInstructions(
                    It.Is<string>(s => s == instructions)))
                .Callback((string _) =>
                {
                    ThisMockRover.Object.Position.YCoordinate += 1;
                })
                .Returns(() =>
                {
                    return new List<IGamePoint>
                    {
                        { new GamePointFake(gamepointValue, Prize.Bronze) }
                    };
                });
        }

        private static int GiveInstructionsAndCheckScore(Player player, string instructions)
        {
            int playerScore;
            player.GiveRoverInstructions(instructions);

            playerScore = player.GetScore();
            
            return playerScore;
        }
    }
}