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

        public void AddTeamMember()
        {
            Team.Add(new Rover(0,0,'N', Platau));
        }
    }
}

