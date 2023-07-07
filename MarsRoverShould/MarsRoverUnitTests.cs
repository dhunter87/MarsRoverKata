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

    [Test]
    public void Given_Mars_Rover_Initailised_With_A_Position()
    {
        //Arrange
        var rover = new MarsRover(0, 0, 'N');

        //Act


        //Assert
        Assert.Multiple(() =>
        {
            Assert.That(rover.X, Is.Not.Null);
            Assert.That(rover.Y, Is.Not.Null);
            Assert.That(rover.Bearing, Is.Not.Null);
        });
    }
}
