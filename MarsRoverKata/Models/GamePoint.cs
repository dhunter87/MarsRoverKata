using System;
using System.Collections.Generic;

namespace MarsRover.Models
{
	public class GamePoint
    {
        private int x;
        private int y;

        public GamePoint(int maxXCoord, int maxYCoord)
        {
            x = GenerateGoalpointCoordinate(maxXCoord);
            y = GenerateGoalpointCoordinate(maxYCoord);
        }

        private int GenerateGoalpointCoordinate(int maxCoord)
        {
            Random random = new Random();
            return random.Next(maxCoord + 1);
        }

        public bool EqualsCoordinates(int xCoordinate, int yCoordinate)
        {
            return x == xCoordinate && y == yCoordinate;
        }
    }
}

