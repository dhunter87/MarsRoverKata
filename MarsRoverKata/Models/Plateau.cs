using MarsRover.Helpers;
using MarsRover.Interfaces;

namespace MarsRover.Models
{
    public class Plateau : IPlateau
    {   
        public int MaxXCoordinate { get; private set; }
        public int MaxYCoordinate { get; private set; }
        private readonly int GamePointCount;
        private HashSet<IGamePoint> GamePoints;

        public Dictionary<string, IRoverPosition> RoverPositions;

        public Plateau(int maxXCoordinate, int maxYCoordinate, int maxGamePoints)
        {
            if (!CoordinatesValidator.IsInitialPlateauCoordinateValid(maxXCoordinate, maxYCoordinate))
            {
                throw new ArgumentException();
            }

            MaxXCoordinate = maxXCoordinate;
            MaxYCoordinate = maxYCoordinate;
            GamePointCount = GetGamePointCount(maxGamePoints);
            GamePoints = new HashSet<IGamePoint>();

            RoverPositions = new Dictionary<string, IRoverPosition>();
        }

        private int GetGamePointCount(int maxGamePoints)
        {
            var maxGamePointLimit = (int)Math.Floor((MaxXCoordinate * MaxYCoordinate) * 0.2); // limited to 20% of available Plateau
            var minGamepointLimit = 1;

            return maxGamePointLimit <= 0 ? minGamepointLimit :
                maxGamePoints <= maxGamePointLimit ? maxGamePoints :
                maxGamePointLimit;
        }

        public void SetupGamePoints()
        {
            for (int i = 0; i < GamePointCount; i++)
            {
                GamePoints.Add(GamePoint.CreateGamePoint(MaxXCoordinate, MaxYCoordinate, RoverPositions));
            }
        }

        public bool IsValildMove(int xCoordinate, int yCoordinate)
        {
            return CoordinatesValidator.IsRoverNextMoveValid(xCoordinate, yCoordinate, MaxXCoordinate, MaxYCoordinate);
        }

        public bool IsGamePointMove(int xCoordinate, int yCoordinate)
        {
            return GamePoints.FirstOrDefault(p => p.EqualsCoordinates(xCoordinate, yCoordinate)) != null;
        }

        public bool HasGamePoints()
        {
            return GamePoints.Count > 0;
        }

        public HashSet<IGamePoint> GetGamePoints()
        {
            return GamePoints;
        }

        public void AddRover(int xCoordinate, int yCoordinate, char bearing, string id)
        {
            RoverPositions.Add(id, RoverPosition.CreateRoverPosition(xCoordinate, yCoordinate, bearing));
        }

        public GamePoint GetGamePoint(int xCoordinate, int yCoordinate)
        {
            var matchedPoint = GamePoints.FirstOrDefault(p => p.EqualsCoordinates(xCoordinate, yCoordinate));

            if (matchedPoint == null)
            {
               throw new Exception();
            }

            GamePoints.Remove(matchedPoint);

            return (GamePoint)matchedPoint;
        }

        public List<IRoverPosition> GetRoverPositions()
        {
            throw new NotImplementedException();
        }
    }
}

