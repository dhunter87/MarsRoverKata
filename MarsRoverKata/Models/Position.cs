namespace MarsRover.Models
{
    public class Position
    {
        public int XCoordinate;
        public int YCoordinate;
        public char Bearing;

        public Position(int xCoordinate, int yCoordinate, char bearing)
        {
            XCoordinate = xCoordinate;
            YCoordinate = yCoordinate;
            Bearing = bearing;
        }
    }
}