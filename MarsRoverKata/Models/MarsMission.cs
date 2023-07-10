using System;
namespace MarsRover.Models
{
	public class MarsMission
	{
		public Platau Platau;
		public Player Player;
        private readonly int TeamLimit;
        private readonly int CommandLimit;

        public MarsMission(int maxXCoordinate, int maxYCoordinate, int teamLimit, int instructionLimit)
		{
            TeamLimit = teamLimit;
            CommandLimit = instructionLimit;
			Platau = CreatePlatau(maxXCoordinate, maxYCoordinate);
			Player = new Player(Platau, TeamLimit, CommandLimit);
		}

        public Platau CreatePlatau(int maxXCoordinate, int maxYCoordinate)
        {
			return new Platau(maxXCoordinate, maxYCoordinate);
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

