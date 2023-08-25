using MarsRover.CLI;
using MarsRover.Interfaces;
using MarsRover.Models;
namespace MarsRover.Helpers
{
    public static class InputValidator
	{
        public static void SetupTeamRovers(List<Player> players, IPlateau plateau)
        {
            while (players.Any(x => x.Team.Count < x.TeamLimit))
            {
                foreach (var player in players)
                {
                    // print current player
                    Console.WriteLine($"Current Player: {player.Id}");
                    // set up 1 rover at a time
                    SetupNextRover(player, plateau);
                }
            }
        }

        private static void SetupNextRover(Player player, IPlateau plateau)
        {
            var currentPlayersTeamCount = player.Team.Count;

            if (currentPlayersTeamCount < player?.TeamLimit) 
            {
                var initialRoverCoordinates = SetupRoverCoordinates(plateau.MaxCoordinates);

                var position = RoverPosition.CreateRoverPosition(initialRoverCoordinates.Value.XCoordinate, initialRoverCoordinates.Value.YCoordinate, initialRoverCoordinates.Key);
                string roverId = SetRoverId(player, currentPlayersTeamCount);

                player.AddTeamMember(position, roverId);
            }
        }

        public static void SetupTeamRoversOld(List<Player> players, IPlateau plateau)
        {
            foreach (var player in players)
            {

                for (int i = 0; i < player.TeamLimit; i++)
                {
                    var initialRoverCoordinates = SetupRoverCoordinates(plateau.MaxCoordinates);

                    var position = RoverPosition.CreateRoverPosition(initialRoverCoordinates.Value.XCoordinate, initialRoverCoordinates.Value.YCoordinate, initialRoverCoordinates.Key);
                    string roverId = SetRoverId(player, i);

                    player.AddTeamMember(position, roverId);
                }
            }
        }

        private static string SetRoverId(Player player, int i)
        {
            if (player.Id != null)
            {
                return $"{player.Id}R{i + 1}";
            }
            throw new ArgumentException();
        }

        public static KeyValuePair<char, ICoordinate> SetupRoverCoordinates(ICoordinate maxCoordinates)
        {
            PrintSetupRoverCoordinateInstructions(maxCoordinates);

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

                var initialRoverPosition = RoverPosition.CreateRoverPosition(initialXCoordinate, initialYCoordinate, initialBearing);

                if(!CoordinatesValidator.IsRoverPositionAndBearingValid(initialRoverPosition, maxCoordinates))
                {
                    Console.WriteLine("Invalid input. Please enter valid integer coordinates and a single character bearing.");
                    continue;
                }

                return new KeyValuePair<char, ICoordinate>(initialBearing, Coordinate.CreateCoordinate(initialXCoordinate, initialYCoordinate));
            }
        }

        private static void PrintSetupRoverCoordinateInstructions(ICoordinate maxCoordinate)
        {
            Console.WriteLine("Enter Rover Coordinates And Bearing!");
            Console.WriteLine("Rover Coordinates must be within Platau maximum Coordinates");
            Console.WriteLine("Bearing Must Be N (North), E (East), S (South), or W (West)");
            Console.WriteLine($"e.g. between '0,0,N' And '{maxCoordinate.XCoordinate},{maxCoordinate.YCoordinate},S'");
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

