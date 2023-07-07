using MarsRoverKata;
using NUnit.Framework;

namespace MarsRoverShould;

public class MarsRoverUnitTests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Mars_Rover_Should_Be_Initailise_With_A_Position()
    {
        //Arrange
        var rover = new MarsRover();

        //Act


        //Assert
        Assert.That(rover, Is.Not.Null);
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
            Assert.That(rover.X, Is.EqualTo(xCoord));
            Assert.That(rover.Y, Is.EqualTo(yCoord));
            Assert.That(rover.Bearing, Is.EqualTo(bearing));
        });
    }


    [Test]
    public void Mars_Rover_Trows_Exception_If_Initailised_With_A_X_Coordinate_Out_Of_Bounds()
    {
        //Arrange

        //Act

        //Assert
        Assert.Throws<ArgumentException>(() => new MarsRover(-1, 0, 'N'));
    }

    [Test]
    public void Mars_Rover_Trows_Exception_If_Initailised_With_A_Y_Coordinate_Out_Of_Bounds()
    {
        //Arrange

        //Act

        //Assert
        Assert.Throws<ArgumentException>(() => new MarsRover(0, -1, 'N'));
    }
}
