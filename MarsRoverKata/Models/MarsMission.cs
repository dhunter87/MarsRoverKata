using System;
using MarsRover.Interfaces;

namespace MarsRover.Models
{
	public class MarsMission
	{
		public IPlateau Plateau;
		public Player Player;
        private readonly int TeamLimit;
        private readonly int CommandLimit;

        public MarsMission(int maxXCoordinate, int maxYCoordinate, int teamLimit, int instructionLimit)
		{
            TeamLimit = teamLimit;
            CommandLimit = instructionLimit;
			Plateau = CreatePlatau(maxXCoordinate, maxYCoordinate);
			Player = new Player(Plateau, TeamLimit, CommandLimit);
		}

        public IPlateau CreatePlatau(int maxXCoordinate, int maxYCoordinate)
        {
			return new Plateau(maxXCoordinate, maxYCoordinate);
        }

        public void CreateRover(int startingXCoordinate, int startingYCoordinate, char startingBearing, string id)
        {
			Player.AddTeamMember(startingXCoordinate, startingYCoordinate, startingBearing, id);
        }

        public int GetCommandLimit()
        {
            return CommandLimit;
        }
    }
}

