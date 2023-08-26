using MarsRover.Models;

namespace MarsRover.Interfaces
{
    public interface IPlateau
    {
        ICoordinate MaxCoordinates { get; set; }
        bool IsValildMove(ICoordinate coordinate, string roverId);
        bool IsGamePointMove(ICoordinate coordinate);
        bool HasGamePoints();
        void SetupGamePoints();
        bool AddRover(IRoverPosition position, string id);
        GamePoint GetGamePoint(ICoordinate coordinate);
        List<IRoverPosition> GetRoverPositions();
    }
}