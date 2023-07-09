using System;
using MarsRover.Helpers;
using MarsRover.Models;

class Program
{
    static void Main()
    {
        var maxCoordinates = MissionInstructions.SetupPlatauCoordinates();
        var maxTeamMembers = MissionInstructions.SetUpTeamLimits();

        var mission = new MarsMission(maxCoordinates.Item1, maxCoordinates.Item2, maxTeamMembers);

        var initialRoverCoordinates = MissionInstructions.SetupRoverCoordinates();

        mission.CreateRover(initialRoverCoordinates.Value.Item1,
                            initialRoverCoordinates.Value.Item2,
                            initialRoverCoordinates.Key);

        var instructions = MissionInstructions.SetupRoverInstructions();

        mission.Player.GiveRoverInstructions(instructions);
       
        Console.WriteLine("END: Pause to check output:");
        Console.ReadLine();
    }
}
