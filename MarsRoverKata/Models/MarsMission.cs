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
    }
}

