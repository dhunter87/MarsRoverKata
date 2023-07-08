using System;
using MarsRoverKata.Mappers;

namespace MarsRoverKata
{
    public class MarsRover
    {
        private readonly CoordinatesValidator _validator;
        public Position Position;
        private readonly Mapper _mapper;

        public MarsRover(int xCoordinate, int yCoodinate, char bearing)
        {
            _validator = new CoordinatesValidator();
            _mapper = new Mapper();

            bearing = Char.ToUpper(bearing);

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
                Rotate((int)instruction);
            }
        }

        public void Move()
        {
            (int deltaXCoordinate, int deltaYCoordinate) = _mapper.GetDirectionDelta(Position.Bearing);
            Position.XCoordinate += deltaXCoordinate;
            Position.YCoordinate += deltaYCoordinate;
        }

        private void Rotate(int bearingIncrementor)
        {
            var currentRotationValue = _mapper.GetCurrentBearing(Position);
            var newRotationValue = (currentRotationValue + bearingIncrementor) % 4;

            Position.Bearing = _mapper.GetNewBearing(newRotationValue);
        }
    }
}

