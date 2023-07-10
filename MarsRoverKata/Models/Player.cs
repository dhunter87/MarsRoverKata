using System;
namespace MarsRover.Models
{
	public class Player
	{
        public Platau Platau;
        public List<Rover> Team;
        private int Score;
        private readonly int TeamLimit;
        private readonly int InstructionLimit;

        public Player(Platau platau, int teamLimit, int instructionLimit)
		{
            TeamLimit = teamLimit;
            InstructionLimit = instructionLimit;
            Score = 0;
            Platau = platau;
            Team = new List<Rover>();
		}

        public void AddTeamMember(int xCoordinate, int yCoordinate, char bearing, string id)
        {
            if (Team.Count < TeamLimit)
            {
                Team.Add(new Rover(xCoordinate, yCoordinate, bearing, Platau, id));
            }
        }

        public int GetScore()
        {
            return Score;
        }

        public void GiveRoverInstructions(Rover rover, string instructions)
        {
            if (instructions.Length > InstructionLimit)
            {
                instructions = instructions.Substring(0, InstructionLimit);
            }
            if (!string.IsNullOrEmpty(instructions))
            { 
                rover.ExecuteInstructions(instructions);
            }

        }
    }
}

