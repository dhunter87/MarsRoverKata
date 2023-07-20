using System;
using MarsRover.Interfaces;

namespace MarsRover.Models
{
    public class Coordinate : ICoordinate
    {
        public int XCoordinate { get; set; }
        public int YCoordinate { get; set; }

        // Factory Method
        public static ICoordinate CreateCoordinate(int x, int y)
        {
            return new Coordinate { XCoordinate = x, YCoordinate = y };
        }
    }
}

