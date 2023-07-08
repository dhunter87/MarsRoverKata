using System;
namespace MarsRoverKata.Mappers
{
	public class Mapper
	{
        const char North = 'N';
        const char South = 'S';
        const char West = 'W';
        const char East = 'E';
        
        public Dictionary<char, (int, int)> DirectionOfTravel = new Dictionary<char, (int, int)>
        {
            {North, (0, 1) },
            {East, (1, 0) },
            {South, (0, -1) },
            {West, (-1, 0) }
        };

        public Dictionary<char, int> BearingRotation = new Dictionary<char, int>
        {
            { North, 0 },
            { East, 1 },
            { South, 2 },
            { West, 3 }
        };


        public (int, int) GetDirectionDelta(char currentBearing)
        {
            if (DirectionOfTravel.TryGetValue(currentBearing, out var directionDelta))
            {
                return directionDelta;
            }
            throw new ArgumentException($"Invalid bearing: {currentBearing}");
        }

        internal int GetCurrentBearing(Position position)
        {
            return BearingRotation[position.Bearing];
        }

        internal char GetNewBearing(int newRotationValue)
        {
            return BearingRotation.Single(kvp => kvp.Value == newRotationValue).Key;
        }
    }
}

