﻿using MarsRoverKata;
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
    public void Given_Mars_Rover_Initailised_The_Coordinates_And_Bearing_Are_Stored(int xCoord, int yCoord, char bearing)
    {
        var rover = new MarsRover(xCoord, yCoord, bearing);

        Assert.Multiple(() =>
        {
            Assert.That(rover.Position.XCoordinate, Is.EqualTo(xCoord));
            Assert.That(rover.Position.YCoordinate, Is.EqualTo(yCoord));
            Assert.That(rover.Position.Bearing, Is.EqualTo(bearing));
        });
    }

    [TestCase('n', 'N')]
    [TestCase('e', 'E')]
    [TestCase('s', 'S')]
    [TestCase('w', 'W')]
    public void Given_Mars_Rover_Initailised_With_A_Lowecase_Bearing_Value_The_Bearing_Is_Stored_In_Uppercase(char bearing, char expectedBearing)
    {
        var rover = new MarsRover(0, 0, bearing);

        Assert.That(rover.Position.Bearing, Is.EqualTo(expectedBearing));
    }

    [TestCase(-1, 0, 'N')]
    [TestCase(0, -1, 'N')]
    [TestCase(0, 0, 'Q')]
    public void Mars_Rover_Trows_Exception_If_Initailised_With_An_Invalid_Coordinate_Or_Bearing(int xCoord, int yCoord, char bearing)
    {
        Assert.Throws<ArgumentException>(() => new MarsRover(0, 0, 'Q'));
    }

    [TestCase(0,0,'N', 0, 1, 'N')]
    [TestCase(0,0,'E', 1, 0, 'E')]
    [TestCase(1,1,'S', 1, 0, 'S')]
    [TestCase(1,1,'W', 0, 1, 'W')]
    public void Mars_Rover_Coordinates_Change_Relevant_To_Its_Bearing_When_ExecuteInstruction_Is_Called_With_M_Command(
        int xCoord, int yCoord, char bearing,
        int expectedXCoord, int expectedYCoord, char expectedBearing)
    {
        var rover = new MarsRover(xCoord, yCoord, bearing);

        rover.ExecuteInstruction(RoverCommand.M);

        Assert.Multiple(() =>
        {
            Assert.That(rover.Position.XCoordinate, Is.EqualTo(expectedXCoord));
            Assert.That(rover.Position.YCoordinate, Is.EqualTo(expectedYCoord));
            Assert.That(rover.Position.Bearing, Is.EqualTo(expectedBearing));
        });
    }

    [TestCase(0,0,'N','W')]
    [TestCase(0,0,'W','S')]
    [TestCase(0,0,'S','E')]
    [TestCase(0,0,'E','N')]
    public void Mars_Rovers_Bearing_Changes_To_Expected_Bearing_When_Given_L_Instruction(int xCord, int yCord, char bearing, char expectedBearing)
    {
        var rover = new MarsRover(xCord, yCord, bearing);

        rover.ExecuteInstruction(RoverCommand.L);

        Assert.That(rover.Position.Bearing, Is.EqualTo(expectedBearing));
    }

    [TestCase(0, 0, 'N', 'E')]
    [TestCase(0, 0, 'E', 'S')]
    [TestCase(0, 0, 'S', 'W')]
    [TestCase(0, 0, 'W', 'N')]
    public void Mars_Rovers_Bearing_Changes_To_Expected_Bearing_When_Given_R_Instruction(int xCord, int yCord, char bearing, char expectedBearing)
    {
        var rover = new MarsRover(xCord, yCord, bearing);

        rover.ExecuteInstruction(RoverCommand.R);

        Assert.That(rover.Position.Bearing, Is.EqualTo(expectedBearing));
    }

    [TestCase(0,0,'N',"MM",0,2,'N')]
    [TestCase(0,0,'N',"MMRM",1,2,'E')]
    [TestCase(5,5,'S',"MM",5,3,'S')]
    [TestCase(5,5,'S',"MMRM",4,3,'W')]
    public void Mars_Rover_Executes_A_Sequence_Of_Instructions_MMRM(int xCord, int yCord, char bearing, string instructions, int expectedXCoord, int expectedYCoord, char expectedBearing)
    {
        var rover = new MarsRover(xCord, yCord, bearing);

        rover.ExecuteInstructions(instructions);

        Assert.Multiple(() =>
        {
            Assert.That(rover.Position.YCoordinate, Is.EqualTo(expectedYCoord));
            Assert.That(rover.Position.XCoordinate, Is.EqualTo(expectedXCoord));
            Assert.That(rover.Position.Bearing, Is.EqualTo(expectedBearing));
        });
    }

    [TestCase(0, 0, 'N', "mm", 0, 2, 'N')]
    [TestCase(0, 0, 'N', "mmrm", 1, 2, 'E')]
    [TestCase(5, 5, 'S', "Mm", 5, 3, 'S')]
    [TestCase(5, 5, 'S', "MmRm", 4, 3, 'W')]
    public void Given_Valid_Lowecase_Values_ExecuteInstructions_Perfoms_Expected_Actions(int xCord, int yCord, char bearing, string instructions, int expectedXCoord, int expectedYCoord, char expectedBearing)
    {
        var rover = new MarsRover(xCord, yCord, bearing);

        rover.ExecuteInstructions(instructions);

        Assert.Multiple(() =>
        {
            Assert.That(rover.Position.YCoordinate, Is.EqualTo(expectedYCoord));
            Assert.That(rover.Position.XCoordinate, Is.EqualTo(expectedXCoord));
            Assert.That(rover.Position.Bearing, Is.EqualTo(expectedBearing));
        });
    }

    [TestCase(0, 0, 'N', "mQm", 0, 2, 'N')]
    [TestCase(0, 0, 'N', "mSmprzm", 1, 2, 'E')]
    [TestCase(5, 5, 'S', "Mzzzm", 5, 3, 'S')]
    [TestCase(5, 5, 'S', "MMqweRtyM", 4, 3, 'W')]
    public void Mars_Rover_Executes_A_Sequence_Of_Instructions_MMRM_And_Ignores_Invalid_Commands(int xCord, int yCord, char bearing, string instructions, int expectedXCoord, int expectedYCoord, char expectedBearing)
    {
        var rover = new MarsRover(xCord, yCord, bearing);

        rover.ExecuteInstructions(instructions);

        Assert.Multiple(() =>
        {
            Assert.That(rover.Position.YCoordinate, Is.EqualTo(expectedYCoord));
            Assert.That(rover.Position.XCoordinate, Is.EqualTo(expectedXCoord));
            Assert.That(rover.Position.Bearing, Is.EqualTo(expectedBearing));
        });
    }


    [Test]
    [Ignore("This test is being ignored for until implementation has developed further.")]
    public void Mars_Rover_Trows_Exception_If_Moved_Out_Of_Bounds()
    {
        //Arrange
        var rover = new MarsRover(0, 0, 'S');
        //Act

        //Assert
        Assert.Throws<ArgumentException>(() => rover.ExecuteInstruction(RoverCommand.M));
    }

}
