using MarsRover.Models;

namespace MarsRover.Interfaces
{
    public interface IPlateau
    {
        int MaxXCoordinate { get; }
        int MaxYCoordinate { get; }

        bool IsValildMove(int xCoordinate, int yCoordinate, string roverId);

        bool IsGamePointMove(int xCoordinate, int yCoordinate);

        bool HasGamePoints();

        void SetupGamePoints();

        bool AddRover(IRoverPosition position, string id);

        GamePoint GetGamePoint(int xCoordinate, int yCoordinate);

        List<IRoverPosition> GetRoverPositions();
    }
}

