﻿using System;
using MarsRover.Helpers;
using MarsRover.Interfaces;
using MarsRover.Models;

namespace MarsRoverUnitTests.Dummies
{
	public class PlateauFake : IPlateau
	{
        public int MaxXCoordinate { get; private set; }
        public int MaxYCoordinate { get; private set; }
        public List<ICoordinate> GamePoints;
        private Dictionary<string, IRoverPosition> RoverPositions;

        public PlateauFake(int maxXCoordinate, int maxYCoordinate)
		{
            MaxXCoordinate = maxXCoordinate;
            MaxYCoordinate = maxYCoordinate;
            GamePoints = new List<ICoordinate>();
            RoverPositions = new Dictionary<string, IRoverPosition>();
        }

        public void GenerateGamePoint(int xCoordinate, int yCoordinate)
        {
            GamePoints.Add(Coordinate.CreateCoordinate(xCoordinate, yCoordinate));
        }

        public ICoordinate GetPlatauCoordinatesUpperLimits()
        {
            throw new NotImplementedException();    
        }

        public bool IsValildMove(int xCoordinate, int yCoordinate, string roverId)
        {
            return CoordinatesValidator.IsRoverNextMoveValid(xCoordinate, yCoordinate, MaxXCoordinate, MaxYCoordinate, RoverPositions, roverId);

        }

        public bool IsGamePointMove(int xCoordinate, int yCoordinate)
        {
            var matchedPoint = GamePoints.Any(p => p.XCoordinate == xCoordinate && p.YCoordinate == yCoordinate);

            return matchedPoint == true;
        }

        public bool HasGamePoints()
        {
            return true;
        }

        public void SetupGamePoints()
        {
            throw new NotImplementedException();
        }

        public bool AddRover(IRoverPosition position, string id)
        {
            RoverPositions.Add(id, position);
            return true;
        }

        public GamePoint GetGamePoint(int xCoordinate, int yCoordinate)
        {
            throw new NotImplementedException();
        }

        public List<IRoverPosition> GetRoverPositions()
        {
            throw new NotImplementedException();
        }
    }
}

    