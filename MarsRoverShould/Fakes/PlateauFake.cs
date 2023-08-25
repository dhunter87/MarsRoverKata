using System;
using MarsRover.Helpers;
using MarsRover.Interfaces;
using MarsRover.Models;

namespace MarsRoverUnitTests.Dummies
{
	public class PlateauFake : IPlateau
	{
        public ICoordinate MaxCoordinates { get; set; }
        public List<ICoordinate> GamePoints;
        private Dictionary<string, IRoverPosition> RoverPositions;

        public PlateauFake(int maxXCoordinate, int maxYCoordinate)
		{
            MaxCoordinates = Coordinate.CreateCoordinate(maxXCoordinate, maxYCoordinate);
            GamePoints = new List<ICoordinate>();
            RoverPositions = new Dictionary<string, IRoverPosition>();
        }

        public void GenerateGamePoint(ICoordinate coordinate)
        {
            GamePoints.Add(coordinate);
        }

        public ICoordinate GetPlatauCoordinatesUpperLimits()
        {
            throw new NotImplementedException();    
        }

        public bool IsValildMove(ICoordinate coordinate, string roverId)
        {
            return CoordinatesValidator.IsRoverNextMoveValid(coordinate, MaxCoordinates, RoverPositions, roverId);

        }

        public bool IsGamePointMove(ICoordinate coordinate)
        {
            var matchedPoint = GamePoints.Any(p => p.XCoordinate == coordinate.XCoordinate && p.YCoordinate == coordinate.YCoordinate);

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

        public bool AddRover(IRoverPosition position, string id)
        {
            RoverPositions.Add(id, position);
            return true;
        }

        public GamePoint GetGamePoint(ICoordinate coordinate)
        {
            throw new NotImplementedException();
        }

        public List<IRoverPosition> GetRoverPositions()
        {
            throw new NotImplementedException();
        }
    }
}

    