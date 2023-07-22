using MarsRover.Interfaces;
//using static System.Formats.Asn1.AsnWriter;

namespace MarsRover.Models
{
    public class Player
    {
        public IPlateau Plateau;
        public List<IRover> Team;

        public string Id;
        private int Score;
        private readonly int TeamLimit;
        private readonly int InstructionLimit;
        public Dictionary<IGamePoint, string> GamePoints;

        public Player(IPlateau platau, int teamLimit, int instructionLimit, int id)
        {
            Id = $"P{id}";
            Score = new int();
            TeamLimit = teamLimit;
            InstructionLimit = instructionLimit;
            Plateau = platau;
            Team = new List<IRover>();
            GamePoints = new Dictionary<IGamePoint, string> ();
        }

        public bool AddTeamMember(IRoverPosition position, string id)
        {
            if (Team.Count < TeamLimit)
            {
                var rover = new Rover(position, Plateau, id);

                return TryAddRoverToPlateau(position, rover);
            }
            return false;
        }

        private bool TryAddRoverToPlateau(IRoverPosition position, IRover rover)
        {
            if (Plateau.AddRover(position, rover.GetId()))
            {
                Team.Add(rover);
                return true;
            }

            throw new ArgumentException($"Argument Exception. Unable to place rover in position, X: {position.XCoordinate}, Y: {position.YCoordinate}");
        }

        public int GetScore()
        {
            Score = 0;

            foreach (var gamepoint in GamePoints)
            {
                Score += gamepoint.Key.TreasureValue;
            }

            Console.WriteLine($"Player: {Id}, Score: {Score}");

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
            foreach (var point in points)
            {
                GamePoints.Add(point, rover.GetId());
            }

            GetScore();
        }

        public Dictionary<IGamePoint, string> GetGamePoints()
        {
            if (GamePoints.Count() > 0)
            {
                return GamePoints;
            }
            return null;
        }
    }
}

