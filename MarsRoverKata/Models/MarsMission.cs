using System;
using System.Collections.Generic;
using MarsRover.Helpers;
using MarsRover.Interfaces;

namespace MarsRover.Models
{
	public class MarsMission
	{
		public IPlateau Plateau;
        private readonly int TeamLimit;
        private readonly int GamePointLimit;
        private readonly int CommandLimit;
        private List<Player> Players;

        public MarsMission(MissionConfig config)
		{
            GamePointLimit = config.GamePoints;
            TeamLimit = config.MaxTeamMembers;
            CommandLimit = config.InstructionLimit;
			Plateau = CreatePlatau(config.MaxCoordinates.Item1, config.MaxCoordinates.Item2);
            Players = new List<Player>();
            SetupPlayers(config.PlayerCount);
        }

        public void SetupTeamRovers()
        {
            foreach (var player in Players)
            {
                var counter = 1;
                var initialRoverCoordinates = SetupRoverCoordinates(Plateau.MaxXCoordinate);
                player.AddTeamMember(initialRoverCoordinates.Value.Item1, initialRoverCoordinates.Value.Item2, initialRoverCoordinates.Key, $"Player{player.PlayerId}-Rover{counter}");

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


        public string SetupRoverInstructions()
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

        public void SetupPlayers(int playerCount)
        {
            for (int i = 0; i < playerCount; i++)
            {
                Players.Add(new Player(Plateau, TeamLimit, CommandLimit, i+1));   
            }
            if (Players.Count() == 0)   
            {
                Players.Add(new Player(Plateau, TeamLimit, CommandLimit, 1));
            }
        }

        public bool IsActive => Plateau.HasGamePoints();

        public IPlateau CreatePlatau(int maxXCoordinate, int maxYCoordinate)
        {
			return new Plateau(maxXCoordinate, maxYCoordinate, GamePointLimit);
        }

        public void CreateRover(Player player, int startingXCoordinate, int startingYCoordinate, char startingBearing, string id)
        {
			player.AddTeamMember(startingXCoordinate, startingYCoordinate, startingBearing, id);
        }

        public int GetCommandLimit()
        {
            return CommandLimit;
        }

        public List<Player> GetConfiguredPlayers()
        {
            return Players;
        }

        public void StartMission()
        {   
            Plateau.SetupGamePoints();
        }
    }
}

