using MarsRover.Helpers;

namespace MarsRover.Interfaces
{
    public interface IRover
	{
        public IRoverPosition Position { get; set; }

        int ExecuteInstructions(string instructions);

        int ExecuteInstruction(RoverCommand instruction);

        string GetId();
    }
}

