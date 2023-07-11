using System;       
using MarsRover.Interfaces;

namespace MarsRoverUnitTests.Dummies
{
	public class PlatauFake : IPlatau
	{
        public int MaxXCoordinate { get; private set; }
        public int MaxYCoordinate { get; private set; }
        private (int, int) GoalPoint => (0,1);

        public PlatauFake(int maxXCoordinate, int maxYCoordinate)
		{
            MaxXCoordinate = maxXCoordinate;
            MaxYCoordinate = maxYCoordinate;    
        }

        public (int, int) GetPlatauCoordinatesUpperLimits()
        {
            throw new NotImplementedException();
        }

        public bool IsValildMove(int xCoordinate, int yCoordinate)
        {
            return true;
        }

        public bool IsGamePointMove(int xCoordinate, int yCoordinate)
        {
            return true;
        }
    }
}

    