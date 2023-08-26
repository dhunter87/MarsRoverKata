using MarsRover.Helpers;
using MarsRover.Interfaces;

namespace MarsRover.Models
{
    public class Plateau : IPlateau
    {   
        public ICoordinate MaxCoordinates { get; set; }
        private readonly int GamePointCount;
        private readonly HashSet<IGamePoint> GamePoints;
        public Dictionary<string, IRoverPosition> RoverPositions;

        public Plateau(ICoordinate maxCoordinates, int maxGamePoints)
        {
            if (!CoordinatesValidator.IsInitialPlateauCoordinateValid(maxCoordinates))
            {
                throw new ArgumentException();
            }
            MaxCoordinates = maxCoordinates;

            GamePointCount = SetupGamepointLimit(maxGamePoints);
            GamePoints = new HashSet<IGamePoint>();

            RoverPositions = new Dictionary<string, IRoverPosition>();
        }

        private int SetupGamepointLimit(int maxGamePoints)
        {
            var maxGamePointLimit = (int)Math.Floor((MaxCoordinates.XCoordinate * MaxCoordinates.YCoordinate) * 0.2); // limited to 20% of available Plateau
            var minGamepointLimit = 1;

            return maxGamePointLimit <= 0 ? minGamepointLimit :
                maxGamePoints <= maxGamePointLimit ? maxGamePoints :
                maxGamePointLimit;
        }

        public void SetupGamePoints()
        {
            for (int i = 0; i < GamePointCount; i++)
            {
                GamePoints.Add(GamePoint.CreateGamePoint(MaxCoordinates, RoverPositions));
            }
        }

        public bool IsValildMove(ICoordinate nextCoordinates, string roverId)
        {
            if (CoordinatesValidator.IsRoverNextMoveValid(nextCoordinates, MaxCoordinates, RoverPositions, roverId))
            {
                RoverPositions[roverId].XCoordinate = nextCoordinates.XCoordinate;
                RoverPositions[roverId].YCoordinate = nextCoordinates.YCoordinate;
                return true;
            }
            return false;
        }

        public bool IsGamePointMove(ICoordinate coordinate)
        {
            return GamePoints.FirstOrDefault(p => p.EqualsCoordinates(coordinate)) != null;
        }

        public bool HasGamePoints()
        {
            return GamePoints.Count > 0;
        }

        // only used in tests??
        public HashSet<IGamePoint> GetGamePoints()
        {
            return GamePoints;
        }

        public bool AddRover(IRoverPosition position, string id)
        {
            if (CoordinatesValidator.IsUnOccupiedPosition(RoverPositions, position))
            {
                RoverPositions.Add(id, position);
                return true;
            }
            return false;
        }

        public GamePoint GetGamePoint(ICoordinate coordinate)
        {
            var matchedPoint = GamePoints.FirstOrDefault(p => p.EqualsCoordinates(coordinate));

            if (matchedPoint != null)
            {
                GamePoints.Remove(matchedPoint);

                return (GamePoint)matchedPoint;
            }

            throw new Exception();
        }

        // only used in tests?
        public List<IRoverPosition> GetRoverPositions()
        {
            return RoverPositions.Values.ToList();
        }
    }
}

