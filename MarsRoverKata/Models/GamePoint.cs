using System;
using System.Collections.Generic;
using MarsRover.Interfaces;

namespace MarsRover.Models
{
	public class GamePoint : IGamePoint
    {
        public int XCoordinate { get; set; }
        public int YCoordinate { get; set; }
        public int Value { get; set; }

        // Factory Method
        public static IGamePoint CreateGamePoint(int maxXCoord, int maxYCoord, List<IRoverPosition> roverPositions, int value = 1)
        {
            Random random = new Random();
            int randomisedXCoord;
            int randomisedYCoord;

            for (int retry = 0; retry < 100; retry++)
            {
                randomisedXCoord = GenerateGoalpointCoordinate(maxXCoord, random);
                randomisedYCoord = GenerateGoalpointCoordinate(maxYCoord, random);

                if (!roverPositions.Any(p => p.XCoordinate == randomisedXCoord && p.YCoordinate == randomisedYCoord))
                {
                    return new GamePoint { XCoordinate = randomisedXCoord, YCoordinate = randomisedYCoord, Value = value };
                }
            }

            throw new InvalidOperationException("Unable to find a suitable position for the GamePoint after 100 tries.");
        }

        private static int GenerateGoalpointCoordinate(int maxCoord, Random random)
        {
            return random.Next(maxCoord + 1);
        }

        public bool EqualsCoordinates(int xCoordinate, int yCoordinate)
        {
            return XCoordinate == xCoordinate && YCoordinate == yCoordinate;
        }
    }
}

