using MarsRover.Interfaces;
using MarsRover.Models;

namespace MarsRover.Helpers
{
    public static class DirectionMapper
    {       
        public static Dictionary<char, (int, int)> DirectionOfTravel => DirectionDictionary;
        private static readonly Dictionary<char, (int, int)> DirectionDictionary = new()
        {
            { (char)Directions.North, (0, 1) },
            { (char)Directions.East, (1, 0) },
            { (char)Directions.South, (0, -1) },
            { (char)Directions.West, (-1, 0) }
        };

        public static Dictionary<char, int> BearingRotation => BearingDictionary;
        private static readonly Dictionary<char, int> BearingDictionary = new()
        {
            { (char)Directions.North, 0 },
            { (char)Directions.East, 1 },
            { (char)Directions.South, 2 },
            { (char)Directions.West, 3 }
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