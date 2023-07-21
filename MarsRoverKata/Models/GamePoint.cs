using System;
using System.Collections.Generic;
using MarsRover.Helpers;
using MarsRover.Interfaces;

namespace MarsRover.Models
{
	public class GamePoint : IGamePoint
    {
        public int XCoordinate { get; set; }
        public int YCoordinate { get; set; }
        public int TreasureValue { get; set; }
        public Prize TreasureType { get; set; }

        // Factory Method
        public static IGamePoint CreateGamePoint(int maxXCoord, int maxYCoord, List<IRoverPosition> roverPositions)
        {
            Random random = new Random();
            int randomisedXCoord;
            int randomisedYCoord;
            Prize prize;

            for (int retry = 0; retry < 100; retry++)
            {
                randomisedXCoord = GenerateGoalpointCoordinate(maxXCoord, random);
                randomisedYCoord = GenerateGoalpointCoordinate(maxYCoord, random);
                prize = GenerateGoalPointPrize(random);

                if (CoordinatesValidator.IsUnOccupiedPosition(roverPositions, randomisedXCoord, randomisedYCoord))
                {
                    return new GamePoint
                    {
                        XCoordinate = randomisedXCoord,
                        YCoordinate = randomisedYCoord,
                        TreasureType = prize,
                        TreasureValue = (int)prize
                    };
                }
            }

            throw new InvalidOperationException("Unable to find a suitable position for the GamePoint after 100 tries.");
        }

        private static Prize GenerateGoalPointPrize(Random random)
        {
            return (Prize)Enum.Parse(typeof(Prize), random.Next(1,4).ToString());
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

