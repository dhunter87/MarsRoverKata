﻿using System;
namespace MarsRoverKata
{
	public class MarsRover
	{
		public int? X { get; }
		public int? Y { get; }
        public char? Bearing { get; }

        public MarsRover()
        {

        }

        public MarsRover(int xCoordinate, int yCoodinate, char bearing)
        {
            X = new int();
            Y = new int();
            Bearing = new char();
        }
    }
}

