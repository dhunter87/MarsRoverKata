using MarsRover.Interfaces;

namespace MarsRover.Helpers
{
    public static class CoordinatesValidator
	{
        public static bool IsRoverPositionAndBearingValid(IRoverPosition position, ICoordinate platauMaxCoordinates)
        {
            if (!IsValidPosition(position, platauMaxCoordinates) ||
                !IsValidBearing(position.Bearing))
            {
                return false;
            }
            return true;
        }

        public static bool IsUnOccupiedPosition(Dictionary<string, IRoverPosition> roverPositions, ICoordinate coordinate)
        {
            return !roverPositions.Any(p => p.Value.XCoordinate == coordinate.XCoordinate && p.Value.YCoordinate == coordinate.YCoordinate);
        }

        public static bool IsUnOccupiedPosition(Dictionary<string, IRoverPosition> roverPositions, ICoordinate coordinate, string thisRoverId)
        {
            return !roverPositions.Any(p => p.Value.XCoordinate == coordinate.XCoordinate && p.Value.YCoordinate == coordinate.YCoordinate && p.Key != thisRoverId);
        }

        public static bool IsInitialPlateauCoordinateValid(ICoordinate coordinate)
        {
            if (coordinate.XCoordinate < 0 || coordinate.YCoordinate < 0)
            {
                return false;
            }
            return true;
        }

        public static bool IsRoverNextMoveValid(ICoordinate nextCoordinates, ICoordinate maxPlateauCoordinates, Dictionary<string, IRoverPosition> roverPositions, string roverId)
        {
            if (IsValidPosition(nextCoordinates, maxPlateauCoordinates)
                && IsUnOccupiedPosition(roverPositions, nextCoordinates, roverId))
            {
                return true;
            }
            return false;
        }

        private static bool IsValidBearing(char bearing)
        {
            if (bearing != 'N' && bearing != 'E' && bearing != 'S' && bearing != 'W')
            {
                return false;
            }
            return true;
        }

        private static bool IsValidPosition(ICoordinate nextCoordinates, ICoordinate maxPlateauCoordinates)
        {
            if (nextCoordinates.XCoordinate < 0 || nextCoordinates.XCoordinate > maxPlateauCoordinates.XCoordinate ||
                nextCoordinates.YCoordinate < 0 || nextCoordinates.YCoordinate > maxPlateauCoordinates.YCoordinate)
            {
                return false;
            }
            return true;
        }
    }
}

