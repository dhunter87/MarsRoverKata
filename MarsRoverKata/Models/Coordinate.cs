using System;
using MarsRover.Interfaces;

namespace MarsRover.Models
{
    public class Coordinate : ICoordinate
    {
        public Coordinate(int xCoordinate, int yCoordinate)
        {
            XCoordinate = xCoordinate;
            YCoordinate = yCoordinate;
        }

        public int XCoordinate { get; set; }
        public int YCoordinate { get; set; }
    }
}

