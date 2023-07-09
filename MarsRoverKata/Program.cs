using System;
using MarsRover.Helpers;
using MarsRover.Models;

class Program
{
    static void Main()
    {
        var maxCoordinates = MissionInstructions.SetupPlatauCoordinates();
        var maxTeamMembers = MissionInstructions.SetUpTeamLimits();
        var instructionLimit = MissionInstructions.SetupInstructionLimit();

        var mission = new MarsMission(maxCoordinates.Item1, maxCoordinates.Item2, maxTeamMembers, instructionLimit);
        var team1 = mission.Player.Team;

        while (team1.Count < maxTeamMembers)
        {
            var counter = 1;
            var initialRoverCoordinates = MissionInstructions.SetupRoverCoordinates();
            mission.CreateRover(initialRoverCoordinates.Value.Item1,
                                initialRoverCoordinates.Value.Item2,
                                initialRoverCoordinates.Key,
                                $"Team1-Rover{counter}");
            counter++;
        }

        foreach (var rover in team1)
        {
            Console.WriteLine("\n Current Rover position: \n");
            Console.WriteLine($"RoverId: {rover.GetId()}:");
            Console.WriteLine($"XCoordinate: {rover.Position.XCoordinate}, YCoordinate: {rover.Position.YCoordinate}, Bearing: {rover.Position.Bearing}");
            var instructions = MissionInstructions.SetupRoverInstructions();
            mission.Player.GiveRoverInstructions(rover, instructions, mission.GetCommandLimit());
        }
       
        Console.WriteLine("END: Pause to check output:");
        Console.ReadLine();
    }
}
