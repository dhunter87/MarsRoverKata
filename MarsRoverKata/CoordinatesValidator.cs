using System;
namespace MarsRoverKata
{
	public class CoordinatesValidator
	{
		public CoordinatesValidator()
		{

		}

        public bool IsValid(int xCoordinate, int yCoodinate, char bearing)
        {
            if (xCoordinate < 0 || yCoodinate < 0)
            {
                throw new ArgumentException();
            }
            if (bearing != 'N' && bearing != 'E' && bearing != 'S' && bearing != 'W')
            {
                throw new ArgumentException();
            }
            return true;

        }
    }
}

