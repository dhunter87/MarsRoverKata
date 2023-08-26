using MarsRover.Models;

namespace MarsRover.CLI
{
    public static class PlateauUi
	{
        public static void PrintGrid(Dictionary<GamePoint, string> discoveredGamepoints, int gridSizeX, int gridSizeY, string[,] grid)
        {
            var gridSquareSpaceing = 9;
            Console.WriteLine(new string('-', gridSquareSpaceing * gridSizeX));
            for (int yCoordinate = gridSizeY - 1; yCoordinate >= 0; yCoordinate--)
            {
                for (int line = 0; line < 2; line++)
                {
                    PrtinLine(discoveredGamepoints, gridSizeX, grid, yCoordinate, line);
                }
                Console.WriteLine(new string('-', gridSquareSpaceing * gridSizeX));
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

        private static void PrintGamePoint(KeyValuePair<GamePoint, string> currentGridSquareGamepoint)
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
                currentGridSquareGamepoint.Value[..2]);

            // Considering the length of "P1-G" is 6
            int gamePointExtraPadding = 6 - gamePointIndicator.Length;

            string gamePointPadded = gamePointExtraPadding > 0 ?
                gamePointIndicator.PadRight(6 + gamePointExtraPadding) :
                gamePointIndicator;

            return gamePointPadded;
        }

        private static string GetGamePointIndicator(Prize prize, string playerId)
        {
            return prize switch
            {
                Prize.Bronze => $"{playerId}-B",
                Prize.Silver => $"{playerId}-S",
                Prize.Gold => $"{playerId}-G",
                _ => "",
            };
        }
    }
}

