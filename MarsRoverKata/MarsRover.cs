using System;
namespace MarsRoverKata
{
	public class MarsRover
	{
        private readonly CoordinatesValidator validator;

        public int XCoordinate { get; private set; }
		public int YCoordinate { get; private set; }
        public char Bearing { get; private set; }

        public MarsRover(int xCoordinate, int yCoodinate, char bearing)
        {
            validator = new CoordinatesValidator();
            if (validator.IsValid(xCoordinate, yCoodinate, bearing))
            {
                XCoordinate = xCoordinate;
                YCoordinate = yCoodinate;
                Bearing = bearing;
            }
        }


        public void Move()
        {
            switch (Bearing)
            {
                case 'N':
                    YCoordinate++;
                    break;
                case 'E':
                    XCoordinate++;
                    break;
                case 'S':
                    YCoordinate--;
                    break;
                case 'W':
                    XCoordinate--;
                    break;
            }
        }

        public void TurnLeft()
        {
            switch (Bearing)
            {
                case 'N':
                    Bearing = 'W';
                    break;
                case 'W':
                    Bearing = 'S';
                    break;
                case 'S':
                    Bearing = 'E';
                    break;
                case 'E':
                    Bearing = 'N';
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
        }
    }
}

