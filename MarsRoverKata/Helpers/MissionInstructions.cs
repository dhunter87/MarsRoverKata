using System;
using System.Reflection;
using MarsRover.Configuration;
using MarsRover.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MarsRover.Helpers
{
	public static class MissionSetup
	{
        public static MissionConfig CreateMissionConfig()
        {

            var difficultySetting = GetDifficultySetting();

            var maxCoordinates = SetupPlateauCoordinates(difficultySetting);
            var gamePoints = SetupGamepoints(difficultySetting);
            var maxTeamMembers = SetUpTeamLimits(difficultySetting);
            var instructionLimit = SetupInstructionLimit(difficultySetting);
            var playerCount = SetupPlayerCount();

            return new MissionConfig(maxCoordinates, gamePoints, maxTeamMembers, instructionLimit, playerCount);
        }

        private static DifficultySetting GetDifficultySetting()
        {
            DifficultySetting difficultySetting;
            while (true)
            {
                Console.WriteLine("Select Mission Difficulty. 1 = Easy, 2 = Medium, 3 = Hard!");
                var difficulty = Console.ReadLine();

                if (!int.TryParse(difficulty, out int difficultyLevel))
                {
                    Console.WriteLine("Invalid input format.");
                    continue;
                }

                difficultySetting = (DifficultySetting)Enum.Parse(typeof(DifficultySetting), difficultyLevel.ToString());
                break;
            }
            return difficultySetting;
        }

        private static (int, int) SetupPlateauCoordinates(DifficultySetting setting)
        {
            var maxCoordinateDictionary = new Dictionary<DifficultySetting, int> { { DifficultySetting.Easy, 5 }, { DifficultySetting.Medium, 10 }, { DifficultySetting.Hard, 15 } };

            if (!maxCoordinateDictionary.TryGetValue(setting, out int maxCoord))
            {
                Console.WriteLine($"The maximum coordinate for {setting} is: {maxCoord}");
            }

            return (maxCoord, maxCoord);
        }

        private static int SetupGamepoints(DifficultySetting setting)
        {
            switch (setting)
            {
                case DifficultySetting.Easy:
                    return 1;
                case DifficultySetting.Medium:
                    return 3;
                case DifficultySetting.Hard:
                    return 5;
            }
            return 0;
        }

        private static int SetUpTeamLimits(DifficultySetting setting)
        {
            // default number of rovers set to 1
            // possible bug in directing multiple rovers
            // reqs better UI to properly utilise multiple rovers
            return 1;
        }


        private static int SetupInstructionLimit(DifficultySetting setting)
        {
            switch (setting)
            {
                case DifficultySetting.Easy:
                    return 5;
                case DifficultySetting.Medium:
                    return 10;
                case DifficultySetting.Hard:
                    return 5;
            }
            return 0;
        }

        private static int SetupPlayerCount()
        {
            while (true)
            {
                Console.WriteLine("Enter Number Players Joining 'Mars Mission!'");
                var playerCount = Console.ReadLine();

                if (!int.TryParse(playerCount, out int players))
                {
                    Console.WriteLine("Invalid input format.");
                    continue;

                }
                if (players <= 0)
                {
                    Console.WriteLine("Invalid input - out of range.");
                    continue;
                }

                return players;
            }
        }

        public static void SetupTeamRovers(List<Player> players,  MarsMission mission)
        {
            foreach (var player in players)
            {
                var counter = 1;
                var initialRoverCoordinates = SetupRoverCoordinates(mission.Plateau.MaxXCoordinate);
                mission.CreateRover(player, initialRoverCoordinates.Value.Item1,
                                    initialRoverCoordinates.Value.Item2,
                                    initialRoverCoordinates.Key,
                                    $"Player{player.PlayerId}-Rover{counter}");
                counter++;
            }
        }

        private static KeyValuePair<char, (int, int)> SetupRoverCoordinates(int maxCoordinate)
        {
            Console.WriteLine("Enter Rover Coordinates And Bearing to start Mars Mission!");
            Console.WriteLine("Rover Coordinates must be within Platau maximum Coordinates");
            Console.WriteLine("Bearing Must Be N (North), E (East), S (South), or W (West)");
            Console.WriteLine($"e.g. between '0,0,N' And '{maxCoordinate},{maxCoordinate},S'");

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

