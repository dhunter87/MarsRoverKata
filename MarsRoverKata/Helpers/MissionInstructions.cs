using System;
namespace MarsRover.Helpers
{
	public static class MissionInstructions
	{

		public static (int, int) SetupPlatauCoordinates()
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

        public static int SetUpTeamLimits()
        {
            while (true)
            {
                Console.WriteLine("Enter max Number of Rovers a Player can have on their Team!");
                var teamLimit = Console.ReadLine();

                if (!int.TryParse(teamLimit, out int maxTeamMembers))
                {
                    Console.WriteLine("Invalid input format.");
                    continue;

                }
                if (maxTeamMembers <= 0 || maxTeamMembers > 10)
                {
                    Console.WriteLine("Invalid input - out of range.");
                    continue;
                }

                return maxTeamMembers;
            }
        }

        public static KeyValuePair<char, (int, int)> SetupRoverCoordinates()
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

        public static string SetupRoverInstructions()
        {
            Console.WriteLine("Enter Rover Instructions to Roam Mars!");
            Console.WriteLine("Instructions must be R (Rotate 90 degrees Right), L (Rotate 90 degrees Left) or M (Move forward 1 grid square)");
            Console.WriteLine("e.g. MMMMMRRMMMMMLLMMMMMRRMMM");

            while (true)
            {
                var instructions = Console.ReadLine();

                if (instructions == null)
                {
                    Console.WriteLine("Invalid input.");
                    continue;
                }

                return instructions;
            }
        }
    }
}

