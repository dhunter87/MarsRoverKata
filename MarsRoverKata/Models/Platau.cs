using System;
using MarsRover.Helpers;

namespace MarsRover.Models
{
    public class Platau
    {
        public int MaxYCoordinate;
        public int MaxXCoordinate;

        public (int, int) goalPoint => (x,y);
        public int x;
        public int y;

        public Platau(int maxXCoordinate, int maxYCoordinate)
        {
            if (CoordinatesValidator.IsValid(maxXCoordinate, maxYCoordinate))
            {
                MaxXCoordinate = maxXCoordinate;
                MaxYCoordinate = maxYCoordinate;
            }

            if (CoordinatesValidator.IsValid(maxXCoordinate, maxYCoordinate, MaxXCoordinate, MaxYCoordinate))
            {
                x = GenerateGoalpointCoordinate(maxXCoordinate);
                y = GenerateGoalpointCoordinate(maxYCoordinate);
            }
        }

        private int GenerateGoalpointCoordinate(int maxCoord)
        {
            Random random = new Random();
            return random.Next(maxCoord+1);
        }

        public (int, int) GetPlatauCoordinatesUpperLimits()
        {
            return (MaxXCoordinate, MaxYCoordinate);
        }

        public bool IsValildMove(int xCoordinate, int yCoordinate)
        {
            return xCoordinate >= 0 && xCoordinate <= MaxXCoordinate &&
                yCoordinate >= 0 && yCoordinate <= MaxYCoordinate;
        }
    }
}

