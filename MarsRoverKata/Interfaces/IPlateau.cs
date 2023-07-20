using MarsRover.Models;

namespace MarsRover.Interfaces
{
    public interface IPlateau
    {
        int MaxXCoordinate { get; }
        int MaxYCoordinate { get; }

        bool IsValildMove(int xCoordinate, int yCoordinate);

        bool IsGamePointMove(int xCoordinate, int yCoordinate);

        bool HasGamePoints();

        void SetupGamePoints();

        void AddRover(int xCoordinate, int yCoordinate, char bearing);

        GamePoint GetGamePoint(int xCoordinate, int yCoordinate);
    }
}

