using System;
namespace MarsRover.Models
{
	public class MarsMission
	{
		public Platau Platau;
		public Player Player;

		public MarsMission()
		{
			Player = new Player();
		}

        public void CreatePlatau(int maxXCoordinate, int maxYCoordinate)
        {
			Platau = new Platau(maxXCoordinate, maxYCoordinate);
        }

        public void CreateRover(int startingXCoordinate, int startingYCoordinate, char startingBearing)
        {
            throw new NotImplementedException();
        }
    }
}

