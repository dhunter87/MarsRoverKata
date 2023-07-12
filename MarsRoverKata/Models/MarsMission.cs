using System;
using System.Collections.Generic;
using MarsRover.Interfaces;

namespace MarsRover.Models
{
	public class MarsMission
	{
		public IPlateau Plateau;
        private readonly int TeamLimit;
        private readonly int CommandLimit;
        private List<Player> Players;

        public MarsMission(int maxXCoordinate, int maxYCoordinate, int teamLimit, int instructionLimit, int players)
		{
            TeamLimit = teamLimit;
            CommandLimit = instructionLimit;
			Plateau = CreatePlatau(maxXCoordinate, maxYCoordinate);
            Players = new List<Player>();
            SetupPlayers(players);
        }

        public void SetupPlayers(int playerCount)
        {
            for (int i = 0; i < playerCount; i++)
            {
                Players.Add(new Player(Plateau, TeamLimit, CommandLimit));   
            }
        }


        public IPlateau CreatePlatau(int maxXCoordinate, int maxYCoordinate)
        {
			return new Plateau(maxXCoordinate, maxYCoordinate);
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

