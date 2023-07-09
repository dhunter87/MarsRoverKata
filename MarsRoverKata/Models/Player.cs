using System;
namespace MarsRover.Models
{
	public class Player
	{
        public Platau Platau;
        public List<Rover> Team;
        private readonly int TeamLimit;
        private readonly int CommandLimit;

        public Player(Platau platau, int teamLimit, int instructionLimit)
		{
            CommandLimit = instructionLimit;
            TeamLimit = teamLimit;
            Platau = platau;
            Team = new List<Rover>();
		}

        public void AddTeamMember(int xCoordinate, int yCoordinate, char bearing)
        {
            if (Team.Count < TeamLimit)
            {
                Team.Add(new Rover(xCoordinate, yCoordinate, bearing, Platau));
            }
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

