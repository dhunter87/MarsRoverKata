using System;
using System.Collections.Generic;
using MarsRover.Interfaces;
using MarsRover.Models;

namespace MarsRover.CLI
{
    public static class ConsoleAppUi
    {
        public static void PrintRoverPositions(MarsMission mission)
        {
            Dictionary<GamePoint, string> discoveredGamepoints = GetDiscoveredGamepoints(mission);
            var gamePointIndicators = discoveredGamepoints.Keys.ToList();

            int gridSizeX = mission.Plateau.MaxCoordinates.XCoordinate + 1;
            int gridSizeY = mission.Plateau.MaxCoordinates.YCoordinate + 1;

            var grid = new string[gridSizeY, gridSizeX];
            InitializeGrid(grid);

            Dictionary<ICoordinate, List<(string roverId, string gamePointIndicator)>> roverDataOnGrid = new Dictionary<ICoordinate, List<(string, string)>>();

            foreach (var player in mission.GetPlayers())
            {
                foreach (var rover in player.Team)
                {
                    var coordinate = Coordinate.CreateCoordinate(rover.Position.XCoordinate, rover.Position.YCoordinate);

                    grid[coordinate.YCoordinate, coordinate.XCoordinate] = $"{rover.GetId()}-{rover.Position.Bearing}";
                }
            }


            int maxIdLength = CalculateMaxIdLength(mission.GetPlayers());

            // Print the grid
            Console.WriteLine(new string('-', (8 + 1) * gridSizeX));
            for (int y = gridSizeY - 1; y >= 0; y--)
            {
                for (int line = 0; line < 2; line++)
                {
                    for (int x = 0; x < gridSizeX; x++)
                    {
                        var coordinate = Coordinate.CreateCoordinate(x,y);
                        string cellValue = grid[y, x].PadLeft(8);
                        int extraPadding = maxIdLength - cellValue.Length;
                        int leftPadding = extraPadding / 2;
                        int rightPadding = extraPadding - leftPadding;

                        if (line == 0)
                        {
                            Console.Write($"|{cellValue.PadLeft(4 + leftPadding).PadRight(4 + rightPadding)}");
                        }
                        else
                        {
                            //var roverData = roverDataOnGrid.ContainsKey(coordinate) ? roverDataOnGrid[coordinate] : new List<(string, string)>();
                            var currentGridSquareGamepoint = discoveredGamepoints.Where(gp => gp.Key.EqualsCoordinates(coordinate)).FirstOrDefault();
                            if (currentGridSquareGamepoint.Key != null && currentGridSquareGamepoint.Value != null)
                            {
                                var gamepoint = currentGridSquareGamepoint.Key;
                                var roverid = currentGridSquareGamepoint.Value;

                                var gamePointIndicator = GetGamePointIndicator(gamepoint.TreasureType, roverid.Substring(0,2));

                                // Calculate padding for the gamePointIndicator
                                int gamePointExtraPadding = 6 - gamePointIndicator.Length; // Considering the length of "P1-G" is 6
                                string gamePointPadded = gamePointExtraPadding > 0 ? gamePointIndicator.PadRight(6 + gamePointExtraPadding) : gamePointIndicator;

                                Console.Write($"|{gamePointPadded}");
                            }
                            else
                            {
                                Console.Write("|        ");
                            }
                        }
                    }
                    Console.WriteLine("|");
                }
                Console.WriteLine(new string('-', (8 + 1) * gridSizeX));
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

        private static int CalculateMaxIdLength(List<Player> players)
        {
            int maxIdLength = 0;
            foreach (var player in players)
            {
                foreach (var rover in player.Team)
                {
                    int idLength = rover.GetId().Length;
                    if (idLength > maxIdLength)
                    {
                        maxIdLength = idLength;
                    }
                }
            }
            return maxIdLength;
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
