using System;
namespace MarsRover.Models
{
	public class MarsMission
	{
		public Platau Platau;
		public Player Player;

		public MarsMission(int maxXCoordinate, int maxYCoordinate, int teamLimit, int instructionLimit)
		{
			Platau = CreatePlatau(maxXCoordinate, maxYCoordinate);
			Player = new Player(Platau, teamLimit, instructionLimit);
		}

        public Platau CreatePlatau(int maxXCoordinate, int maxYCoordinate)
        {
			return new Platau(maxXCoordinate, maxYCoordinate);
        }

        public void CreateRover(int startingXCoordinate, int startingYCoordinate, char startingBearing)
        {
			Player.AddTeamMember(startingXCoordinate, startingYCoordinate, startingBearing);
        }
    }
}

