using System;
using MarsRover.Interfaces;

namespace MarsRover.Models
{
	public class Player
	{
        public IPlateau Plateau;
        public List<Rover> Team;
        private int Score;
        private readonly int TeamLimit;
        private readonly int InstructionLimit;

        public Player(IPlateau platau, int teamLimit, int instructionLimit)
		{
            Score = new int();
            TeamLimit = teamLimit;
            InstructionLimit = instructionLimit;
            Plateau = platau;
            Team = new List<Rover>();
		}

        public void AddTeamMember(int xCoordinate, int yCoordinate, char bearing, string id)
        {
            if (Team.Count < TeamLimit)
            {
                var rover = new Rover(xCoordinate, yCoordinate, bearing, Plateau, id);
                Team.Add(rover);
            }   
        }

        public int GetScore()
        {
            return Score;
        }

        public void GiveRoverInstructions(Rover rover, string instructions)
        {
            Score = 0;

            if (instructions.Length > InstructionLimit)
            {
                instructions = instructions.Substring(0, InstructionLimit);
            }
            if (!string.IsNullOrEmpty(instructions))
            {
                Score += rover.ExecuteInstructions(instructions);
            }
        }
    }
}

