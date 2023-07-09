using System;
namespace MarsRover.Models
{
	public class Player
	{
        public Platau Platau;
        public List<Rover> Team;

        public Player(Platau platau)
		{
            Platau = platau;
            Team = new List<Rover>();
		}

        public void AddTeamMember(int xCoordinate, int yCoordinate, char bearing)
        {
            Team.Add(new Rover(xCoordinate, yCoordinate, bearing, Platau));
        }

        public void GiveRoverInstructions(string instructions)
        {
            if (!string.IsNullOrEmpty(instructions))
            {
                foreach(var rover in Team)
                {
                    rover.ExecuteInstructions(instructions);
                }
            }
        }
    }
}

