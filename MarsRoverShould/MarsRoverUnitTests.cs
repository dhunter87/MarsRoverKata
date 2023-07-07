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
    public void Mars_Rover_Should_Not_Be_Null()
    {
        //Arrange
        var rover = new MarsRover();

        //Act


        //Assert
        Assert.NotNull(rover);

    }
}
