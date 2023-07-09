using System;
namespace MarsRover.Models
{
	public class MarsMission
	{
		public Platau Platau;
		public Player Player;

		public MarsMission(int maxXCoordinate, int maxYCoordinate)
		{
			Platau = CreatePlatau(maxXCoordinate, maxYCoordinate);
			Player = new Player(Platau);
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

