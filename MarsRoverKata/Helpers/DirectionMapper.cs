using MarsRover.Interfaces;
using MarsRover.Models;

namespace MarsRover.Helpers
{
    public static class DirectionMapper
    {
        const char North = 'N';
        const char South = 'S';
        const char West = 'W';
        const char East = 'E';
        
        public static Dictionary<char, (int, int)> DirectionOfTravel = new Dictionary<char, (int, int)>
        {
            {North, (0, 1) },
            {East, (1, 0) },
            {South, (0, -1) },
            {West, (-1, 0) }
        };

        public static Dictionary<char, int> BearingRotation = new Dictionary<char, int>
        {
            { North, 0 },
            { East, 1 },
            { South, 2 },
            { West, 3 }
        };

        public static ICoordinate GetDirectionDelta(char currentBearing)
        {
            if (DirectionOfTravel.TryGetValue(currentBearing, out var directionDelta))
            {
                return Coordinate.CreateCoordinate(directionDelta.Item1, directionDelta.Item2);
            }
            throw new ArgumentException($"Invalid bearing: {currentBearing}");
        }

        internal static int GetCurrentBearing(char bearing)
        {
            return BearingRotation[bearing];
        }

        internal static char GetNewBearing(int newRotationValue)
        {
            return BearingRotation.Single(kvp => kvp.Value == newRotationValue).Key;
        }
    }
}

