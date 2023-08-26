using MarsRover.Models;

namespace MarsRover.Interfaces
{
    public interface IGamePoint : ICoordinate
	{
        int TreasureValue { get; set; }
        Prize TreasureType { get; set; }
        bool EqualsCoordinates(ICoordinate coordinate);
    }
}