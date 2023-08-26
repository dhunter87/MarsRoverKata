using MarsRover.Models;

namespace MarsRover.CLI
{
    public static class ConsoleApp
    {
        public static void PrintPlateauGrid(MarsMission mission)
        {
            Dictionary<GamePoint, string> discoveredGamepoints = GetDiscoveredGamepoints(mission);

            int gridSizeX = mission.Plateau.MaxCoordinates.XCoordinate + 1;
            int gridSizeY = mission.Plateau.MaxCoordinates.YCoordinate + 1;
            var grid = new string[gridSizeY, gridSizeX];

            InitializeGrid(grid);

            SetAllRoversGridPositions(mission, grid);

            PlateauUi.PrintGrid(discoveredGamepoints, gridSizeX, gridSizeY, grid);
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
            var maxYCoord = grid.GetLength(0);
            var maxXCoord = grid.GetLength(1);

            for (int yAxis = 0; yAxis < maxYCoord; yAxis++)
            {
                for (int xAxis = 0; xAxis < maxXCoord; xAxis++)
                {
                    grid[yAxis, xAxis] = " ";
                }
            }
        }
    }
}