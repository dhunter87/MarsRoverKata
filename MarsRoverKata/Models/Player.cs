﻿using System;
namespace MarsRover.Models
{
	public class Player
	{
        public Platau Platau;
        public List<Rover> Team;
        private int? Score;
        public Player(Platau platau, int teamLimit, int instructionLimit)
		{
            Score = 0;
            Platau = platau;
            Team = new List<Rover>();
		}

        public void AddTeamMember(int xCoordinate, int yCoordinate, char bearing, int teamLimit, string id)
        {
            if (Team.Count < teamLimit)
            {
                Team.Add(new Rover(xCoordinate, yCoordinate, bearing, Platau, id));
            }
        }

        public object GetScore()
        {
            throw new NotImplementedException();
        }

        public void GiveRoverInstructions(Rover rover, string instructions, int instructionLimit)
        {
            if (instructions.Length > instructionLimit)
            {
                instructions = instructions.Substring(0, instructionLimit);
            }
            if (!string.IsNullOrEmpty(instructions))
            { 
                rover.ExecuteInstructions(instructions);
            }

        }
    }
}

