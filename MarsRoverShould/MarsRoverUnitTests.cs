using MarsRoverKata;
using NUnit.Framework;

namespace MarsRoverShould;

public class MarsRoverUnitTests
{
    [SetUp]
    public void Setup()
    {
    }

    [TestCase(0,0,'N')]
    [TestCase(3,5,'W')]
    [TestCase(0100,10000,'S')]
    public void Given_Mars_Rover_Initailised_The_Coordinates_And_Bearing_Are_Stroed(int xCoord, int yCoord, char bearing)
    {
        //Arrange
        var rover = new MarsRover(xCoord, yCoord, bearing);

        //Act


        //Assert
        Assert.Multiple(() =>
        {
            Assert.That(rover.XCoordinate, Is.EqualTo(xCoord));
            Assert.That(rover.YCoordinate, Is.EqualTo(yCoord));
            Assert.That(rover.Bearing, Is.EqualTo(bearing));
        });
    }

    [TestCase(-1, 0, 'N')]
    [TestCase(0, -1, 'N')]
    [TestCase(0, 0, 'Q')]
    public void Mars_Rover_Trows_Exception_If_Initailised_With_An_Coordinate_Or_Bearing(int xCoord, int yCoord, char bearing)
    {
        //Arrange

        //Act

        //Assert
        Assert.Throws<ArgumentException>(() => new MarsRover(0, 0, 'Q'));
    }

    [TestCase(0,0,'N', 0, 1, 'N')]
    [TestCase(0,0,'E', 1, 0, 'E')]
    [TestCase(1,1,'S', 1, 0, 'S')]
    [TestCase(1,1,'W', 0, 1, 'W')]
    public void Mars_Rover_X_Coordinate_Changes_When_Bearing_Is_W_And_ExecuteInstruction_Is_Called_With_M(
        int xCoord, int yCoord, char bearing,
        int expectedXCoord, int expectedYCoord, char expectedBearing)
    {
        //Arrange
        var rover = new MarsRover(xCoord, yCoord, bearing);

        //Act
        rover.ExecuteInstruction('M');

        //Assert
        Assert.That(rover.XCoordinate, Is.EqualTo(expectedXCoord));
        Assert.That(rover.YCoordinate, Is.EqualTo(expectedYCoord));
        Assert.That(rover.Bearing, Is.EqualTo(expectedBearing));
    }

    [Test]
    public void Mars_Rover_Trows_Exception_If_Moved_Out_Of_Bounds()
    {
        //Arrange
        var rover = new MarsRover(0, 0, 'S');
        //Act
        
        //Assert
        Assert.Throws<ArgumentException>(() => rover.ExecuteInstruction('M'));
    }
}
