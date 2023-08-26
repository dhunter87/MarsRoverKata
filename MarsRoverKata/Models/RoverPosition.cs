using MarsRover.Interfaces;

namespace MarsRover.Models
{
    public class RoverPosition : IRoverPosition
    {
        public int XCoordinate { get; set; }
        public int YCoordinate { get; set; }
        public char Bearing { get; set; }

        public static IRoverPosition CreateRoverPosition(ICoordinate coordinate, char bearing)
        {
            return new RoverPosition { XCoordinate = coordinate.XCoordinate, YCoordinate = coordinate.YCoordinate, Bearing = Char.ToUpper(bearing) };
        }
    }
}