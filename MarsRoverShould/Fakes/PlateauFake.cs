using System;       
using MarsRover.Interfaces;
using MarsRover.Models;

namespace MarsRoverUnitTests.Dummies
{
	public class PlateauFake : IPlateau
	{
        public int MaxXCoordinate { get; private set; }
        public int MaxYCoordinate { get; private set; }
        public List<(int,int)> GamePoints;

        public PlateauFake(int maxXCoordinate, int maxYCoordinate)
		{
            MaxXCoordinate = maxXCoordinate;
            MaxYCoordinate = maxYCoordinate;
            GamePoints = new List<(int, int)>();
        }

        public void GenerateGamePoint(int xCoordinate, int yCoordinate)
        {
            GamePoints.Add((xCoordinate, yCoordinate));
        }

        public (int, int) GetPlatauCoordinatesUpperLimits()
        {
            throw new NotImplementedException();    
        }

        public bool IsValildMove(int xCoordinate, int yCoordinate)
        {
            return xCoordinate >= 0 && xCoordinate <= MaxXCoordinate &&
                yCoordinate >= 0 && yCoordinate <= MaxYCoordinate;
        }

        public bool IsGamePointMove(int xCoordinate, int yCoordinate)
        {
            var matchedPoint = GamePoints.Any(p => p.Item1 == xCoordinate && p.Item2 == yCoordinate);

            return matchedPoint == true;
        }

        public bool HasGamePoints()
        {
            return true;
        }
    }
}

    