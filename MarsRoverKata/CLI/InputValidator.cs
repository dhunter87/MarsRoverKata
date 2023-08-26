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
                    SetupNextRover(player, plateau);
                }
            }
        }

        private static void SetupNextRover(Player player, IPlateau plateau)
        {
            var currentPlayersTeamCount = player.Team.Count;

            if (currentPlayersTeamCount < player?.TeamLimit) 
            {
                var initialRoverCoordinates = SetupRoverCoordinates(player, plateau.MaxCoordinates);

                var coordinate = Coordinate.CreateCoordinate(initialRoverCoordinates.Value.XCoordinate, initialRoverCoordinates.Value.YCoordinate);

                var position = RoverPosition.CreateRoverPosition(coordinate, initialRoverCoordinates.Key);
                string roverId = SetRoverId(player, currentPlayersTeamCount);

                player.AddTeamMember(position, roverId);
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

        public static KeyValuePair<char, ICoordinate> SetupRoverCoordinates(Player player, ICoordinate maxCoordinates)
        {
            PrintSetupRoverCoordinateInstructions(player, maxCoordinates);

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

                var initialBearing = Char.ToUpper(Char.Parse(initialPosition[2]));
                var coordinate = Coordinate.CreateCoordinate(initialXCoordinate, initialYCoordinate);

                var initialRoverPosition = RoverPosition.CreateRoverPosition(coordinate, initialBearing);

                if(!CoordinatesValidator.IsRoverPositionAndBearingValid(initialRoverPosition, maxCoordinates))
                {
                    Console.WriteLine("Invalid input. Please enter valid integer coordinates and a single character bearing.");
                    continue;
                }

                return new KeyValuePair<char, ICoordinate>(initialBearing, Coordinate.CreateCoordinate(initialXCoordinate, initialYCoordinate));
            }
        }

        private static void PrintSetupRoverCoordinateInstructions(Player player, ICoordinate maxCoordinate)
        {
            Console.WriteLine($"Current Player: {player.Id}, Enter Rover {player.Team.Count + 1} Coordinates And Bearing!");
            Console.WriteLine("Rover Coordinates must be within Platau maximum Coordinates");
            Console.WriteLine("Bearing Must Be N (North), E (East), S (South), or W (West)");
            Console.WriteLine($"e.g. between '0,0,N' And '{maxCoordinate.XCoordinate},{maxCoordinate.YCoordinate},S'");
        }

        public static string SetupRoverInstructions(Player player)
        {
            PrintSetupRoverInstructions(player);

            while (true)
            {
                Console.Write("\nInstructions:");
                var instructions = Console.ReadLine();

                if (instructions == null)
                {
                    Console.WriteLine("Invalid input.");
                    continue;
                }

                return instructions;
            }
        }

        private static void PrintSetupRoverInstructions(Player player)
        {
            Console.WriteLine($"Current Player: {player.Id}, Current Rover: {player.GetNextRover().GetId()}, Enter Rover Instructions to Roam Mars!");
            Console.WriteLine("Instructions must be R (Rotate 90 degrees Right), L (Rotate 90 degrees Left) or M (Move forward 1 grid square)");

            var instructionsExample = "MMMMRMRMMMMMLLMMMMMRRMMM";
            Console.WriteLine($"Instruction Limit: {player.InstructionLimit} E.G. {instructionsExample[..player.InstructionLimit]}");
        }
    }
}