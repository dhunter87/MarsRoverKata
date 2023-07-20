using System;
using System.Collections.Generic;

namespace MarsRover.Models
{
	public class GamePoint
    {
        private int X;
        private int Y;
        //private Coordinate Point; 

        public GamePoint(int maxXCoord, int maxYCoord, List<Coordinate> RoverPositions)
        {
            var retries = 10;
            Random random = new Random();

            for (int i = 0; i < retries; i++) // Limit the number of attempts to prevent infinite loops
            {
                var randomisedXCoord = GenerateGoalpointCoordinate(maxXCoord, random);
                var randomisedYCoord = GenerateGoalpointCoordinate(maxYCoord, random);

                if (!RoverPositions.Contains(new Coordinate(randomisedXCoord, randomisedYCoord)))
                {
                    X = randomisedXCoord;
                    Y = randomisedYCoord;
                    //Point = new Coordinate(randomisedXCoord, randomisedYCoord);
                    return;
                }
            }
        }

        private int GenerateGoalpointCoordinate(int maxCoord, Random random)
        {
            return random.Next(maxCoord + 1);
        }

        public bool EqualsCoordinates(int xCoordinate, int yCoordinate)
        {
            return X == xCoordinate && Y == yCoordinate;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            GamePoint other = (GamePoint)obj;
            return X == other.X && Y == other.Y;
        }

        // Override GetHashCode based on X and Y coordinates
        public override int GetHashCode()
        {
            return X.GetHashCode() ^ Y.GetHashCode();
        }
    }
}

