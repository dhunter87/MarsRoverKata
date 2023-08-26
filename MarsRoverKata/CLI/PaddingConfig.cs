using MarsRover.Interfaces;

namespace MarsRover.CLI
{
    public class PaddingConfig
    {
        public string? CellValue;
        public int ExtraPadding;
        public int LeftPadding;
        public int RightPadding;

        public static PaddingConfig CreatePaddingConfig(string[,] grid, ICoordinate coordinate)
        {
            var maxIdLength = 1;
            var cellValue = grid[coordinate.YCoordinate, coordinate.XCoordinate].PadLeft(8);
            var extraPadding = maxIdLength - cellValue.Length;
            var leftPadding = extraPadding / 2;
            var rightPadding = extraPadding - leftPadding;

            return new PaddingConfig
            {
                CellValue = cellValue,
                ExtraPadding = extraPadding,
                LeftPadding = leftPadding,
                RightPadding = rightPadding
            };
        }
    }
}

