using System;
using MarsRover.Helpers;
using MarsRover.Interfaces;

namespace MarsRover.Models
{
    public class Platau : IPlatau
    {

        private (int, int) GoalPoint => (x,y);

        public int MaxXCoordinate { get; private set; }
        public int MaxYCoordinate { get; private set; }

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

        public bool IsGamePointMove(int xCoordinate, int yCoordinate)
        {
            return xCoordinate == GoalPoint.Item1 &&
                    yCoordinate == GoalPoint.Item2;
        }
    }
}

