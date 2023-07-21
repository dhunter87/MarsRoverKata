using MarsRover.Helpers;
using MarsRover.Interfaces;

namespace MarsRover.Models
{
    public class Rover : IRover
    {
        public IRoverPosition Position { get; set; }
        public IPlateau Plateau;
        private readonly string Id;
        public List<IGamePoint> GamePoints;

        public Rover(IRoverPosition position, IPlateau plateau, string id)
        {
            Id = id;
            Plateau = plateau;
            var bearing = Char.ToUpper(position.Bearing);
            GamePoints = new List<IGamePoint>();

            if (!CoordinatesValidator.IsRoverPositionAndBearingValid(position, Plateau.MaxCoordinates))
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

            var newPosition = Coordinate.CreateCoordinate(Position.XCoordinate + delta.XCoordinate, Position.YCoordinate + delta.YCoordinate);

            if (Plateau.IsValildMove(newPosition, Id))
            {
                Position.XCoordinate += delta.XCoordinate;
                Position.YCoordinate += delta.YCoordinate;
            }

            if (Plateau.IsGamePointMove(Position))
            {
                GamePoints.Add(Plateau.GetGamePoint(Position));
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
            return Id;
        }


    }
}

