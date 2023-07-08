using System;
using MarsRoverKata.Helpers;

namespace MarsRoverKata.Models
{
    public class MarsRover
    {
        public Position Position;

        public MarsRover(int xCoordinate, int yCoodinate, char bearing)
        {
            bearing = Char.ToUpper(bearing);

            if (!CoordinatesValidator.IsValid(xCoordinate, yCoodinate, bearing))
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
            (int deltaXCoordinate, int deltaYCoordinate) = DirectionMapper.GetDirectionDelta(Position.Bearing);
            Position.XCoordinate += deltaXCoordinate;
            Position.YCoordinate += deltaYCoordinate;
        }

        private void Rotate(int bearingIncrementor)
        {
            var currentRotationValue = DirectionMapper.GetCurrentBearing(Position.Bearing);
            var newRotationValue = (currentRotationValue + bearingIncrementor) % 4;

            Position.Bearing = DirectionMapper.GetNewBearing(newRotationValue);
        }
    }
}

