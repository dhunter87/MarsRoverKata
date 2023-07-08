using System;
using MarsRoverKata.Helpers;

namespace MarsRoverKata.Models
{
    public class Platau
    {
        public int MaxYCoordinate;
        public int MaxXCoordinate;

        public Platau(int maxXCoordinate, int maxYCoordinate)
        {
            if (CoordinatesValidator.IsValid(maxXCoordinate, maxYCoordinate))
            {
                MaxXCoordinate = maxXCoordinate;
                MaxYCoordinate = maxYCoordinate;
            }
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

