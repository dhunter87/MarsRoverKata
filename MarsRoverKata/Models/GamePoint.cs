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
        public static IGamePoint CreateGamePoint(ICoordinate maxCoordinates, Dictionary<string, IRoverPosition> roverPositions)
        {
            Random random = new Random();
            int randomisedXCoord;
            int randomisedYCoord;
            Prize prize;

            for (int retry = 0; retry < 100; retry++)
            {
                randomisedXCoord = GenerateGoalpointCoordinate(maxCoordinates.XCoordinate, random);
                randomisedYCoord = GenerateGoalpointCoordinate(maxCoordinates.YCoordinate, random);

                var randomCoordinate = Coordinate.CreateCoordinate(randomisedXCoord, randomisedYCoord);

                prize = GenerateGoalPointPrize(random);

                if (CoordinatesValidator.IsUnOccupiedPosition(roverPositions, randomCoordinate))
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

        public bool EqualsCoordinates(ICoordinate coordinate)
        {
            return XCoordinate == coordinate.XCoordinate && YCoordinate == coordinate.YCoordinate;
        }
    }
}

