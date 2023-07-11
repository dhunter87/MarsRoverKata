using System;
using MarsRover.Helpers;
using MarsRover.Models;

class Program
{
    static void Main()
    {
        var maxCoordinates = MissionInstructions.SetupPlateauCoordinates();
        var maxTeamMembers = MissionInstructions.SetUpTeamLimits();
        var instructionLimit = MissionInstructions.SetupInstructionLimit();
        var playerCount = 2;

        var mission = new MarsMission(maxCoordinates.Item1, maxCoordinates.Item2, maxTeamMembers, instructionLimit, playerCount);
        var playerOne = mission.GetConfiguredPlayers()[0];
        var playerTwo = mission.GetConfiguredPlayers()[1];

        var team1 = playerOne.Team;
        var team2 = playerTwo.Team;


        while (team1.Count < maxTeamMembers)
        {
            var counter = 1;
            var initialRoverCoordinates = MissionInstructions.SetupRoverCoordinates();
            mission.CreateRover(playerOne,initialRoverCoordinates.Value.Item1,
                                initialRoverCoordinates.Value.Item2,
                                initialRoverCoordinates.Key,
                                $"Team1-Rover{counter}");
            counter++;
        }

        while (team2.Count < maxTeamMembers)
        {
            var counter = 1;
            var initialRoverCoordinates = MissionInstructions.SetupRoverCoordinates();
            mission.CreateRover(playerOne, initialRoverCoordinates.Value.Item1,
                                initialRoverCoordinates.Value.Item2,
                                initialRoverCoordinates.Key,
                                $"Team2-Rover{counter}");
            counter++;
        }

        foreach (var rover in team1)
        {
            Console.WriteLine("\n Current Rover position: \n");
            Console.WriteLine($"RoverId: {rover.GetId()}:");
            Console.WriteLine($"XCoordinate: {rover.Position.XCoordinate}, YCoordinate: {rover.Position.YCoordinate}, Bearing: {rover.Position.Bearing}");
            var instructions = MissionInstructions.SetupRoverInstructions();
            playerOne.GiveRoverInstructions(rover, instructions);
        }

        foreach (var rover in team2)
        {
            Console.WriteLine("\n Current Rover position: \n");
            Console.WriteLine($"RoverId: {rover.GetId()}:");
            Console.WriteLine($"XCoordinate: {rover.Position.XCoordinate}, YCoordinate: {rover.Position.YCoordinate}, Bearing: {rover.Position.Bearing}");
            var instructions = MissionInstructions.SetupRoverInstructions();
            playerTwo.GiveRoverInstructions(rover, instructions);
        }

        Console.WriteLine("END: Pause to check output:");
        Console.ReadLine();
    }
}
