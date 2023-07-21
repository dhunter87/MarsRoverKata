using MarsRover.Interfaces;
using static System.Formats.Asn1.AsnWriter;

namespace MarsRover.Models
{
    public class Player
	{
        public IPlateau Plateau;
        public List<IRover> Team;
        
        public int PlayerId;
        private int Score;
        private readonly int TeamLimit;
        private readonly int InstructionLimit;
        List<IGamePoint> GamePoints;

        public Player(IPlateau platau, int teamLimit, int instructionLimit, int id)
		{
            PlayerId = id;
            Score = new int();
            TeamLimit = teamLimit;
            InstructionLimit = instructionLimit;
            Plateau = platau;
            Team = new List<IRover>();
            GamePoints = new List<IGamePoint>();
		}

        public void AddTeamMember(int xCoordinate, int yCoordinate, char bearing, string id)
        {
            if (Team.Count < TeamLimit)
            {
                var rover = new Rover(xCoordinate, yCoordinate, bearing, Plateau, id);
                Team.Add(rover);
                Plateau.AddRover(xCoordinate, yCoordinate, bearing, rover.GetId());
            }   
        }

        public int GetScore()
        {
            Score = 0;

            foreach (var gamepoint in GamePoints)
            {
                Score += gamepoint.TreasureValue;
            }

            Console.WriteLine($"Player: {PlayerId}, Score: {Score}");

            return Score;
        }

        public void GiveRoverInstructions(IRover rover, string instructions)
        {
            if (string.IsNullOrEmpty(instructions))
            {
                return;
            }

            if (instructions.Length > InstructionLimit)
            {
                instructions = instructions.Substring(0, InstructionLimit);
            }
            var points = rover.ExecuteInstructions(instructions);
            GamePoints.AddRange(points);
            GetScore();
        }
    }
}

