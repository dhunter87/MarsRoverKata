using MarsRover.Helpers;
using MarsRover.Interfaces;

namespace MarsRover.Models
{
    public class Plateau : IPlateau
    {
        public int MaxXCoordinate { get; private set; }
        public int MaxYCoordinate { get; private set; }
        private readonly int MaxGamePoints;
        private HashSet<IGamePoint> GamePoints;
        private List<IRoverPosition> RoverPositions;

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

            GamePoints = new HashSet<IGamePoint>();
            RoverPositions = new List<IRoverPosition>();
        }

        public void SetupGamePoints()
        {
            for (int i = 0; i < MaxGamePoints; i++)
            {
                GamePoints.Add(GamePoint.CreateGamePoint(MaxXCoordinate, MaxYCoordinate, RoverPositions));
            }
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

        public HashSet<IGamePoint> GetGamePoints()
        {
            return GamePoints;
        }

        public void AddRover(int xCoordinate, int yCoordinate, char bearing)
        {
            RoverPositions.Add(RoverPosition.CreateRoverPosition(xCoordinate, yCoordinate, bearing));
        }
    }
}

