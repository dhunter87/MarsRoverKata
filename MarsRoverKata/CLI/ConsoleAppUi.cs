using System;
using System.Collections.Generic;
using MarsRover.Interfaces;
using MarsRover.Models;
using System.Linq;

namespace MarsRover.CLI
{
    public static class ConsoleAppUi
    {
        public static void PrintRoverPositions(MarsMission mission)
        {
            Dictionary<ICoordinate, List<(string roverId, string gamePointIndicator)>> roverDataOnGrid = new Dictionary<ICoordinate, List<(string, string)>>();
            Dictionary<GamePoint, string> discoveredGamepoints = GetDiscoveredGamepoints(mission);

            var gamePointIndicators = discoveredGamepoints.Keys.ToList();
            int gridSizeX = mission.Plateau.MaxCoordinates.XCoordinate + 1;
            int gridSizeY = mission.Plateau.MaxCoordinates.YCoordinate + 1;
            var grid = new string[gridSizeY, gridSizeX];

            InitializeGrid(grid);

            SetAllRoversGridPositions(mission, grid);

            PrintPlateauGrid(discoveredGamepoints, gridSizeX, gridSizeY, grid);
        }

        private static void PrintPlateauGrid(Dictionary<GamePoint, string> discoveredGamepoints, int gridSizeX, int gridSizeY, string[,] grid)
        {
            Console.WriteLine(new string('-', (8 + 1) * gridSizeX));
            for (int yCoordinate = gridSizeY - 1; yCoordinate >= 0; yCoordinate--)
            {
                for (int line = 0; line < 2; line++)
                {
                    PrtinLine(discoveredGamepoints, gridSizeX, grid, yCoordinate, line);
                }
                Console.WriteLine(new string('-', (8 + 1) * gridSizeX));
            }
        }

        private static void PrtinLine(Dictionary<GamePoint, string> discoveredGamepoints, int gridSizeX, string[,] grid, int yCoordinate, int line)
        {
            for (int xCoordinate = 0; xCoordinate < gridSizeX; xCoordinate++)
            {
                var currentCoordinates = Coordinate.CreateCoordinate(xCoordinate, yCoordinate);
                var config = PaddingConfig.CreatePaddingConfig(grid, currentCoordinates);
                var currentGridSquareGamepoint = discoveredGamepoints.Where(gp => gp.Key.EqualsCoordinates(currentCoordinates)).FirstOrDefault();

                if (line == 0)
                {
                    PrintRoverPosition(config);
                }

                if (line != 0)
                {
                    if (currentGridSquareGamepoint.Key != null && currentGridSquareGamepoint.Value != null)
                    {
                        PrintGamePoint(currentGridSquareGamepoint);
                        continue;
                    }

                    Console.Write("|        ");
                }
            }
            Console.WriteLine("|");
        }

        private static void PrintGamePoint(KeyValuePair<GamePoint,string> currentGridSquareGamepoint)
        {
            var gamePointPadded = SetupGamePointPadding(currentGridSquareGamepoint);
            Console.Write($"|{gamePointPadded}");
        }

        private static void PrintRoverPosition(PaddingConfig config)
        {
            Console.Write($"|{config.CellValue.PadLeft(4 + config.LeftPadding).PadRight(4 + config.RightPadding)}");
        }

        private static string SetupGamePointPadding(KeyValuePair<GamePoint, string> currentGridSquareGamepoint)
        {
            var gamePointIndicator = GetGamePointIndicator(
                currentGridSquareGamepoint.Key.TreasureType,
                currentGridSquareGamepoint.Value.Substring(0, 2));

            // Considering the length of "P1-G" is 6
            int gamePointExtraPadding = 6 - gamePointIndicator.Length; 

            string gamePointPadded = gamePointExtraPadding > 0 ?
                gamePointIndicator.PadRight(6 + gamePointExtraPadding) :
                gamePointIndicator;

            return gamePointPadded;
        }

        private static void SetAllRoversGridPositions(MarsMission mission, string[,] grid)
        {
            foreach (var rover in mission.GetPlayers().SelectMany(player => player.Team))
            {
                var coordinate = Coordinate.CreateCoordinate(rover.Position.XCoordinate, rover.Position.YCoordinate);
                grid[coordinate.YCoordinate, coordinate.XCoordinate] = $"{rover.GetId()}-{rover.Position.Bearing}";
            }
        }

        private static Dictionary<GamePoint, string> GetDiscoveredGamepoints(MarsMission mission)
        {
            var discoveredGamePoints = new Dictionary<GamePoint, string>();
            foreach (var player in mission.GetPlayers())
            {
                var points = player.GetGamePoints();
                if (points != null)
                {
                    foreach (var point in points)
                    {
                        discoveredGamePoints.Add(point.Key as GamePoint, point.Value);
                    }
                }
            }
            return discoveredGamePoints;
        }
            
        private static void InitializeGrid(string[,] grid)
        {
            for (int y = 0; y < grid.GetLength(0); y++)
            {
                for (int x = 0; x < grid.GetLength(1); x++)
                {
                    grid[y, x] = " ";
                }
            }
        }

        private static string GetGamePointIndicator(Prize prize, string playerId)
        {
            switch (prize)
            {
                case Prize.Bronze:
                    return $"{playerId}-B";
                case Prize.Silver:
                    return $"{playerId}-S";
                case Prize.Gold:
                    return $"{playerId}-G";
                default:
                    return "";
            }
        }
    }
}
