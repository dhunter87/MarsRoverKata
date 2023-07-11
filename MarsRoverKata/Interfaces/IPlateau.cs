using System;
using MarsRover.Models;

namespace MarsRover.Interfaces
{
    public interface IPlateau
    {
        int MaxXCoordinate { get; }
        int MaxYCoordinate { get; }

        (int, int) GetPlatauCoordinatesUpperLimits();

        bool IsValildMove(int xCoordinate, int yCoordinate);

        bool IsGamePointMove(int xCoordinate, int yCoordinate);

        bool HasGamePoints();
    }
}

