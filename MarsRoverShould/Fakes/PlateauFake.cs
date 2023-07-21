using System;       
using MarsRover.Interfaces;
using MarsRover.Models;

namespace MarsRoverUnitTests.Dummies
{
	public class PlateauFake : IPlateau
	{
        public int MaxXCoordinate { get; private set; }
        public int MaxYCoordinate { get; private set; }
        public List<ICoordinate> GamePoints;
        private List<IRoverPosition> RoverPositions;

        public PlateauFake(int maxXCoordinate, int maxYCoordinate)
		{
            MaxXCoordinate = maxXCoordinate;
            MaxYCoordinate = maxYCoordinate;
            GamePoints = new List<ICoordinate>();
            RoverPositions = new List<IRoverPosition>();
        }

        public void GenerateGamePoint(int xCoordinate, int yCoordinate)
        {
            GamePoints.Add(Coordinate.CreateCoordinate(xCoordinate, yCoordinate));
        }

        public ICoordinate GetPlatauCoordinatesUpperLimits()
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
            var matchedPoint = GamePoints.Any(p => p.XCoordinate == xCoordinate && p.YCoordinate == yCoordinate);

            return matchedPoint == true;
        }

        public bool HasGamePoints()
        {
            return true;
        }

        public void SetupGamePoints()
        {
            throw new NotImplementedException();
        }

        public bool AddRover(int xCoordinate, int yCoordinate, char bearing, string id)
        {
            var coord = RoverPosition.CreateRoverPosition(xCoordinate, yCoordinate, bearing); 
            RoverPositions.Add(coord);
            return true;
        }

        public GamePoint GetGamePoint(int xCoordinate, int yCoordinate)
        {
            throw new NotImplementedException();
        }

        public List<IRoverPosition> GetRoverPositions()
        {
            throw new NotImplementedException();
        }
    }
}

    