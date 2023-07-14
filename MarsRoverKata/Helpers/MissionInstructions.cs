using System;
using System.Reflection;
using MarsRover.Models;

namespace MarsRover.Helpers
{
	public static class MissionSetup
	{
        public static MissionConfig CreateMissionConfig()
        {
            var maxCoordinates = SetupPlateauCoordinates();
            var gamePoints = SetupGamepoints();
            var maxTeamMembers = SetUpTeamLimits();
            var instructionLimit = SetupInstructionLimit();
            var playerCount = SetupPlayerCount();

            return new MissionConfig(maxCoordinates, gamePoints, maxTeamMembers, instructionLimit, playerCount);
        }

        private static int SetupGamepoints()
        {
            while (true)
            {
                var gamePointsCount = 0;

                Console.WriteLine("Select Mission Difficulty. 1 = Easy, 2 = Medium, 3 = Hard!");
                var difficulty = Console.ReadLine();

                if (!int.TryParse(difficulty, out int difficultyLevel))
                {
                    Console.WriteLine("Invalid input format.");
                    continue;

                }
                if (difficultyLevel <= 0)
                {
                    switch (difficultyLevel)
                    {
                        case 1:
                            gamePointsCount = 3;
                            break;
                        case 2:
                            gamePointsCount = 5;
                            break;
                        case 3:
                            gamePointsCount = 10;
                            break;
                    }
                }

                return gamePointsCount;
            }
        }

        public static void SetupTeamRovers(List<Player> players,  MarsMission mission)
        {
            foreach (var player in players)
            {
                var counter = 1;
                var initialRoverCoordinates = MissionSetup.SetupRoverCoordinates();
                mission.CreateRover(player, initialRoverCoordinates.Value.Item1,
                                    initialRoverCoordinates.Value.Item2,
                                    initialRoverCoordinates.Key,
                                    $"Player{player.PlayerId}-Rover{counter}");
                counter++;
            }
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

		private static (int, int) SetupPlateauCoordinates()
		{
            while (true)
            {
                Console.WriteLine("Enter max Plateau Coordinates (e.g. 10,10) to Setup Mars Mission!");
                var plateauSize = Console.ReadLine()?.Split(',');

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

        private static int SetUpTeamLimits()
        {
            while (true)
            {
                Console.WriteLine("Enter max Number of Rovers a Player can have on their Team! (Max players: 10)");
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

        private static KeyValuePair<char, (int, int)> SetupRoverCoordinates()
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

        private static int SetupInstructionLimit()
        {
            Console.WriteLine("Enter Number of Instructions each Rover can take per move!");
            var instructions = Console.ReadLine();

            if (!int.TryParse(instructions, out int instructionLimit))
            {
                Console.WriteLine("Invalid input. Limit set to 5 by default");
                return 5;
            }

            return instructionLimit;
        }


    }
}

