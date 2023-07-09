﻿using System;
namespace MarsRover.Helpers
{
	public static class MissionInstructions
	{

		public static (int, int) GetSetupInstructions()
		{

            while (true)
            {
                Console.WriteLine("Enter max Platau Coordinates (e.g. 10,10) to Setup Mars Mission!");
                var plateauSize = Console.ReadLine().Split(',');

                if (plateauSize?.Length != 2)
                {
                    Console.WriteLine("Invalid input format.");
                    continue;
                }
                if (!int.TryParse(plateauSize[0], out int maxXCoodinate) ||
                    !int.TryParse(plateauSize[1], out int maxYCoodinate))
                {
                    Console.WriteLine("Invalid input. Please enter valid integer coordinates and a single character bearing.");
                    continue;
                }

                return (maxXCoodinate, maxXCoodinate);
            }
        }

        public static KeyValuePair<char, (int, int)> GetRoverSetupInstructions()
        {
            Console.WriteLine("Enter Rover Coordinates And Bearing to start Mars Mission!");
            Console.WriteLine("Rover Coordinates must be within Platau maximum Coordinates");
            Console.WriteLine("Bearing Must Be N (North), E (East), S (South), or W (West)");
            Console.WriteLine("e.g. '10,10,N'");

            while (true)
            {
                var initialPosition = Console.ReadLine()?.Split(',');

                if (initialPosition?.Length != 3)
                {
                    Console.WriteLine("Invalid input format.");
                    continue;
                }

                if (!int.TryParse(initialPosition[0], out int initialXCoordinate) ||
                    !int.TryParse(initialPosition[1], out int initialYCoordinate) ||
                    initialPosition[2].Length != 1)
                {
                    Console.WriteLine("Invalid input. Please enter valid integer coordinates and a single character bearing.");
                    continue;
                }

                var initialBearing = Char.ToUpper(initialPosition[2][0]);

                return new KeyValuePair<char, (int, int)>(initialBearing, (initialXCoordinate, initialYCoordinate));
            }
        }
    }
}

