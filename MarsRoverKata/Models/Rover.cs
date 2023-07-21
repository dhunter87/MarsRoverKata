using MarsRover.Helpers;
using MarsRover.Interfaces;

namespace MarsRover.Models
{
    public class Rover : IRover
    {
        public IRoverPosition Position { get; set; }
        public IPlateau Plateau;
        private readonly string RoverID;
        public List<IGamePoint> GamePoints;

        public Rover(IRoverPosition position, IPlateau plateau, string id)
        {
            RoverID = id;
            Plateau = plateau;
            var bearing = Char.ToUpper(position.Bearing);
            GamePoints = new List<IGamePoint>();

            if (!CoordinatesValidator.IsRoverPositionAndBearingValid(position.XCoordinate, position.YCoordinate, bearing, Plateau.MaxXCoordinate, Plateau.MaxYCoordinate))
            {
                throw new ArgumentException();
            }

            Position = RoverPosition.CreateRoverPosition(position.XCoordinate, position.YCoordinate, bearing);
        }

        public List<IGamePoint> ExecuteInstructions(string instructions)
        {
            int score = 0;
            List<char> invalidCommands = new List<char>();
            GamePoints = new List<IGamePoint>(); 

            foreach (var instruction in instructions)
            {
                if (!Enum.TryParse(Char.ToUpper(instruction).ToString(), out RoverCommand currentCommand))
                {
                    invalidCommands.Add(instruction);
                }

                ExecuteInstruction(currentCommand);
            }

            PrintInvalidCommands(invalidCommands);
            PrintRoverFinalPosition(score);

            return GamePoints;
        }

        private void PrintInvalidCommands(List<char> invalidCommands)
        {
            if (invalidCommands.Any())
            {
                Console.WriteLine($"Invalid commands: {string.Join(", ", invalidCommands.Distinct())}");
            }
        }

        private void PrintRoverFinalPosition(int score)
        {
            Console.WriteLine($"Rover Final Position. XCoordinate: {Position.XCoordinate}, YCoordinate: {Position.YCoordinate}, Bearing: {Position.Bearing}");
        }

        public void ExecuteInstruction(RoverCommand instruction)
        {
            if (instruction == RoverCommand.M)
            {
                Move();
            }

            if (instruction == RoverCommand.L || instruction == RoverCommand.R)
            {
                Rotate((int)instruction);
            }
        }

        private void Move()
        {
            var delta = DirectionMapper.GetDirectionDelta(Position.Bearing);

            if (Plateau.IsValildMove(Position.XCoordinate + delta.XCoordinate, Position.YCoordinate + delta.YCoordinate, RoverID))
            {
                Position.XCoordinate += delta.XCoordinate;
                Position.YCoordinate += delta.YCoordinate;
            }

            if (Plateau.IsGamePointMove(Position.XCoordinate, Position.YCoordinate))
            {
                GamePoints.Add(Plateau.GetGamePoint(Position.XCoordinate, Position.YCoordinate));
            }
        }

        private void Rotate(int bearingIncrementor)
        {
            var currentRotationValue = DirectionMapper.GetCurrentBearing(Position.Bearing);
            var newRotationValue = (currentRotationValue + bearingIncrementor) % 4;

            Position.Bearing = DirectionMapper.GetNewBearing(newRotationValue);
        }

        // CLI method only
        public string GetId()
        {
            return RoverID;
        }


    }
}

