using MarsRover.Interfaces;

namespace MarsRover.Helpers
{
    public static class CoordinatesValidator
	{
        public static bool IsRoverPositionAndBearingValid(int xCoordinate, int yCoodinate, char bearing, int platauMaxXCoordinate, int platauMaxYCoordinate)
        {
            if (!IsValidPosition(xCoordinate, yCoodinate, platauMaxXCoordinate, platauMaxYCoordinate) ||
                !isValidBearing(bearing))
            {
                return false;
            }

            return true;
        }

        public static bool IsUnOccupiedPosition(Dictionary<string, IRoverPosition> roverPositions, int xCoord, int yCoord)
        {
            return !roverPositions.Any(p => p.Value.XCoordinate == xCoord && p.Value.YCoordinate == yCoord);
        }

        public static bool IsInitialPlateauCoordinateValid(int xCoordinate, int yCoodinate)
        {
            if (xCoordinate < 0 || yCoodinate < 0)
            {
                return false;
            }

            return true;
        }

        public static bool IsRoverNextMoveValid(int nextXCoordinate, int nextYCoordinate,int platauMaxXCoordinate, int platauMaxYCoordinate, Dictionary<string, IRoverPosition> roverPositions)
        {
            if (IsValidPosition(nextXCoordinate, nextYCoordinate, platauMaxXCoordinate, platauMaxYCoordinate)
                && IsUnOccupiedPosition(roverPositions, nextXCoordinate, nextYCoordinate))
            {
                return true;
            }
            return false;
        }

        private static bool isValidBearing(char bearing)
        {
            if (bearing != 'N' && bearing != 'E' && bearing != 'S' && bearing != 'W')
            {
                return false;
            }

            return true;
        }

        private static bool IsValidPosition(int xCoordinate, int yCoodinate, int platauMaxXCoordinate, int platauMaxYCoordinate)
        {
            if (xCoordinate < 0 || xCoordinate > platauMaxXCoordinate ||
                yCoodinate < 0 || yCoodinate > platauMaxYCoordinate)
            {
                return false;
            }
            return true;
        }
    }
}

