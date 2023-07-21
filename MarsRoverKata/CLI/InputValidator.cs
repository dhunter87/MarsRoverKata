using MarsRover.Interfaces;
using MarsRover.Models;
namespace MarsRover.Helpers
{
    public static class InputValidator
	{
        public static void SetupTeamRovers(List<Player> players, IPlateau plateau)
        {
            foreach (var player in players)
            {
                var counter = 1;
                var initialRoverCoordinates = SetupRoverCoordinates(plateau.MaxXCoordinate);

                var position = RoverPosition.CreateRoverPosition(initialRoverCoordinates.Value.XCoordinate, initialRoverCoordinates.Value.YCoordinate, initialRoverCoordinates.Key);

                player.AddTeamMember(position, $"Player{player.PlayerId}-Rover{counter}");

                counter++;
            }
        }

        public static KeyValuePair<char, ICoordinate> SetupRoverCoordinates(int maxCoordinate)
        {
            PrintSetupRoverCoordinateInstructions(maxCoordinate);

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

                if(!CoordinatesValidator.IsRoverPositionAndBearingValid(initialXCoordinate, initialYCoordinate, initialBearing, maxCoordinate, maxCoordinate))
                {
                    Console.WriteLine("Invalid input. Please enter valid integer coordinates and a single character bearing.");
                    continue;
                }

                return new KeyValuePair<char, ICoordinate>(initialBearing, Coordinate.CreateCoordinate(initialXCoordinate, initialYCoordinate));
            }
        }

        private static void PrintSetupRoverCoordinateInstructions(int maxCoordinate)
        {
            Console.WriteLine("Enter Rover Coordinates And Bearing to start Mars Mission!");
            Console.WriteLine("Rover Coordinates must be within Platau maximum Coordinates");
            Console.WriteLine("Bearing Must Be N (North), E (East), S (South), or W (West)");
            Console.WriteLine($"e.g. between '0,0,N' And '{maxCoordinate},{maxCoordinate},S'");
        }


        public static string SetupRoverInstructions()
        {
            PrintSetupRoverInstructions();

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

        private static void PrintSetupRoverInstructions()
        {
            Console.WriteLine("Enter Rover Instructions to Roam Mars!");
            Console.WriteLine("Instructions must be R (Rotate 90 degrees Right), L (Rotate 90 degrees Left) or M (Move forward 1 grid square)");
            Console.WriteLine("e.g. MMMMMRRMMMMMLLMMMMMRRMMM");
        }
    }
}

