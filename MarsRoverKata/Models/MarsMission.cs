using System;
namespace MarsRover.Models
{
	public class MarsMission
	{
		public Platau Platau;
		public Player Player;

		public MarsMission()
		{
		}

        public void CreatePlatau(int maxXCoordinate, int maxYCoordinate)
        {
			Platau = new Platau(maxXCoordinate, maxYCoordinate);
        }
    }

    public class Player
    {
    }
}

