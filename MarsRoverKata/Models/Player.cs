using System.Numerics;
using MarsRover.Interfaces;

namespace MarsRover.Models
{
    public class Player
    {
        public Dictionary<IGamePoint, string> GamePoints;
        public List<IRover> Team;
        public IRover? PreviousRover;
        public IPlateau Plateau;

        public string Id;
        private int Score;
        public readonly int TeamLimit;
        private readonly int InstructionLimit;

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

        public void GiveRoverInstructions(string instructions)
        {
            var rover = GetNextRover();

            if (string.IsNullOrEmpty(instructions))
            {
                return;
            }

            if (instructions.Length > InstructionLimit)
            {
                instructions = instructions.Substring(0, InstructionLimit);
            }

            var points = rover.ExecuteInstructions(instructions);

            GetScoreThisMove(rover, points);
            PreviousRover = rover;
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

            return Score;
        }

        private void GetScoreThisMove(IRover rover, List<IGamePoint> points)
        {
            foreach (var point in points)
            {
                GamePoints.Add(point, rover.GetId());
            }

            GetScore();
            Console.WriteLine($"Player: {Id}, Score: {Score}");
        }

        public IRover GetNextRover()
        {
            var previousRoverIndex = Team.IndexOf(PreviousRover);

            return previousRoverIndex + 1 < Team.Count ?
                    Team[previousRoverIndex + 1] :
                    Team[0];
        }

        public Dictionary<IGamePoint, string>? GetGamePoints()
        {
            if (GamePoints.Count <= 0)
            {
                return null;
            }
            return GamePoints;
        }
    }
}

