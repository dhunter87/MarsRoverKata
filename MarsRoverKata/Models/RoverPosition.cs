﻿using MarsRover.Interfaces;

namespace MarsRover.Models
{
    public class RoverPosition : IRoverPosition
    {
        public int XCoordinate { get; set; }
        public int YCoordinate { get; set; }
        public char Bearing { get; set; }

        // Factory Method
        public static IRoverPosition CreateRoverPosition(int xCoordinate, int yCoordinate, char bearing)
        {
            return new RoverPosition { XCoordinate = xCoordinate, YCoordinate = yCoordinate, Bearing = Char.ToUpper(bearing) };
        }
    }
}