﻿using MarsRover.Interfaces;

namespace MarsRover.Models
{
    public class Position : IPosition
    {
        public char Bearing { get; set; }

        public Position(int xCoordinate, int yCoordinate, char bearing)
        {
            XCoordinate = xCoordinate;
            YCoordinate = yCoordinate;
            Bearing = bearing;
        }

        public int XCoordinate { get; set; }
        public int YCoordinate { get; set; }
    }
}