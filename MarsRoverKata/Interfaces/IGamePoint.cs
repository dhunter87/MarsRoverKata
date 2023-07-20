using System;
namespace MarsRover.Interfaces
{
	public interface IGamePoint : ICoordinate
	{
        int Value { get; set; }
        bool EqualsCoordinates(int xCoordinate, int yCoordinate);
    }
}

