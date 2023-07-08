using System;
namespace MarsRoverKata
{
    public class MarsRover
    {
        private readonly CoordinatesValidator _validator;
        public Position Position;

        public Dictionary<char, (int, int)> DirectionOfTravelMapper = new Dictionary<char, (int, int)>
        {
            {'N', (0, 1) },
            {'E', (1, 0) },
            {'S', (0, -1) },
            {'W', (-1, 0) }
        };

        public Dictionary<char, int> BearingRotationMapper = new Dictionary<char, int>
        {
            { 'N', 0 },
            { 'E', 1 },
            { 'S', 2 },
            { 'W', 3 }
        };

        public (int , int) GetDirectionDelta(char currentBearing)
        {
            if (DirectionOfTravelMapper.TryGetValue(currentBearing, out var directionDelta))
            {
                return directionDelta;
            }
            throw new ArgumentException($"Invalid bearing: {currentBearing}");
        }

        public MarsRover(int xCoordinate, int yCoodinate, char bearing)
        {
            bearing = Char.ToUpper(bearing);
            _validator = new CoordinatesValidator();

            if (!_validator.IsValid(xCoordinate, yCoodinate, bearing))
            {
                throw new ArgumentException();
            }

            Position = new Position(xCoordinate, yCoodinate, bearing);
        }

        public void ExecuteInstructions(string instructions, List<char>? invalidCommands = null)
        {
            if (invalidCommands == null)
            {
                invalidCommands = new List<char>();
            }

            if (!string.IsNullOrEmpty(instructions))
            {
                if (Enum.TryParse(Char.ToUpper(instructions[0]).ToString(), out RoverCommand currentCommand))
                {
                    ExecuteInstruction(currentCommand);
                }
                else
                {
                    invalidCommands.Add(instructions[0]);
                }

                ExecuteInstructions(instructions.Substring(1), invalidCommands);
            }

            if (invalidCommands.Any())
            {
                Console.WriteLine($"Invalid commands: {string.Join(", ", invalidCommands.Distinct<char>())}");
            }
        }

        public void ExecuteInstruction(RoverCommand instruction)
        {
            if (instruction == RoverCommand.M)
            {
                Move();
            }

            if (instruction == RoverCommand.L || instruction == RoverCommand.R)
            {
                Rotate(instruction);
            }
        }

        public void Move()
        {
            (int deltaXCoordinate, int deltaYCoordinate) = GetDirectionDelta(Position.Bearing);
            Position.XCoordinate += deltaXCoordinate;
            Position.YCoordinate += deltaYCoordinate;
        }

        public void Rotate(RoverCommand instruction)
        {
            if (instruction == RoverCommand.L)
            {
                TurnLeft();
            }
            if(instruction == RoverCommand.R)
            {
                TurnRight();
            }
        }

        public void TurnLeft()
        {
            var currentRotationValue = BearingRotationMapper[Position.Bearing];
            var newRotationValue = (currentRotationValue + 3) % 4;

            Position.Bearing = BearingRotationMapper.Single(kvp => kvp.Value == newRotationValue).Key;
        }

        private void TurnRight()
        {
            var currentRotationValue = BearingRotationMapper[Position.Bearing];
            var newRotationValue = (currentRotationValue + 1) % 4;

            Position.Bearing = BearingRotationMapper.Single(kvp => kvp.Value == newRotationValue).Key;
        }
    }
}

