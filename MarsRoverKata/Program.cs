using System;
using MarsRover.Helpers;
using MarsRover.Models;

class Program
{
    static void Main()
    {
        var maxCoordinates = MissionInstructions.GetSetupInstructions();

        MarsMission mission = new MarsMission(maxCoordinates.Item1, maxCoordinates.Item2);

        var initialRoverCoordinates = MissionInstructions.GetRoverSetupInstructions();

        mission.CreateRover(initialRoverCoordinates.Value.Item1,
                            initialRoverCoordinates.Value.Item2,
                            initialRoverCoordinates.Key);

        // Give Rover Instructions
        Console.WriteLine("Enter Rover Instructions to Roam Mars!");
        Console.WriteLine("Instructions must be R (Rotate 90 degrees Right), L (Rotate 90 degrees Left) or M (Move forward 1 grid square)");
        Console.WriteLine("e.g. MMMMMRRMMMMMLLMMMMMRRMMM");
        var instructions = Console.ReadLine();


        //output end position
    }
}
