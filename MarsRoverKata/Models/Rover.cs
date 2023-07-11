using System;
using MarsRover.Helpers;
using MarsRover.Interfaces;

namespace MarsRover.Models
{
    public class Rover
    {
        public Position Position;
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

            Position = new Position(xCoordinate, yCoodinate, bearing);
        }

        public int ExecuteInstructions(string instructions, List<char>? invalidCommands = null, bool isRecursiveCall = false, int score = 0)
        {
            
            if (invalidCommands == null)
            {
                invalidCommands = new List<char>();
            }

            if (!string.IsNullOrEmpty(instructions))
            {
                if (Enum.TryParse(Char.ToUpper(instructions[0]).ToString(), out RoverCommand currentCommand))
                {
                    score += ExecuteInstruction(currentCommand);
                }
                else
                {
                    invalidCommands.Add(instructions[0]);
                }

                score = ExecuteInstructions(instructions.Substring(1), invalidCommands, true, score);
            }

            if (invalidCommands.Any())
            {
                Console.WriteLine($"Invalid commands: {string.Join(", ", invalidCommands.Distinct<char>())}");
            }
            if (!isRecursiveCall)
            {
                Console.WriteLine($"Rover Final Position. XCoordinate: {Position.XCoordinate}, YCoordinate: {Position.YCoordinate}, Bearing: {Position.Bearing}");
                Console.WriteLine($"Score This Move: {score}");
                return score;
            }
            return score;
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

        public int Move()
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

        public string GetId()
        {
            return RoverID;
        }
    }
}

