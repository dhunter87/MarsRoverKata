using System;
using MarsRover.Helpers;
using MarsRover.Interfaces;

namespace MarsRover.Models
{
    public class Plateau : IPlateau
    {
        public int MaxXCoordinate { get; private set; }
        public int MaxYCoordinate { get; private set; }
        private readonly int MaxGamePoints;
        private List<GamePoint> GamePoints;
        private List<Coordinate> RoverPositions;

        public Plateau(int maxXCoordinate, int maxYCoordinate, int maxGamePoints)
        {
            if (CoordinatesValidator.IsValid(maxXCoordinate, maxYCoordinate))
            {
                MaxXCoordinate = maxXCoordinate;
                MaxYCoordinate = maxYCoordinate;
            }

            if (!CoordinatesValidator.IsValid(maxXCoordinate, maxYCoordinate, MaxXCoordinate, MaxYCoordinate))
            {
                throw new ArgumentException();
            }

            if (maxGamePoints >= 0)
            {
                MaxGamePoints = maxGamePoints;
            }

            GamePoints = new List<GamePoint>();
            RoverPositions = new List<Coordinate>();
        }

        public void SetupGamePoints()
        {
            GamePoints = new List<GamePoint>();
            for (int i = 0; i < MaxGamePoints; i++)
            {
                GamePoints.Add(new GamePoint(MaxXCoordinate, MaxYCoordinate, RoverPositions));
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

        public bool IsGamePointMove(int xCoordinate, int yCoordinate)
        {
            var matchedPoint = GamePoints.FirstOrDefault(p => p.EqualsCoordinates(xCoordinate, yCoordinate));

            if (matchedPoint != null)
            {
                GamePoints.Remove(matchedPoint);
            }

            return matchedPoint != null;
        }

        public bool HasGamePoints()
        {
            return GamePoints.Count > 0;
        }

        public List<GamePoint> GetGamePoints()
        {
            return GamePoints;
        }

        public void AddRover(int xCoordinate, int yCoordinate)
        {
            RoverPositions.Add(new Coordinate(xCoordinate, yCoordinate));
        }
    }
}

