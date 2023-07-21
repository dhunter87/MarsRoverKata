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
            int gridSizeX = mission.Plateau.MaxCoordinates.XCoordinate + 1;
            int gridSizeY = mission.Plateau.MaxCoordinates.YCoordinate + 1;

            var grid = new string[gridSizeX, gridSizeY];
            InitializeGrid(grid);

            Dictionary<(int, int), List<(string roverId, string gamePointIndicator)>> roverDataOnGrid = new Dictionary<(int, int), List<(string, string)>>();

            foreach (var player in mission.GetPlayers())
            {
                foreach (var rover in player.Team)
                {
                    int x = rover.Position.XCoordinate;
                    int y = rover.Position.YCoordinate;
                    grid[x, y] = $"{rover.GetId()}-{rover.Position.Bearing}";

                    var gamePoint = player.GetGamePointAt(x, y);
                    if (gamePoint != null)
                    {
                        var roverId = rover.GetId();
                        var gamePointIndicator = GetGamePointIndicator(gamePoint.TreasureType);

                        if (!roverDataOnGrid.ContainsKey((x, y)))
                        {
                            roverDataOnGrid[(x, y)] = new List<(string, string)>();
                        }

                        roverDataOnGrid[(x, y)].Add((roverId, gamePointIndicator));
                    }
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
                        string cellValue = grid[x, y].PadLeft(8);
                        int extraPadding = maxIdLength - cellValue.Length;
                        int leftPadding = extraPadding / 2;
                        int rightPadding = extraPadding - leftPadding;

                        if (line == 0)
                        {
                            Console.Write($"|{cellValue.PadLeft(4 + leftPadding).PadRight(4 + rightPadding)}");
                        }
                        else
                        {
                            var roverData = roverDataOnGrid.ContainsKey((x, y)) ? roverDataOnGrid[(x, y)] : new List<(string, string)>();

                            if (roverData.Count > 0)
                            {
                                var (roverId, gamePointIndicator) = roverData[roverData.Count - 1];
                                Console.Write($"|{gamePointIndicator.PadLeft(4)}");
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

        private static void InitializeGrid(string[,] grid)
        {
            for (int y = 0; y < grid.GetLength(1); y++)
            {
                for (int x = 0; x < grid.GetLength(0); x++)
                {
                    grid[x, y] = " ";
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

        private static string GetGamePointIndicator(Prize prize)
        {
            switch (prize)
            {
                case Prize.Bronze:
                    return "B";
                case Prize.Silver:
                    return "S";
                case Prize.Gold:
                    return "G";
                default:
                    return "";
            }
        }
    }
}
