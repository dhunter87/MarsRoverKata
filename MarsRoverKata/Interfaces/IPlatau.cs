using System;
namespace MarsRover.Interfaces
{
    public interface IPlatau
    {
        int MaxXCoordinate { get; }
        int MaxYCoordinate { get; }

        (int, int) GetPlatauCoordinatesUpperLimits();

        bool IsValildMove(int xCoordinate, int yCoordinate);

        bool IsGamePointMove(int xCoordinate, int yCoordinate);
    }
}

