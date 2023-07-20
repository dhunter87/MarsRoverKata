using MarsRover.Helpers;

namespace MarsRover.Interfaces
{
    public interface IRover
	{
        public IRoverPosition Position { get; set; }

        List<IGamePoint> ExecuteInstructions(string instructions);

        void ExecuteInstruction(RoverCommand instruction);

        string GetId();
    }
}

