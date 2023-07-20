using MarsRover.Helpers;
using MarsRover.Interfaces;

namespace MarsRover.Models
{
    public class Rover : IRover
    {
        public IRoverPosition Position { get; set; }
        public IPlateau Plateau;
        private readonly string RoverID;

        public Rover(int xCoordinate, int yCoodinate, char bearing, IPlateau plateau, string id)
        {
            RoverID = id;
            Plateau = plateau;
            bearing = Char.ToUpper(bearing);

            if (!CoordinatesValidator.IsValid(xCoordinate, yCoodinate, bearing, Plateau))
            {
                throw new ArgumentException();
            }

            Position = RoverPosition.CreateRoverPosition(xCoordinate, yCoodinate, bearing);
        }

        public int ExecuteInstructions(string instructions)
        {
            int score = 0;
            List<char> invalidCommands = new List<char>();

            foreach (var instruction in instructions)
            {
                var scoreThisMove = TryExecuteSingleInstruction(Char.ToUpper(instruction));

                if (scoreThisMove < 0)
                {
                    invalidCommands.Add(instructions[0]);
                }

                score += (int)scoreThisMove;
            }

            PrintInvalidCommands(invalidCommands);
            PrintRoverFinalPosition(score);

            return score;
        }

        private int TryExecuteSingleInstruction(char instruction)
        {
            if (Enum.TryParse(instruction.ToString(), out RoverCommand currentCommand))
            {
                return ExecuteInstruction(currentCommand);
            }

            return -1;
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
            Console.WriteLine($"Score This Move: {score}");
        }

        public int ExecuteInstruction(RoverCommand instruction)
        {
            if (instruction == RoverCommand.M)
            {
                return Move();
            }

            if (instruction == RoverCommand.L || instruction == RoverCommand.R)
            {
                Rotate((int)instruction);
            }
            return 0;
        }

        private int Move()
        {
            (int deltaXCoordinate, int deltaYCoordinate) = DirectionMapper.GetDirectionDelta(Position.Bearing);
            
            var nextXCoordinate = Position.XCoordinate + deltaXCoordinate;
            var nextYCoordinate = Position.YCoordinate + deltaYCoordinate;

            if (Plateau.IsValildMove(nextXCoordinate, nextYCoordinate))
            {
                Position.XCoordinate += deltaXCoordinate;
                Position.YCoordinate += deltaYCoordinate;
            }
            if (Plateau.IsGamePointMove(Position.XCoordinate, Position.YCoordinate))
            {
                return 1;
            }

            return 0;
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

