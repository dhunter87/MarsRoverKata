using System;
using MarsRover.Interfaces;

namespace MarsRover.Models
{
	public class Player
	{
        public IPlateau Plateau;
        public List<IRover> Team;
        private int Score;
        private readonly int TeamLimit;
        private readonly int InstructionLimit;

        public int PlayerId;

        public Player(IPlateau platau, int teamLimit, int instructionLimit, int id)
		{
            PlayerId = id;
            Score = new int();
            TeamLimit = teamLimit;
            InstructionLimit = instructionLimit;
            Plateau = platau;
            Team = new List<IRover>();
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
            Console.WriteLine($"Score: {Score}");

            return Score;
        }

        public void GiveRoverInstructions(IRover rover, string instructions)
        {
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

