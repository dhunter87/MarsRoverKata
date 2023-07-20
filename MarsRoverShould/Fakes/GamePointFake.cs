using System;
using MarsRover.Interfaces;
using MarsRover.Models;

namespace MarsRoverUnitTests.Fakes
{
	public class GamePointFake : IGamePoint
    {
		public GamePointFake(int treasureValue, Prize prize)
		{
            TreasureValue = treasureValue;
            TreasureType = prize;
		}

        public int TreasureValue { get; set; }
        public Prize TreasureType { get; set; }
        public int XCoordinate { get; set; }
        public int YCoordinate { get; set; }

        public bool EqualsCoordinates(int xCoordinate, int yCoordinate)
        {
            throw new NotImplementedException();
        }
    }
}

