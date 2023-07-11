using System;
using MarsRover.Interfaces;
using MarsRover.Models;

namespace MarsRover.Helpers
{
	public static class CoordinatesValidator
	{
        public static bool IsValid(int xCoordinate, int yCoodinate, char bearing, IPlateau plateau)
        {

            if (xCoordinate < 0 || xCoordinate > plateau.MaxXCoordinate ||
                yCoodinate < 0 || yCoodinate > plateau.MaxYCoordinate)
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

        public static bool IsValid(int maxXCoordinate, int maxYCoordinate,int platauMaxXCoordinate, int platauMaxYCoordinate)
        {
            if (maxXCoordinate >= 0 && maxXCoordinate <= platauMaxXCoordinate &&
                maxYCoordinate >= 0 && maxYCoordinate <= platauMaxYCoordinate)
            {
                return true;
            }
            return false;
        }
    }
}

