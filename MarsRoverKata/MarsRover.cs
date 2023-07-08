using System;
namespace MarsRoverKata
{
	public class MarsRover
	{
        private readonly CoordinatesValidator _validator;
        public Position Position;

        public MarsRover(int xCoordinate, int yCoodinate, char bearing)
        {
            _validator = new CoordinatesValidator();

            if (_validator.IsValid(xCoordinate, yCoodinate, bearing))
            {
                Position = new Position(xCoordinate, yCoodinate, bearing);
            }
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

        public void ExecuteInstruction(char instruction)
        {
            if (instruction == 'M')
            {
                Move();
            }
            if (instruction == 'L')
            {
                TurnLeft();
            }
            if (instruction == 'R')
            {
                TurnRight();
            }
        }

    }
}

