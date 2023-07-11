using System;
using MarsRover.Helpers;
using MarsRover.Models;

namespace MarsRover.Interfaces
{
	public interface IRover
	{
        public Position Position { get; set; }

        int ExecuteInstructions(string instructions, List<char>? invalidCommands = null, bool isRecursiveCall = false, int score = 0);

        int ExecuteInstruction(RoverCommand instruction);

        string GetId();
    }
}

