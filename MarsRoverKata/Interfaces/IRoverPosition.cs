namespace MarsRover.Interfaces
{
    public interface IRoverPosition : ICoordinate
	{
        char Bearing { get; set; }
    }
}