using System;
namespace MarsRoverKata.Helpers
{
	public static class CoordinatesValidator
	{
        public static bool IsValid(int xCoordinate, int yCoodinate, char bearing)
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

        public static bool IsValid(int xCoordinate, int yCoodinate)
        {
            if (xCoordinate < 0 || yCoodinate < 0)
            {
                throw new ArgumentException();
            }

            return true;
        }
    }
}

