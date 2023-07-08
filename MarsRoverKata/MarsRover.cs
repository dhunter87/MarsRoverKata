using System;
namespace MarsRoverKata
{
    public class MarsRover
    {
        private readonly CoordinatesValidator _validator;
        public Position Position;

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

        public void Move()
        {
            switch (Position.Bearing)
            {
                case 'N':
                    Position.YCoordinate++;
                    break;
                case 'E':
                    Position.XCoordinate++;
                    break;
                case 'S':
                    Position.YCoordinate--;
                    break;
                case 'W':
                    Position.XCoordinate--;
                    break;
            }
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
            switch (Position.Bearing)
            {
                case 'N':
                    Position.Bearing = 'W';
                    break;
                case 'W':
                    Position.Bearing = 'S';
                    break;
                case 'S':
                    Position.Bearing = 'E';
                    break;
                case 'E':
                    Position.Bearing = 'N';
                    break;
            }
        }

        private void TurnRight()
        {
            switch (Position.Bearing)
            {
                case 'N':
                    Position.Bearing = 'E';
                    break;
                case 'E':
                    Position.Bearing = 'S';
                    break;
                case 'S':
                    Position.Bearing = 'W';
                    break;
                case 'W':
                    Position.Bearing = 'N';
                    break;
            }
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

    }
}

